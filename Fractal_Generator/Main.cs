using System.Drawing.Drawing2D;

namespace Fractal_Generator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // Fills the background of the form with a two-tone gradient
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using LinearGradientBrush brush = new(this.ClientRectangle, Color.LightSkyBlue, Color.SteelBlue, 90F);
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Invalidate and redraw the form as the background gradient doesn't update without it
        }
        private void Mandlelbrot_Set_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Mandelbrot_Set" form
            var myForm = new Mandelbrot_Set();
            // Display the newly created form
            myForm.Show();
        }

        private void Quadratic_Julia_Set_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Quadratic_Julia_Set" form
            var myForm = new Quadratic_Julia_Set();
            // Display the newly created form
            myForm.Show();
        }

        private void Multibrot_Set_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Multibrot_Set" form
            var myForm = new Multibrot_Set();
            // Display the newly created form
            myForm.Show();
        }

        private void Polynomial_Julia_Set_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Polynomial_Julia_Set" form
            var myForm = new Polynomial_Julia_Set();
            // Display the newly created form
            myForm.Show();
        }

        private void Rational_Map_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Rational_Map" form
            var myForm = new Rational_Map();
            // Display the newly created form
            myForm.Show();
        }

        private void Newtonian_Fractal_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Newtonian_Fractal" form
            var myForm = new Newtonian_Fractal();
            // Display the newly created form
            myForm.Show();
        }

        private void Nova_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Nova" form
            var myForm = new Nova();
            // Display the newly created form
            myForm.Show();
        }

        private void Phoenix_Fractal_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Phoenix_Fractal" form
            var myForm = new Phoenix_Fractal();
            // Display the newly created form
            myForm.Show();
        }

        private void Burning_Ship_Fractal_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Burning_Ship_Fractal" form
            var myForm = new Burning_Ship_Fractal();
            // Display the newly created form
            myForm.Show();
        }
        private void Burning_Ship_Julia_Set_Click(object sender, EventArgs e)
        {
            // Create a new instance of the "Burning_Ship_Julia_Set" form
            var myForm = new Burning_Ship_Julia_Set();

            // Display the newly created form
            myForm.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
