using System.Drawing.Drawing2D;

namespace Fractal_Generator
{
    public partial class Options : Form
    {
        public int MaxIterations { get; private set; }
        public double Exponent { get; private set; }
        public double JuliaReal { get; private set; }
        public double JuliaImaginary { get; private set; }
        public double Gamma { get; private set; }
        public double Tolerance { get; private set; }
        public double Start { get; private set; }
        public double Relaxation { get; private set; }
        public double Lambda { get; private set; }
        public Options(int currentMaxIterations)
        {
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            tbIteration.Text = MaxIterations.ToString();
        }
        public Options(int currentMaxIterations, int currentExponent)
        {
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Exponent = currentExponent;
            tbIteration.Text = MaxIterations.ToString();
            tbExponent.Text = Exponent.ToString();
        }
        public Options(int currentMaxIterations, double currentExponent)
        { // Bruning / Mandel / Multi
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Exponent = currentExponent;
            tbIteration.Text = MaxIterations.ToString();
            tbExponent.Text = Exponent.ToString();
        }
        public Options(int currentMaxIterations, double currentExponent, double juliaReal, double juliaImaginary)
        {// Poly / Burning Julia
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Exponent = currentExponent;
            JuliaReal = juliaReal;
            JuliaImaginary = juliaImaginary;
            tbIteration.Text = MaxIterations.ToString();
            tbExponent.Text = Exponent.ToString();
            tbJuliaReal.Text = JuliaReal.ToString();
            tbJuliaImagined.Text = JuliaImaginary.ToString();
        }
        public Options(int currentExponent, double tolerance, double relaxation, int currentMaxIterations)
        {// Newtonian
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Exponent = currentExponent;
            Relaxation = relaxation;
            Tolerance = tolerance;
            tbIteration.Text = MaxIterations.ToString();
            tbExponent.Text = Exponent.ToString();
            tbRelaxation.Text = Relaxation.ToString();
            tbTolerance.Text = Tolerance.ToString();
        }
        public Options(int currentMaxIterations, double gamma, double tolerance)
        {// Nova
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Gamma = gamma;
            Tolerance = tolerance;
            tbIteration.Text = MaxIterations.ToString();
            tbGamma.Text = Gamma.ToString();
            tbTolerance.Text = Tolerance.ToString();
        }
        public Options(double juliaReal, double juliaImaginary, int currentMaxIterations)
        {// Quadratic
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            JuliaReal = juliaReal;
            JuliaImaginary = juliaImaginary;
            tbIteration.Text = MaxIterations.ToString();
            tbJuliaReal.Text = JuliaReal.ToString();
            tbJuliaImagined.Text = JuliaImaginary.ToString();
        }
        public Options(int currentMaxIterations, double start, double relaxation, double juliaReal, double juliaImaginary)
        {// Phoenix
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Start = start;
            Relaxation = relaxation;
            JuliaReal = juliaReal;
            JuliaImaginary = juliaImaginary;
            tbIteration.Text = MaxIterations.ToString();
            tbStart.Text = Start.ToString();
            tbRelaxation.Text = Relaxation.ToString();
            tbJuliaReal.Text = JuliaReal.ToString();
            tbJuliaImagined.Text = JuliaImaginary.ToString();
        }
        public Options(int currentMaxIterations, double gamma, double tolerance, double juliaReal, double juliaImaginary, double lambda)
        {// Rational
            InitializeComponent();
            MaxIterations = currentMaxIterations;
            Gamma = gamma;
            Tolerance = tolerance;
            JuliaReal = juliaReal;
            JuliaImaginary = juliaImaginary;
            Lambda = lambda;
            tbIteration.Text = MaxIterations.ToString();
            tbGamma.Text = Gamma.ToString();
            tbTolerance.Text = Tolerance.ToString();
            tbJuliaReal.Text = JuliaReal.ToString();
            tbJuliaImagined.Text = JuliaImaginary.ToString();
            tbLambda.Text = Lambda.ToString();
        }

        // Fills the background of the form with a two-tone gradient
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using LinearGradientBrush brush = new(this.ClientRectangle, Color.LightSkyBlue, Color.SteelBlue, 90F);
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
        private void BtnIteration_Click(object sender, EventArgs e)
        {
            bool valid = true;

            int newMaxIterations = MaxIterations;
            double newExponent = Exponent;
            double newJuliaReal = JuliaReal;
            double newJuliaImaginary = JuliaImaginary;
            double newGamma = Gamma;
            double newTolerance = Tolerance;
            double newStart = Start;
            double newRelaxation = Relaxation;
            double newLambda = Lambda;

            if (tbIteration.Visible && !int.TryParse(tbIteration.Text, out newMaxIterations))
            {
                MessageBox.Show("Please enter a valid integer value for Max Iterations.");
                valid = false;
            }

            if (tbExponent.Visible && !double.TryParse(tbExponent.Text, out newExponent))
            {
                MessageBox.Show("Please enter a valid double value for Exponent.");
                valid = false;
            }

            if (tbJuliaReal.Visible && !double.TryParse(tbJuliaReal.Text, out newJuliaReal))
            {
                MessageBox.Show("Please enter a valid double value for Julia Real.");
                valid = false;
            }

            if (tbJuliaImagined.Visible && !double.TryParse(tbJuliaImagined.Text, out newJuliaImaginary))
            {
                MessageBox.Show("Please enter a valid double value for Julia Imaginary.");
                valid = false;
            }

            if (tbGamma.Visible && !double.TryParse(tbGamma.Text, out newGamma))
            {
                MessageBox.Show("Please enter a valid double value for Gamma.");
                valid = false;
            }

            if (tbTolerance.Visible && !double.TryParse(tbTolerance.Text, out newTolerance))
            {
                MessageBox.Show("Please enter a valid double value for Tolerance.");
                valid = false;
            }

            if (tbStart.Visible && !double.TryParse(tbStart.Text, out newStart))
            {
                MessageBox.Show("Please enter a valid double value for Start.");
                valid = false;
            }

            if (tbRelaxation.Visible && !double.TryParse(tbRelaxation.Text, out newRelaxation))
            {
                MessageBox.Show("Please enter a valid double value for Relaxation.");
                valid = false;
            }

            if (tbLambda.Visible && !double.TryParse(tbLambda.Text, out newLambda))
            {
                MessageBox.Show("Please enter a valid double value for Lambda.");
                valid = false;
            }

            if (valid)
            {
                if (tbIteration.Visible) MaxIterations = newMaxIterations;
                if (tbExponent.Visible) Exponent = newExponent;
                if (tbJuliaReal.Visible) JuliaReal = newJuliaReal;
                if (tbJuliaImagined.Visible) JuliaImaginary = newJuliaImaginary;
                if (tbGamma.Visible) Gamma = newGamma;
                if (tbTolerance.Visible) Tolerance = newTolerance;
                if (tbStart.Visible) Start = newStart;
                if (tbRelaxation.Visible) Relaxation = newRelaxation;
                if (tbLambda.Visible) Lambda = newLambda;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
