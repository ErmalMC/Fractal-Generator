using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Fractal_Generator
{
    public partial class Mandelbrot_Set : Form
    {
        private int MaxIterations = 64;
        private double XMin, XMax, YMin, YMax;
        private readonly List<Color> colorPalette = [Color.Black, Color.Red, Color.Green, Color.Yellow];
        private readonly int MaxColors = 4; // Maximum number of colors allowed in the palette
        private Bitmap? bitmap;
        public Mandelbrot_Set()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Mandelbrot_Set_Paint); // Add an event handler for the Paint event of the form
            this.Resize += new EventHandler(Form1_Resize); // Add an event handler for the Resize event of the form
            UpdateBounds();
            this.DoubleBuffered = true; // Enable double buffering for smoother rendering
        }

        private void Mandelbrot_Set_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);
            DrawMandelbrot(g, this.ClientSize.Width, this.ClientSize.Height);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateBounds();
            this.Invalidate(); // Force the form to redraw itself
        }
        private new void UpdateBounds()
        {
            double aspectRatio = (double)this.ClientSize.Width / this.ClientSize.Height;

            if (aspectRatio > 1) // Check if the aspect ratio is greater than 1 (landscape orientation)
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
        private Color GetColor(int iteration) //Returns black if current iteration is last iteration, otherwise returns corresponding color 
        {
            if (iteration == MaxIterations)
            {
                return Color.Black;
            }

            return ColorFromPalette(iteration);
        }

        private Color ColorFromPalette(int iteration)  // Determines the color to be used for a given iteration of the fractal calculation
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
        private void DrawMandelbrot(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height);
            Parallel.For(0, width, px =>
            {
                for (int py = 0; py < height; py++)
                {
                    double x0 = XMin + (XMax - XMin) * px / width;
                    double y0 = YMin + (YMax - YMin) * py / height;
                    double x = 0.0;
                    double y = 0.0;
                    int iteration = 0;

                    while (x * x + y * y <= 4 && iteration < MaxIterations) // Perform the Mandelbrot fractal calculation
                    {
                        double xtemp = x * x - y * y + x0;
                        y = 2 * x * y + y0;
                        x = xtemp;
                        iteration++;
                    }

                    Color color = GetColor(iteration); //Get the pixel color
                    lock (bitmap) //Lock the bitmap and set pixel color 
                    {
                        bitmap.SetPixel(px, py, color);
                    }
                }
            });

            g.DrawImage(bitmap, 0, 0); // Draw the bitmap on the form's graphics surface
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the filter for the Save File dialog
            dlgSaveFile.Filter = "Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg|GIF Image|*.gif|PNG Image|*.png|TIFF Image|*.tif;*.tiff";
            // Set the initial filter index to 4 (PNG)
            dlgSaveFile.FilterIndex = 4;
            if (dlgSaveFile.ShowDialog() == DialogResult.OK) // Display the Save File dialog
            {
                string filename = dlgSaveFile.FileName;
                string extension = filename[filename.LastIndexOf('.')..];
                ImageFormat imageFormat = extension switch // Determine the appropriate ImageFormat based on the file extension
                {
                    ".bmp" => ImageFormat.Bmp,
                    ".jpg" or ".jpeg" => ImageFormat.Jpeg,
                    ".gif" => ImageFormat.Gif,
                    ".png" => ImageFormat.Png,
                    ".tif" or ".tiff" => ImageFormat.Tiff,
                    _ => ImageFormat.Png,
                };
                bitmap.Save(filename, imageFormat); // Save the bitmap to the selected file using the determined ImageFormat
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

        private void OptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using Options settingsForm = new(MaxIterations);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                MaxIterations = settingsForm.MaxIterations;
                this.Invalidate(); // Redraw with new settings
            }
        }
    }
}
