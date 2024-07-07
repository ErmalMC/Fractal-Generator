using System.Drawing.Imaging;

namespace Fractal_Generator
{
    public partial class Quadratic_Julia_Set : Form
    {
        private int MaxIterations = 100;
        private double XMin = -2.0, XMax = 2.0, YMin = -2.0, YMax = 2.0;
        private Bitmap? bitmap;
        private double juliaReal = -0.7, juliaImaginary = 0.27015; // Adjust Julia constant here
        private readonly List<Color> colorPalette = [Color.Black, Color.Red, Color.Green, Color.Yellow];
        private readonly int MaxColors = 4; // Maximum number of colors allowed in the palette
        public Quadratic_Julia_Set()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Quadratic_Julia_Set_Paint); // Add an event handler for the Paint event of the form
            this.Resize += new EventHandler(Form1_Resize); // Add an event handler for the Resize event of the form
            this.DoubleBuffered = true; // Enable double buffering for smoother rendering
            UpdateBounds();
        }

        private void Quadratic_Julia_Set_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Clear the previous drawing
            DrawJuliaSet(g, this.ClientSize.Width, this.ClientSize.Height);
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
                XMin = -2.5 * aspectRatio;
                XMax = 2.5 * aspectRatio;
                YMin = -2.5;
                YMax = 2.5;
            }
            else
            {
                XMin = -2.5;
                XMax = 2.5;
                YMin = -2.5 / aspectRatio;
                YMax = 2.5 / aspectRatio;
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
        private void DrawJuliaSet(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height);
            double xScale = (XMax - XMin) / width;
            double yScale = (YMax - YMin) / height;

            Parallel.For(0, width, px => // Use a parallel loop to iterate over each pixel in the bitma
            {
                for (int py = 0; py < height; py++) // Iterate over each pixel in the current column
                {
                    double x0 = XMin + xScale * px;
                    double y0 = YMin + yScale * py;
                    double zReal = x0, zImaginary = y0;
                    int iteration = 0;
                    // Iterate until the magnitude of zReal and zImaginary squared is greater than or equal to 4,
                    // or the maximum number of iterations is reached
                    while (zReal * zReal + zImaginary * zImaginary < 4 && iteration < MaxIterations)
                    {
                        double zNextReal = zReal * zReal - zImaginary * zImaginary + juliaReal;
                        // Calculate the new values of zReal and zImaginary using the Julia set formula
                        double zNextImaginary = 2 * zReal * zImaginary + juliaImaginary;
                        zReal = zNextReal;
                        zImaginary = zNextImaginary;
                        iteration++;
                    }

                    Color color = GetColor(iteration); // Get the color based on the final iteration count
                    lock (bitmap)  // Set the pixel color in the bitmap using a lock
                    {
                        bitmap.SetPixel(px, py, color);
                    }
                }
            });

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
            using Options settingsForm = new(juliaReal, juliaImaginary, MaxIterations);
            settingsForm.tbJuliaReal.Visible = true;
            settingsForm.tbJuliaImagined.Visible = true;
            settingsForm.lblJuliaReal.Visible = true;
            settingsForm.lblJuliaImagined.Visible = true;
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                juliaReal = settingsForm.JuliaReal;
                juliaImaginary = settingsForm.JuliaImaginary;
                MaxIterations = settingsForm.MaxIterations;
                this.Invalidate(); // Redraw with new settings
            }
        }
    }
}
