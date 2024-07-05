using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal_Generator
{
    public partial class Burning_Ship_Julia_Set : Form
    {
        private int MaxIterations = 100;
        private double XMin = -2.5, XMax = 2.5, YMin = -2.5, YMax = 2.5;
        private Bitmap bitmap;
        private double juliaReal = -0.6;
        private double juliaImaginary = 0.6;
        private double exponent = 2.0; // Default exponent
        private List<Color> colorPalette = new List<Color> { Color.Black, Color.Red, Color.Green, Color.Yellow };
        private int MaxColors = 4; // Maximum number of colors allowed in the palette
        public Burning_Ship_Julia_Set()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Burning_Ship_Julia_Set_Paint);
            this.Resize += new EventHandler(Form1_Resize);
        }

        private void Burning_Ship_Julia_Set_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Clear the previous drawing
            DrawBurningShipJuliaSet(g, this.ClientSize.Width, this.ClientSize.Height);
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
        private void DrawBurningShipJuliaSet(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * height;
            byte[] pixels = new byte[byteCount];

            double xScale = (XMax - XMin) / width;
            double yScale = (YMax - YMin) / height;

            Parallel.For(0, height, py =>
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
                        // Burning Ship iteration with exponent
                        double zx2 = zx * zx - zy * zy;
                        double zy2 = 2 * Math.Abs(zx * zy);
                        zx = Math.Pow(Math.Abs(zx2), exponent) + juliaReal;
                        zy = Math.Pow(Math.Abs(zy2), exponent) + juliaImaginary;

                        if (zx * zx + zy * zy > 4)
                        {
                            break;
                        }
                        iteration++;
                    }

                    Color color = GetColor(iteration);
                    int pixelIndex = rowStart + px * bytesPerPixel;
                    pixels[pixelIndex] = color.B;
                    pixels[pixelIndex + 1] = color.G;
                    pixels[pixelIndex + 2] = color.R;
                }
            });

            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length);
            bitmap.UnlockBits(bmpData);
            g.DrawImage(bitmap, 0, 0);
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
                    colorPalette.Add(Color.Black);
                    colorPalette.Add(Color.Red);
                    colorPalette.Add(Color.Green);
                    colorPalette.Add(Color.Yellow);
                }

                this.Invalidate(); // Force the form to redraw itself
            }
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Options settingsForm = new Options(MaxIterations, exponent, juliaReal,  juliaImaginary))
            {
                settingsForm.tbJuliaImagined.Visible = true;
                settingsForm.tbJuliaReal.Visible = true;
                settingsForm.lblJuliaImagined.Visible=true;
                settingsForm.lblJuliaReal.Visible=true;
                settingsForm.tbExponent.Visible = true;
                settingsForm.lblExponent.Visible = true;
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    juliaReal = settingsForm.JuliaReal;
                    juliaImaginary = settingsForm.JuliaImaginary;
                    exponent = settingsForm.Exponent;
                    MaxIterations = settingsForm.MaxIterations;
                    this.Invalidate(); // Redraw with new settings
                }
            }
        }

        
    }
}
