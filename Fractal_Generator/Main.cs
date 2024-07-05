namespace Fractal_Generator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Mandlelbrot_Set_Click(object sender, EventArgs e)
        {
            var myForm= new Mandelbrot_Set();
            myForm.Show();
        }

        private void Quadratic_Julia_Set_Click(object sender, EventArgs e)
        {
            var myForm = new Quadratic_Julia_Set();
            myForm.Show();
        }

        private void Multibrot_Set_Click(object sender, EventArgs e)
        {
            var myForm = new Multibrot_Set();
            myForm.Show();
        }

        private void Polynomial_Julia_Set_Click(object sender, EventArgs e)
        {
            var myForm = new Polynomial_Julia_Set();
            myForm.Show();
        }

        private void Rational_Map_Click(object sender, EventArgs e)
        {
            var myForm = new Rational_Map();
            myForm.Show();
        }

        private void Newtonian_Fractal_Click(object sender, EventArgs e)
        {
            var myForm = new Newtonian_Fractal();
            myForm.Show();
        }

        private void Nova_Click(object sender, EventArgs e)
        {
            var myForm = new Nova();
            myForm.Show();
        }

        private void Phoenix_Fractal_Click(object sender, EventArgs e)
        {
            var myForm = new Phoenix_Fractal();
            myForm.Show();
        }

        private void Burning_Ship_Fractal_Click(object sender, EventArgs e)
        {
            var myForm = new Burning_Ship_Fractal();
            myForm.Show();
        }

        private void Burning_Ship_Julia_Set_Click(object sender, EventArgs e)
        {
            var myForm = new Burning_Ship_Julia_Set();
            myForm.Show();
        }
    }
}
