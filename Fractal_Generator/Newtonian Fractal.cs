using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal_Generator
{
    public partial class Newtonian_Fractal : Form
    {
        private int MaxIterations = 100;
        private double Tolerance = 0.0001;
        private double XMin, XMax, YMin, YMax;
        private Bitmap bitmap;
        private List<Color> colorPalette = new List<Color> { Color.Black, Color.Red, Color.Green, Color.Yellow };
        private int MaxColors = 4; // Maximum number of colors allowed in the palette
        // New parameters
        private double RelaxationParameter = 0.5; // Relaxation parameter
        private int PolynomialDegree = 4; // Polynomial degree

        private Complex[] roots;
        public Newtonian_Fractal()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Newtonian_Fractal_Paint);
            this.Resize += new EventHandler(Form1_Resize);
            UpdateBounds();
            InitializeRoots();
            this.DoubleBuffered = true;
        }
        private void InitializeRoots()
        {
            roots = ComputeRoots(PolynomialDegree);
        }
        private void Newtonian_Fractal_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Clear the previous drawing
            DrawNewtonianFractal(g, this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateBounds();
            this.Invalidate(); // Force the form to redraw itself
        }

        private void UpdateBounds()
        {
            double aspectRatio = (double)this.ClientSize.Width / this.ClientSize.Height;

            if (aspectRatio > 1)
            {
                XMin = -2.0 * aspectRatio;
                XMax = 2.0 * aspectRatio;
                YMin = -2.0;
                YMax = 2.0;
            }
            else
            {
                XMin = -2.0;
                XMax = 2.0;
                YMin = -2.0 / aspectRatio;
                YMax = 2.0 / aspectRatio;
            }
        }

        private void DrawNewtonianFractal(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height);
            double dx = (XMax - XMin) / width;
            double dy = (YMax - YMin) / height;

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * height;
            byte[] pixels = new byte[byteCount];

            Parallel.For(0, width, px =>
            {
                for (int py = 0; py < height; py++)
                {
                    double x0 = XMin + dx * px;
                    double y0 = YMin + dy * py;
                    Complex z = new Complex(x0, y0);
                    int iteration = 0;

                    while (iteration < MaxIterations)
                    {
                        Complex zToN = z.Power(PolynomialDegree);
                        Complex zToNMinusOne = zToN / z;
                        Complex numerator = zToN - Complex.One;
                        Complex denominator = PolynomialDegree * zToNMinusOne;
                        Complex zNew = z - RelaxationParameter * (numerator / denominator);

                        if (zNew.DistanceSquaredTo(z) < Tolerance * Tolerance)
                        {
                            break;
                        }
                        z = zNew;
                        iteration++;
                    }

                    Color color = GetColor(iteration);
                    int pixelIndex = py * bmpData.Stride + px * bytesPerPixel;
                    pixels[pixelIndex] = color.B;
                    pixels[pixelIndex + 1] = color.G;
                    pixels[pixelIndex + 2] = color.R;
                    pixels[pixelIndex + 3] = 255; // Alpha channel (opaque)
                }
            });

            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length);
            bitmap.UnlockBits(bmpData);

            g.DrawImage(bitmap, 0, 0);
        }


        private Complex[] ComputeRoots(int degree)
        {
            Complex[] roots = new Complex[degree];
            double angleIncrement = 2 * Math.PI / degree;
            for (int i = 0; i < degree; i++)
            {
                double angle = i * angleIncrement;
                roots[i] = new Complex(Math.Cos(angle), Math.Sin(angle));
            }
            return roots;
        }

        private Color GetColor(int iteration)
        {
            if (iteration == MaxIterations)
            {
                return Color.Black;
            }

            return ColorFromPalette(iteration);
        }

        private Color ColorFromPalette(int iteration)
        {
            int colorCount = colorPalette.Count;
            double t = (double)iteration / MaxIterations;
            double scaledT = t * (colorCount - 1);
            int index = (int)scaledT;
            double blend = scaledT - index;

            Color startColor = colorPalette[index];
            Color endColor = colorPalette[Math.Min(index + 1, colorCount - 1)];

            int r = (int)(startColor.R * (1 - blend) + endColor.R * blend);
            int g = (int)(startColor.G * (1 - blend) + endColor.G * blend);
            int b = (int)(startColor.B * (1 - blend) + endColor.B * blend);

            return Color.FromArgb(r, g, b);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSaveFile.Filter = "Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg|GIF Image|*.gif|PNG Image|*.png|TIFF Image|*.tif;*.tiff";
            dlgSaveFile.FilterIndex = 4;
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                string filename = dlgSaveFile.FileName;
                string extension = filename.Substring(filename.LastIndexOf("."));
                ImageFormat imageFormat = extension switch
                {
                    ".bmp" => ImageFormat.Bmp,
                    ".jpg" or ".jpeg" => ImageFormat.Jpeg,
                    ".gif" => ImageFormat.Gif,
                    ".png" => ImageFormat.Png,
                    ".tif" or ".tiff" => ImageFormat.Tiff,
                    _ => ImageFormat.Png,
                };
                bitmap.Save(filename, imageFormat);
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorPalette.Clear();

            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.AllowFullOpen = true;
                colorDialog.AnyColor = true;
                colorDialog.FullOpen = true;
                colorDialog.SolidColorOnly = false;

                for (int i = 0; i < MaxColors; i++)
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        colorPalette.Add(colorDialog.Color);
                    }
                    else
                    {
                        break; // Exit loop if dialog is cancelled
                    }
                }

                if (colorPalette.Count == 0)
                {
                    // If no colors are selected, revert to default palette
                    colorPalette.Add(Color.Blue);
                    colorPalette.Add(Color.Red);
                    colorPalette.Add(Color.Green);
                    colorPalette.Add(Color.Yellow);
                }

                this.Invalidate(); // Force the form to redraw itself
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (Options settingsForm = new Options(PolynomialDegree,Tolerance,RelaxationParameter,MaxIterations))
            {
                settingsForm.tbTolerance.Visible = true;
                settingsForm.lblTolerance.Visible = true;
                settingsForm.tbRelaxation.Visible=true;
                settingsForm.lblRelaxation.Visible = true;
                settingsForm.tbExponent.Visible=true;
                settingsForm.lblExponent.Visible=true;
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    PolynomialDegree = Convert.ToInt32(settingsForm.Exponent);
                    Tolerance=settingsForm.Tolerance;
                    RelaxationParameter = settingsForm.Relaxation;
                    MaxIterations = settingsForm.MaxIterations;
                    this.Invalidate(); // Redraw with new settings
                }
            }
        }
    }

    public struct Complex
    {
        public double Real { get; }
        public double Imaginary { get; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static Complex operator +(Complex a, Complex b) => new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        public static Complex operator -(Complex a, Complex b) => new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        public static Complex operator *(Complex a, Complex b) => new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        public static Complex operator *(Complex a, double b) => new Complex(a.Real * b, a.Imaginary * b);
        public static Complex operator *(double a, Complex b) => new Complex(a * b.Real, a * b.Imaginary);
        public static Complex operator /(Complex a, Complex b)
        {
            double denom = b.Real * b.Real + b.Imaginary * b.Imaginary;
            return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) / denom, (a.Imaginary * b.Real - a.Real * b.Imaginary) / denom);
        }

        public double MagnitudeSquared => Real * Real + Imaginary * Imaginary;

        public Complex Power(int exponent)
        {
            if (exponent == 0)
            {
                return Complex.One;
            }

            Complex result = this;
            for (int i = 1; i < exponent; i++)
            {
                result *= this;
            }
            return result;
        }

        public double DistanceSquaredTo(Complex other)
        {
            double dx = Real - other.Real;
            double dy = Imaginary - other.Imaginary;
            return dx * dx + dy * dy;
        }

        public override string ToString()
        {
            return $"({Real}, {Imaginary})";
        }

        public static Complex One => new Complex(1, 0);
    }
}