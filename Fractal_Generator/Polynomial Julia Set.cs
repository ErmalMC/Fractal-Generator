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
    public partial class Polynomial_Julia_Set : Form
    {
        private int MaxIterations = 100;
        private double XMin = -2.0, XMax = 2.0, YMin = -2.0, YMax = 2.0;
        private Bitmap bitmap;
        private double JuliaReal = 0.4, JuliaImaginary = 0.2; // Julia constants
        private int PolynomialExponent = 2; // Polynomial exponent (adjust as needed)
        private List<Color> colorPalette = new List<Color> { Color.Black, Color.Red, Color.Green, Color.Yellow };
        private int MaxColors = 4; // Maximum number of colors allowed in the palette
        public Polynomial_Julia_Set()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Paint += new PaintEventHandler(Polynomial_Julia_Set_Paint);
            this.Resize += new EventHandler(Form1_Resize);
        }
        private void Polynomial_Julia_Set_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Clear the previous drawing
            DrawPolynomialJuliaSet(g, this.ClientSize.Width, this.ClientSize.Height);
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

        private void DrawPolynomialJuliaSet(Graphics g, int width, int height)
        {
            bitmap = new Bitmap(width, height);

            double dx = (XMax - XMin) / width;
            double dy = (YMax - YMin) / height;

            for (int px = 0; px < width; px++)
            {
                for (int py = 0; py < height; py++)
                {
                    double x0 = XMin + dx * px;
                    double y0 = YMin + dy * py;
                    Complex z = new Complex(x0, y0);
                    int iteration = 0;

                    while (z.MagnitudeSquared < 4 && iteration < MaxIterations)
                    {
                        z = z.Power(PolynomialExponent) + new Complex(JuliaReal, JuliaImaginary);
                        iteration++;
                    }

                    Color color = GetColor(iteration);
                    bitmap.SetPixel(px, py, color);
                }
            }

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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (Options settingsForm = new Options(MaxIterations, PolynomialExponent, JuliaReal, JuliaImaginary))
            {
                settingsForm.lblExponent.Visible = true;
                settingsForm.lblJuliaReal.Visible = true;
                settingsForm.lblJuliaImagined.Visible = true;
                settingsForm.tbExponent.Visible = true;
                settingsForm.tbJuliaReal.Visible = true;
                settingsForm.tbJuliaImagined.Visible=true;
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    PolynomialExponent = Convert.ToInt32(settingsForm.Exponent);
                    JuliaReal = settingsForm.JuliaReal;
                    JuliaImaginary = settingsForm.JuliaImaginary;
                    MaxIterations = settingsForm.MaxIterations;
                    this.Invalidate(); // Redraw with new settings
                }
            }
        }

        
        }
    }
