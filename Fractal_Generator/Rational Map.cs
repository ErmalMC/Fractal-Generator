﻿using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Fractal_Generator
{
    public partial class Rational_Map : Form
    {
        private int MaxIterations = 100;
        private double Tolerance = 0.0001;
        private double Gamma = 0.5; // Perturbation factor
        private double XMin = -2.5, XMax = 2.5, YMin = -2.5, YMax = 2.5; // Fractal bounds
        private double JuliaReal = 0.4, JuliaImaginary = 0.2; // Julia constants
        private double Lambda = 0.5; // Lambda constant
        private Bitmap? bitmap;
        private readonly List<Color> colorPalette = [Color.Black, Color.Red, Color.Green, Color.Yellow];
        private readonly int MaxColors = 4; // Maximum number of colors allowed in the palette
        public Rational_Map()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Rational_Map_Paint); // Add an event handler for the Paint event of the form
            this.Resize += new EventHandler(Form1_Resize); // Add an event handler for the Resize event of the form
            UpdateBounds();
            this.DoubleBuffered = true; // Enable double buffering for smoother rendering
        }

        private void Rational_Map_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Clear the previous drawing
            DrawRationalMapFractal(g, this.ClientSize.Width, this.ClientSize.Height);
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

        private void DrawRationalMapFractal(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb); // Lock the bitmap's bits and get the bitmap data
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * height;
            byte[] pixels = new byte[byteCount];

            double xScale = (XMax - XMin) / width;
            double yScale = (YMax - YMin) / height;

            Parallel.For(0, height, py => // Use Parallel.For to iterate over the pixels in the bitmap
            {
                int rowStart = py * bmpData.Stride;
                double y0 = YMin + yScale * py;
                for (int px = 0; px < width; px++)
                {
                    double x0 = XMin + xScale * px;
                    double zx = x0;
                    double zy = y0;
                    int iteration = 0;

                    while (iteration < MaxIterations)
                    {
                        // Rational Map iteration
                        double zx2 = zx * zx;
                        double zy2 = zy * zy;
                        double zxJulia = zx - JuliaReal;
                        double zyJulia = zy - JuliaImaginary;
                        double denom = zx2 + zy2 + Lambda;
                        double zxn = zx - Gamma * (zxJulia * zx2 - zyJulia * zy2) / denom;
                        double zyn = zy - Gamma * (zyJulia * zx2 + zxJulia * zy2) / denom;

                        if ((zxn - zx) * (zxn - zx) + (zyn - zy) * (zyn - zy) < Tolerance * Tolerance) // Check if the iteration has converged
                        {
                            break;
                        }
                        zx = zxn;
                        zy = zyn;
                        iteration++;
                    }

                    Color color = GetColor(iteration);
                    int pixelIndex = rowStart + px * bytesPerPixel;
                    pixels[pixelIndex] = color.B;
                    pixels[pixelIndex + 1] = color.G;
                    pixels[pixelIndex + 2] = color.R;
                }
            });
            // Copy the pixel array back to the bitmap and unlock the bitmap data
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length);
            bitmap.UnlockBits(bmpData);
            g.DrawImage(bitmap, 0, 0);
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

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using Options settingsForm = new(MaxIterations, Gamma, Tolerance, JuliaReal, JuliaImaginary, Lambda);
            settingsForm.tbGamma.Visible = true;
            settingsForm.tbTolerance.Visible = true;
            settingsForm.tbJuliaReal.Visible = true;
            settingsForm.tbJuliaImagined.Visible = true;
            settingsForm.tbLambda.Visible = true;
            settingsForm.lblGamma.Visible = true;
            settingsForm.lblTolerance.Visible = true;
            settingsForm.lblJuliaReal.Visible = true;
            settingsForm.lblJuliaImagined.Visible = true;
            settingsForm.lblLambda.Visible = true;
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                Gamma = settingsForm.Gamma;
                Tolerance = settingsForm.Tolerance;
                JuliaReal = settingsForm.JuliaReal;
                JuliaImaginary = settingsForm.JuliaImaginary;
                Lambda = settingsForm.Lambda;
                MaxIterations = settingsForm.MaxIterations;
                this.Invalidate(); // Redraw with new settings
            }
        }
    }


}

