namespace Fractal_Generator
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblIteration = new Label();
            tbIteration = new TextBox();
            btnIteration = new Button();
            tbExponent = new TextBox();
            lblExponent = new Label();
            tbJuliaReal = new TextBox();
            lblJuliaReal = new Label();
            lblJuliaImagined = new Label();
            tbJuliaImagined = new TextBox();
            tbStart = new TextBox();
            tbTolerance = new TextBox();
            tbGamma = new TextBox();
            tbRelaxation = new TextBox();
            lblTolerance = new Label();
            lblGamma = new Label();
            lblStart = new Label();
            lblRelaxation = new Label();
            tbLambda = new TextBox();
            lblLambda = new Label();
            SuspendLayout();
            // 
            // lblIteration
            // 
            lblIteration.AutoSize = true;
            lblIteration.Font = new Font("Segoe UI", 12F);
            lblIteration.Location = new Point(45, 25);
            lblIteration.Name = "lblIteration";
            lblIteration.Size = new Size(108, 21);
            lblIteration.TabIndex = 0;
            lblIteration.Text = "Max Iterations";
            // 
            // tbIteration
            // 
            tbIteration.Location = new Point(45, 52);
            tbIteration.Name = "tbIteration";
            tbIteration.Size = new Size(100, 23);
            tbIteration.TabIndex = 1;
            // 
            // btnIteration
            // 
            btnIteration.Location = new Point(249, 341);
            btnIteration.Name = "btnIteration";
            btnIteration.Size = new Size(255, 63);
            btnIteration.TabIndex = 2;
            btnIteration.Text = "Confirm";
            btnIteration.UseVisualStyleBackColor = true;
            btnIteration.Click += btnIteration_Click;
            // 
            // tbExponent
            // 
            tbExponent.Location = new Point(45, 138);
            tbExponent.Name = "tbExponent";
            tbExponent.Size = new Size(100, 23);
            tbExponent.TabIndex = 4;
            tbExponent.Visible = false;
            // 
            // lblExponent
            // 
            lblExponent.AutoSize = true;
            lblExponent.Font = new Font("Segoe UI", 12F);
            lblExponent.Location = new Point(45, 114);
            lblExponent.Name = "lblExponent";
            lblExponent.Size = new Size(74, 21);
            lblExponent.TabIndex = 5;
            lblExponent.Text = "Exponent";
            lblExponent.Visible = false;
            // 
            // tbJuliaReal
            // 
            tbJuliaReal.Location = new Point(215, 52);
            tbJuliaReal.Name = "tbJuliaReal";
            tbJuliaReal.Size = new Size(100, 23);
            tbJuliaReal.TabIndex = 8;
            tbJuliaReal.Visible = false;
            // 
            // lblJuliaReal
            // 
            lblJuliaReal.AutoSize = true;
            lblJuliaReal.Font = new Font("Segoe UI", 12F);
            lblJuliaReal.Location = new Point(215, 25);
            lblJuliaReal.Name = "lblJuliaReal";
            lblJuliaReal.Size = new Size(75, 21);
            lblJuliaReal.TabIndex = 9;
            lblJuliaReal.Text = "Julia Real";
            lblJuliaReal.Visible = false;
            // 
            // lblJuliaImagined
            // 
            lblJuliaImagined.AutoSize = true;
            lblJuliaImagined.Font = new Font("Segoe UI", 12F);
            lblJuliaImagined.Location = new Point(215, 114);
            lblJuliaImagined.Name = "lblJuliaImagined";
            lblJuliaImagined.Size = new Size(115, 21);
            lblJuliaImagined.TabIndex = 12;
            lblJuliaImagined.Text = "Julia Imaginary";
            lblJuliaImagined.Visible = false;
            // 
            // tbJuliaImagined
            // 
            tbJuliaImagined.Location = new Point(215, 138);
            tbJuliaImagined.Name = "tbJuliaImagined";
            tbJuliaImagined.Size = new Size(100, 23);
            tbJuliaImagined.TabIndex = 11;
            tbJuliaImagined.Visible = false;
            // 
            // tbStart
            // 
            tbStart.Location = new Point(404, 138);
            tbStart.Name = "tbStart";
            tbStart.Size = new Size(100, 23);
            tbStart.TabIndex = 13;
            tbStart.Visible = false;
            // 
            // tbTolerance
            // 
            tbTolerance.Location = new Point(551, 52);
            tbTolerance.Name = "tbTolerance";
            tbTolerance.Size = new Size(100, 23);
            tbTolerance.TabIndex = 14;
            tbTolerance.Visible = false;
            // 
            // tbGamma
            // 
            tbGamma.Location = new Point(404, 52);
            tbGamma.Name = "tbGamma";
            tbGamma.Size = new Size(100, 23);
            tbGamma.TabIndex = 15;
            tbGamma.Visible = false;
            // 
            // tbRelaxation
            // 
            tbRelaxation.Location = new Point(551, 138);
            tbRelaxation.Name = "tbRelaxation";
            tbRelaxation.Size = new Size(100, 23);
            tbRelaxation.TabIndex = 16;
            tbRelaxation.Visible = false;
            // 
            // lblTolerance
            // 
            lblTolerance.AutoSize = true;
            lblTolerance.Font = new Font("Segoe UI", 12F);
            lblTolerance.Location = new Point(551, 28);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(75, 21);
            lblTolerance.TabIndex = 17;
            lblTolerance.Text = "Tolerance";
            lblTolerance.Visible = false;
            // 
            // lblGamma
            // 
            lblGamma.AutoSize = true;
            lblGamma.Font = new Font("Segoe UI", 12F);
            lblGamma.Location = new Point(404, 28);
            lblGamma.Name = "lblGamma";
            lblGamma.Size = new Size(65, 21);
            lblGamma.TabIndex = 18;
            lblGamma.Text = "Gamma";
            lblGamma.Visible = false;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Font = new Font("Segoe UI", 12F);
            lblStart.Location = new Point(404, 114);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(84, 21);
            lblStart.TabIndex = 19;
            lblStart.Text = "Start Value";
            lblStart.Visible = false;
            // 
            // lblRelaxation
            // 
            lblRelaxation.AutoSize = true;
            lblRelaxation.Font = new Font("Segoe UI", 12F);
            lblRelaxation.Location = new Point(551, 114);
            lblRelaxation.Name = "lblRelaxation";
            lblRelaxation.Size = new Size(82, 21);
            lblRelaxation.TabIndex = 20;
            lblRelaxation.Text = "Relaxation";
            lblRelaxation.Visible = false;
            // 
            // tbLambda
            // 
            tbLambda.Location = new Point(688, 52);
            tbLambda.Name = "tbLambda";
            tbLambda.Size = new Size(100, 23);
            tbLambda.TabIndex = 21;
            tbLambda.Visible = false;
            // 
            // lblLambda
            // 
            lblLambda.AutoSize = true;
            lblLambda.Font = new Font("Segoe UI", 12F);
            lblLambda.Location = new Point(688, 28);
            lblLambda.Name = "lblLambda";
            lblLambda.Size = new Size(66, 21);
            lblLambda.TabIndex = 22;
            lblLambda.Text = "Lambda";
            lblLambda.Visible = false;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblLambda);
            Controls.Add(tbLambda);
            Controls.Add(lblRelaxation);
            Controls.Add(lblStart);
            Controls.Add(lblGamma);
            Controls.Add(lblTolerance);
            Controls.Add(tbRelaxation);
            Controls.Add(tbGamma);
            Controls.Add(tbTolerance);
            Controls.Add(tbStart);
            Controls.Add(lblJuliaImagined);
            Controls.Add(tbJuliaImagined);
            Controls.Add(lblJuliaReal);
            Controls.Add(tbJuliaReal);
            Controls.Add(lblExponent);
            Controls.Add(tbExponent);
            Controls.Add(btnIteration);
            Controls.Add(tbIteration);
            Controls.Add(lblIteration);
            Name = "Options";
            Text = "Options";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIteration;
        private TextBox tbIteration;
        private Button btnIteration;
        public TextBox tbExponent;
        public Label lblExponent;
        public TextBox tbJuliaReal;
        public Label lblJuliaReal;
        public Label lblJuliaImagined;
        public TextBox tbJuliaImagined;
        public TextBox tbStart;
        public TextBox tbTolerance;
        public TextBox tbGamma;
        public TextBox tbRelaxation;
        public Label lblTolerance;
        public Label lblGamma;
        public Label lblStart;
        public Label lblRelaxation;
        public TextBox tbLambda;
        public Label lblLambda;
    }
}