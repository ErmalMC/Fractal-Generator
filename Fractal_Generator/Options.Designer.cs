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
            lblIteration.BackColor = Color.Transparent;
            lblIteration.Font = new Font("Segoe UI", 12F);
            lblIteration.Location = new Point(51, 33);
            lblIteration.Name = "lblIteration";
            lblIteration.Size = new Size(136, 28);
            lblIteration.TabIndex = 0;
            lblIteration.Text = "Max Iterations";
            // 
            // tbIteration
            // 
            tbIteration.BackColor = SystemColors.GradientActiveCaption;
            tbIteration.BorderStyle = BorderStyle.None;
            tbIteration.Location = new Point(51, 69);
            tbIteration.Margin = new Padding(3, 4, 3, 4);
            tbIteration.Name = "tbIteration";
            tbIteration.Size = new Size(114, 20);
            tbIteration.TabIndex = 1;
            // 
            // btnIteration
            // 
            btnIteration.BackColor = Color.SteelBlue;
            btnIteration.ForeColor = Color.SkyBlue;
            btnIteration.Location = new Point(285, 455);
            btnIteration.Margin = new Padding(3, 4, 3, 4);
            btnIteration.Name = "btnIteration";
            btnIteration.Size = new Size(291, 84);
            btnIteration.TabIndex = 2;
            btnIteration.Text = "Confirm";
            btnIteration.UseVisualStyleBackColor = false;
            btnIteration.Click += BtnIteration_Click;
            // 
            // tbExponent
            // 
            tbExponent.BackColor = SystemColors.GradientActiveCaption;
            tbExponent.BorderStyle = BorderStyle.None;
            tbExponent.Location = new Point(51, 184);
            tbExponent.Margin = new Padding(3, 4, 3, 4);
            tbExponent.Name = "tbExponent";
            tbExponent.Size = new Size(114, 20);
            tbExponent.TabIndex = 4;
            tbExponent.Visible = false;
            // 
            // lblExponent
            // 
            lblExponent.AutoSize = true;
            lblExponent.BackColor = Color.Transparent;
            lblExponent.Font = new Font("Segoe UI", 12F);
            lblExponent.Location = new Point(51, 152);
            lblExponent.Name = "lblExponent";
            lblExponent.Size = new Size(94, 28);
            lblExponent.TabIndex = 5;
            lblExponent.Text = "Exponent";
            lblExponent.Visible = false;
            // 
            // tbJuliaReal
            // 
            tbJuliaReal.BackColor = SystemColors.GradientActiveCaption;
            tbJuliaReal.BorderStyle = BorderStyle.None;
            tbJuliaReal.Location = new Point(246, 69);
            tbJuliaReal.Margin = new Padding(3, 4, 3, 4);
            tbJuliaReal.Name = "tbJuliaReal";
            tbJuliaReal.Size = new Size(114, 20);
            tbJuliaReal.TabIndex = 8;
            tbJuliaReal.Visible = false;
            // 
            // lblJuliaReal
            // 
            lblJuliaReal.AutoSize = true;
            lblJuliaReal.BackColor = Color.Transparent;
            lblJuliaReal.Font = new Font("Segoe UI", 12F);
            lblJuliaReal.Location = new Point(246, 33);
            lblJuliaReal.Name = "lblJuliaReal";
            lblJuliaReal.Size = new Size(91, 28);
            lblJuliaReal.TabIndex = 9;
            lblJuliaReal.Text = "Julia Real";
            lblJuliaReal.Visible = false;
            // 
            // lblJuliaImagined
            // 
            lblJuliaImagined.AutoSize = true;
            lblJuliaImagined.BackColor = Color.Transparent;
            lblJuliaImagined.Font = new Font("Segoe UI", 12F);
            lblJuliaImagined.Location = new Point(246, 152);
            lblJuliaImagined.Name = "lblJuliaImagined";
            lblJuliaImagined.Size = new Size(142, 28);
            lblJuliaImagined.TabIndex = 12;
            lblJuliaImagined.Text = "Julia Imaginary";
            lblJuliaImagined.Visible = false;
            // 
            // tbJuliaImagined
            // 
            tbJuliaImagined.BackColor = SystemColors.GradientActiveCaption;
            tbJuliaImagined.BorderStyle = BorderStyle.None;
            tbJuliaImagined.Location = new Point(246, 184);
            tbJuliaImagined.Margin = new Padding(3, 4, 3, 4);
            tbJuliaImagined.Name = "tbJuliaImagined";
            tbJuliaImagined.Size = new Size(114, 20);
            tbJuliaImagined.TabIndex = 11;
            tbJuliaImagined.Visible = false;
            // 
            // tbStart
            // 
            tbStart.BackColor = SystemColors.GradientActiveCaption;
            tbStart.BorderStyle = BorderStyle.None;
            tbStart.Location = new Point(462, 184);
            tbStart.Margin = new Padding(3, 4, 3, 4);
            tbStart.Name = "tbStart";
            tbStart.Size = new Size(114, 20);
            tbStart.TabIndex = 13;
            tbStart.Visible = false;
            // 
            // tbTolerance
            // 
            tbTolerance.BackColor = SystemColors.GradientActiveCaption;
            tbTolerance.BorderStyle = BorderStyle.None;
            tbTolerance.Location = new Point(630, 69);
            tbTolerance.Margin = new Padding(3, 4, 3, 4);
            tbTolerance.Name = "tbTolerance";
            tbTolerance.Size = new Size(114, 20);
            tbTolerance.TabIndex = 14;
            tbTolerance.Visible = false;
            // 
            // tbGamma
            // 
            tbGamma.BackColor = SystemColors.GradientActiveCaption;
            tbGamma.BorderStyle = BorderStyle.None;
            tbGamma.Location = new Point(462, 69);
            tbGamma.Margin = new Padding(3, 4, 3, 4);
            tbGamma.Name = "tbGamma";
            tbGamma.Size = new Size(114, 20);
            tbGamma.TabIndex = 15;
            tbGamma.Visible = false;
            // 
            // tbRelaxation
            // 
            tbRelaxation.BackColor = SystemColors.GradientActiveCaption;
            tbRelaxation.BorderStyle = BorderStyle.None;
            tbRelaxation.Location = new Point(630, 184);
            tbRelaxation.Margin = new Padding(3, 4, 3, 4);
            tbRelaxation.Name = "tbRelaxation";
            tbRelaxation.Size = new Size(114, 20);
            tbRelaxation.TabIndex = 16;
            tbRelaxation.Visible = false;
            // 
            // lblTolerance
            // 
            lblTolerance.AutoSize = true;
            lblTolerance.BackColor = Color.Transparent;
            lblTolerance.Font = new Font("Segoe UI", 12F);
            lblTolerance.Location = new Point(630, 37);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(94, 28);
            lblTolerance.TabIndex = 17;
            lblTolerance.Text = "Tolerance";
            lblTolerance.Visible = false;
            // 
            // lblGamma
            // 
            lblGamma.AutoSize = true;
            lblGamma.BackColor = Color.Transparent;
            lblGamma.Font = new Font("Segoe UI", 12F);
            lblGamma.Location = new Point(462, 37);
            lblGamma.Name = "lblGamma";
            lblGamma.Size = new Size(80, 28);
            lblGamma.TabIndex = 18;
            lblGamma.Text = "Gamma";
            lblGamma.Visible = false;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.BackColor = Color.Transparent;
            lblStart.Font = new Font("Segoe UI", 12F);
            lblStart.Location = new Point(462, 152);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(105, 28);
            lblStart.TabIndex = 19;
            lblStart.Text = "Start Value";
            lblStart.Visible = false;
            // 
            // lblRelaxation
            // 
            lblRelaxation.AutoSize = true;
            lblRelaxation.BackColor = Color.Transparent;
            lblRelaxation.Font = new Font("Segoe UI", 12F);
            lblRelaxation.Location = new Point(630, 152);
            lblRelaxation.Name = "lblRelaxation";
            lblRelaxation.Size = new Size(102, 28);
            lblRelaxation.TabIndex = 20;
            lblRelaxation.Text = "Relaxation";
            lblRelaxation.Visible = false;
            // 
            // tbLambda
            // 
            tbLambda.BackColor = SystemColors.GradientActiveCaption;
            tbLambda.BorderStyle = BorderStyle.None;
            tbLambda.Location = new Point(786, 69);
            tbLambda.Margin = new Padding(3, 4, 3, 4);
            tbLambda.Name = "tbLambda";
            tbLambda.Size = new Size(114, 20);
            tbLambda.TabIndex = 21;
            tbLambda.Visible = false;
            // 
            // lblLambda
            // 
            lblLambda.AutoSize = true;
            lblLambda.BackColor = Color.Transparent;
            lblLambda.Font = new Font("Segoe UI", 12F);
            lblLambda.Location = new Point(786, 37);
            lblLambda.Name = "lblLambda";
            lblLambda.Size = new Size(82, 28);
            lblLambda.TabIndex = 22;
            lblLambda.Text = "Lambda";
            lblLambda.Visible = false;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
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
            Margin = new Padding(3, 4, 3, 4);
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