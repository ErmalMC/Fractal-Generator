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
            this.Paint += new PaintEventHandler(Newtonian_Fractal_Paint); // Add an event handler for the Paint event of the form
            this.Resize += new EventHandler(Form1_Resize); // Add an event handler for the Resize event of the form
            UpdateBounds();
            InitializeRoots();
            this.DoubleBuffered = true; // Enable double buffering for smoother rendering
        }
        private void InitializeRoots() // Intializes the roots 
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

            // Lock the bitmap's bits and get the bitmap data
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * height;
            byte[] pixels = new byte[byteCount];


            Parallel.For(0, width, px => // Use Parallel.For to iterate over the pixels in the bitmap
            {
                for (int py = 0; py < height; py++)
                {
                    // Calculate the initial x and y coordinates
                    double x0 = XMin + dx * px;
                    double y0 = YMin + dy * py;

                    // Create a new Complex number with the initial x and y coordinates
                    Complex z = new Complex(x0, y0);
                    int iteration = 0;

                    while (iteration < MaxIterations) // Perform the Newtonian fractal calculation
                    {
                        Complex zToN = z.Power(PolynomialDegree); // Calculate the numerator and denominator of the Newtonian formula
                        Complex zToNMinusOne = zToN / z;
                        Complex numerator = zToN - Complex.One;
                        Complex denominator = PolynomialDegree * zToNMinusOne;
                        Complex zNew = z - RelaxationParameter * (numerator / denominator); // Update the z value using the Newtonian formula

                        if (zNew.DistanceSquaredTo(z) < Tolerance * Tolerance) // Check if the iteration has converged
                        {
                            break;
                        }
                        z = zNew;
                        iteration++;
                    }

                    Color color = GetColor(iteration);
                    int pixelIndex = py * bmpData.Stride + px * bytesPerPixel;  // Set the pixel color in the pixel array
                    pixels[pixelIndex] = color.B;
                    pixels[pixelIndex + 1] = color.G;
                    pixels[pixelIndex + 2] = color.R;
                    pixels[pixelIndex + 3] = 255; // Alpha channel (opaque)
                }
            });

            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length); // Copy the pixel array back to the bitmap and unlock the bitmap data
            bitmap.UnlockBits(bmpData);

            g.DrawImage(bitmap, 0, 0); //Draw the bitmap on the form's graphics surface
        }


        private Complex[] ComputeRoots(int degree)  // Calculates the roots of a polynomial equation with the specified degree
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
        // The real part of the complex number
        public double Real { get; }

        // The imaginary part of the complex number
        public double Imaginary { get; }

        // Constructor that initializes the real and imaginary parts
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Overloaded addition operator
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        // Overloaded subtraction operator
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        // Overloaded multiplication operator for two Complex numbers
        public static Complex operator *(Complex a, Complex b)
        {
            // Calculate the real and imaginary parts of the product using the formula:
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        // Overloaded multiplication operator for a Complex number and a scalar
        public static Complex operator *(Complex a, double b)
        {
            return new Complex(a.Real * b, a.Imaginary * b);
        }

        // Overloaded multiplication operator for a scalar and a Complex number
        public static Complex operator *(double a, Complex b)
        {
            return new Complex(a * b.Real, a * b.Imaginary);
        }

        // Overloaded division operator for two Complex numbers
        public static Complex operator /(Complex a, Complex b)
        {
            // Calculate the denominator as the sum of the squares of the real and imaginary parts of b
            double denom = b.Real * b.Real + b.Imaginary * b.Imaginary;

            // Calculate the real and imaginary parts of the quotient using the formula:
            return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) / denom, (a.Imaginary * b.Real - a.Real * b.Imaginary) / denom);
        }

        // Property that returns the squared magnitude of the complex number
        public double MagnitudeSquared => Real * Real + Imaginary * Imaginary;

        // Method that calculates the complex number raised to a given integer exponent
        public Complex Power(int exponent)
        {
            // If the exponent is 0, return the complex number 1 + 0i
            if (exponent == 0)
            {
                return Complex.One;
            }

            // Initialize the result to the current complex number
            Complex result = this;

            // Multiply the result by the current complex number (exponent - 1) times
            for (int i = 1; i < exponent; i++)
            {
                result *= this;
            }

            // Return the final result
            return result;
        }

        // Method that calculates the squared distance between the current complex number and another complex number
        public double DistanceSquaredTo(Complex other)
        {
            // Calculate the differences between the real and imaginary parts
            double dx = Real - other.Real;
            double dy = Imaginary - other.Imaginary;

            // Return the squared distance
            return dx * dx + dy * dy;
        }

        // Override the ToString method to provide a string representation of the complex number
        public override string ToString()
        {
            return $"({Real}, {Imaginary})";
        }

        // Static property that represents the complex number 1 + 0i
        public static Complex One => new Complex(1, 0);
    }

}