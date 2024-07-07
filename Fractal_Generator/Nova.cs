using System.Drawing.Imaging;

namespace Fractal_Generator
{
    public partial class Nova : Form
    {
        private int MaxIterations = 100;
        private double Tolerance = 0.0001;
        private double Gamma = 0.154; // Perturbation factor
        private double XMin, XMax, YMin, YMax;
        private Bitmap? bitmap;
        private readonly List<Color> colorPalette = [Color.Black, Color.Red, Color.Green, Color.Yellow];
        private readonly int MaxColors = 4; // Maximum number of colors allowed in the palette

        // New parameters
        private readonly double StartValue = 0.5; // Start value parameter
        private readonly double RelaxationValue = 0.5; // Relaxation value parameter
        public Nova()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Nova_Paint); // Add an event handler for the Paint event of the form
            this.Resize += new EventHandler(Form1_Resize); // Add an event handler for the Resize event of the form
            UpdateBounds();
            this.DoubleBuffered = true; // Enable double buffering for smoother rendering
        }

        private void Nova_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Clear the previous drawing
            DrawNovaFractal(g, this.ClientSize.Width, this.ClientSize.Height);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateBounds();
            this.Invalidate(); // Force the form to redraw itself
        }
        private new void UpdateBounds()
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
        private void DrawNovaFractal(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * height;
            byte[] pixels = new byte[byteCount];

            double xScale = (XMax - XMin) / width;
            double yScale = (YMax - YMin) / height;

            Parallel.For(0, height, py => // Use a parallel loop to iterate over each pixel in the bitmap
            {
                int rowStart = py * bmpData.Stride;
                double y0 = YMin + yScale * py;
                for (int px = 0; px < width; px++) // Iterate over each pixel in the current row
                {
                    double x0 = XMin + xScale * px;
                    double zx = x0;
                    double zy = y0;
                    int iteration = 0;

                    while (iteration < MaxIterations) // Iterate until the maximum number of iterations is reached or the condition is met
                    {
                        double zx2 = zx * zx;
                        double zy2 = zy * zy;
                        double zxCubed = zx * zx2 - 3 * zx * zy2;
                        double zyCubed = 3 * zx2 * zy - zy * zy2;
                        double denom = zx2 + zy2;

                        // Apply relaxation value here
                        double relaxationFactor = RelaxationValue * Math.Pow(denom, 2);

                        double zxn = zx - Gamma * (zxCubed - StartValue * zx) / (denom + relaxationFactor);
                        double zyn = zy - Gamma * (zyCubed - StartValue * zy) / (denom + relaxationFactor);

                        if ((zxn - zx) * (zxn - zx) + (zyn - zy) * (zyn - zy) < Tolerance * Tolerance) // Check if the change in zx and zy is less than the tolerance value
                        {
                            break;
                        }
                        zx = zxn;
                        zy = zyn;
                        iteration++;
                    }

                    Color color = GetColor(iteration); // Get the color based on the final iteration count
                    int pixelIndex = rowStart + px * bytesPerPixel;
                    pixels[pixelIndex] = color.B;
                    pixels[pixelIndex + 1] = color.G;
                    pixels[pixelIndex + 2] = color.R;
                }
            });

            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length); // Copy the pixels array back to the locked bitmap data
            bitmap.UnlockBits(bmpData);
            g.DrawImage(bitmap, 0, 0);
        }


        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSaveFile.Filter = "Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg|GIF Image|*.gif|PNG Image|*.png|TIFF Image|*.tif;*.tiff";
            dlgSaveFile.FilterIndex = 4;
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                string filename = dlgSaveFile.FileName;
                string extension = filename[filename.LastIndexOf('.')..];
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

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorPalette.Clear();

            using ColorDialog colorDialog = new();
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
                colorPalette.Add(Color.Black);
                colorPalette.Add(Color.Red);
                colorPalette.Add(Color.Green);
                colorPalette.Add(Color.Yellow);
            }

            this.Invalidate(); // Force the form to redraw itself
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using Options settingsForm = new(MaxIterations, Gamma, Tolerance);
            settingsForm.tbGamma.Visible = true;
            settingsForm.tbTolerance.Visible = true;
            settingsForm.lblGamma.Visible = true;
            settingsForm.lblTolerance.Visible = true;
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                Gamma = settingsForm.Gamma;
                Tolerance = settingsForm.Tolerance;
                MaxIterations = settingsForm.MaxIterations;
                this.Invalidate(); // Redraw with new settings
            }
        }
    }


}