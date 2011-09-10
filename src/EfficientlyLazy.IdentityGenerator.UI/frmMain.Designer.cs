namespace EfficientlyLazy.IdentityGenerator.UI
{
    partial class frmMain
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.nudRecords = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxIncludeDOB = new System.Windows.Forms.CheckBox();
            this.cbxIncludeSSN = new System.Windows.Forms.CheckBox();
            this.cbxIncludeAddress = new System.Windows.Forms.CheckBox();
            this.rbGenderBoth = new System.Windows.Forms.RadioButton();
            this.rbGenderFemale = new System.Windows.Forms.RadioButton();
            this.rbGenderMale = new System.Windows.Forms.RadioButton();
            this.gbGender = new System.Windows.Forms.GroupBox();
            this.gbAgeRange = new System.Windows.Forms.GroupBox();
            this.nudMinAge = new System.Windows.Forms.NumericUpDown();
            this.nudMaxAge = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecords)).BeginInit();
            this.gbGender.SuspendLayout();
            this.gbAgeRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAge)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(259, 309);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 28);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // nudRecords
            // 
            this.nudRecords.Location = new System.Drawing.Point(20, 47);
            this.nudRecords.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudRecords.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudRecords.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRecords.Name = "nudRecords";
            this.nudRecords.Size = new System.Drawing.Size(92, 22);
            this.nudRecords.TabIndex = 0;
            this.nudRecords.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Records:";
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Location = new System.Drawing.Point(20, 136);
            this.cmdGenerate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(92, 28);
            this.cmdGenerate.TabIndex = 2;
            this.cmdGenerate.Text = "Generate";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Location = new System.Drawing.Point(20, 100);
            this.txtDelimiter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size(92, 22);
            this.txtDelimiter.TabIndex = 1;
            this.txtDelimiter.Text = "|";
            this.txtDelimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Delimiter:";
            // 
            // cbxIncludeDOB
            // 
            this.cbxIncludeDOB.AutoSize = true;
            this.cbxIncludeDOB.Location = new System.Drawing.Point(20, 195);
            this.cbxIncludeDOB.Name = "cbxIncludeDOB";
            this.cbxIncludeDOB.Size = new System.Drawing.Size(158, 21);
            this.cbxIncludeDOB.TabIndex = 3;
            this.cbxIncludeDOB.Text = "Include Date of Birth";
            this.cbxIncludeDOB.UseVisualStyleBackColor = true;
            this.cbxIncludeDOB.CheckedChanged += new System.EventHandler(this.cbxIncludeDOB_CheckedChanged);
            // 
            // cbxIncludeSSN
            // 
            this.cbxIncludeSSN.AutoSize = true;
            this.cbxIncludeSSN.Location = new System.Drawing.Point(20, 28);
            this.cbxIncludeSSN.Name = "cbxIncludeSSN";
            this.cbxIncludeSSN.Size = new System.Drawing.Size(107, 21);
            this.cbxIncludeSSN.TabIndex = 0;
            this.cbxIncludeSSN.Text = "Include SSN";
            this.cbxIncludeSSN.UseVisualStyleBackColor = true;
            // 
            // cbxIncludeAddress
            // 
            this.cbxIncludeAddress.AutoSize = true;
            this.cbxIncludeAddress.Location = new System.Drawing.Point(20, 54);
            this.cbxIncludeAddress.Name = "cbxIncludeAddress";
            this.cbxIncludeAddress.Size = new System.Drawing.Size(131, 21);
            this.cbxIncludeAddress.TabIndex = 1;
            this.cbxIncludeAddress.Text = "Include Address";
            this.cbxIncludeAddress.UseVisualStyleBackColor = true;
            // 
            // rbGenderBoth
            // 
            this.rbGenderBoth.AutoSize = true;
            this.rbGenderBoth.Checked = true;
            this.rbGenderBoth.Location = new System.Drawing.Point(13, 25);
            this.rbGenderBoth.Name = "rbGenderBoth";
            this.rbGenderBoth.Size = new System.Drawing.Size(58, 21);
            this.rbGenderBoth.TabIndex = 0;
            this.rbGenderBoth.TabStop = true;
            this.rbGenderBoth.Text = "Both";
            this.rbGenderBoth.UseVisualStyleBackColor = true;
            // 
            // rbGenderFemale
            // 
            this.rbGenderFemale.AutoSize = true;
            this.rbGenderFemale.Location = new System.Drawing.Point(13, 52);
            this.rbGenderFemale.Name = "rbGenderFemale";
            this.rbGenderFemale.Size = new System.Drawing.Size(75, 21);
            this.rbGenderFemale.TabIndex = 1;
            this.rbGenderFemale.TabStop = true;
            this.rbGenderFemale.Text = "Female";
            this.rbGenderFemale.UseVisualStyleBackColor = true;
            // 
            // rbGenderMale
            // 
            this.rbGenderMale.AutoSize = true;
            this.rbGenderMale.Location = new System.Drawing.Point(13, 79);
            this.rbGenderMale.Name = "rbGenderMale";
            this.rbGenderMale.Size = new System.Drawing.Size(59, 21);
            this.rbGenderMale.TabIndex = 2;
            this.rbGenderMale.TabStop = true;
            this.rbGenderMale.Text = "Male";
            this.rbGenderMale.UseVisualStyleBackColor = true;
            // 
            // gbGender
            // 
            this.gbGender.Controls.Add(this.rbGenderBoth);
            this.gbGender.Controls.Add(this.rbGenderMale);
            this.gbGender.Controls.Add(this.rbGenderFemale);
            this.gbGender.Location = new System.Drawing.Point(20, 81);
            this.gbGender.Name = "gbGender";
            this.gbGender.Size = new System.Drawing.Size(158, 108);
            this.gbGender.TabIndex = 2;
            this.gbGender.TabStop = false;
            this.gbGender.Text = "Gender";
            // 
            // gbAgeRange
            // 
            this.gbAgeRange.Controls.Add(this.nudMaxAge);
            this.gbAgeRange.Controls.Add(this.nudMinAge);
            this.gbAgeRange.Controls.Add(this.label4);
            this.gbAgeRange.Controls.Add(this.label3);
            this.gbAgeRange.Enabled = false;
            this.gbAgeRange.Location = new System.Drawing.Point(20, 222);
            this.gbAgeRange.Name = "gbAgeRange";
            this.gbAgeRange.Size = new System.Drawing.Size(158, 94);
            this.gbAgeRange.TabIndex = 4;
            this.gbAgeRange.TabStop = false;
            this.gbAgeRange.Text = "Age Range";
            // 
            // nudMinAge
            // 
            this.nudMinAge.Location = new System.Drawing.Point(61, 29);
            this.nudMinAge.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudMinAge.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinAge.Name = "nudMinAge";
            this.nudMinAge.Size = new System.Drawing.Size(75, 22);
            this.nudMinAge.TabIndex = 0;
            this.nudMinAge.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMaxAge
            // 
            this.nudMaxAge.Location = new System.Drawing.Point(61, 57);
            this.nudMaxAge.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudMaxAge.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxAge.Name = "nudMaxAge";
            this.nudMaxAge.Size = new System.Drawing.Size(75, 22);
            this.nudMaxAge.TabIndex = 1;
            this.nudMaxAge.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Min:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Max:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbGender);
            this.groupBox1.Controls.Add(this.gbAgeRange);
            this.groupBox1.Controls.Add(this.cbxIncludeDOB);
            this.groupBox1.Controls.Add(this.cbxIncludeAddress);
            this.groupBox1.Controls.Add(this.cbxIncludeSSN);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 334);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Requirements";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nudRecords);
            this.groupBox2.Controls.Add(this.cmdGenerate);
            this.groupBox2.Controls.Add(this.txtDelimiter);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(227, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 181);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 359);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Identity Generator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRecords)).EndInit();
            this.gbGender.ResumeLayout(false);
            this.gbGender.PerformLayout();
            this.gbAgeRange.ResumeLayout(false);
            this.gbAgeRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAge)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.NumericUpDown nudRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxIncludeDOB;
        private System.Windows.Forms.CheckBox cbxIncludeSSN;
        private System.Windows.Forms.CheckBox cbxIncludeAddress;
        private System.Windows.Forms.RadioButton rbGenderBoth;
        private System.Windows.Forms.RadioButton rbGenderFemale;
        private System.Windows.Forms.RadioButton rbGenderMale;
        private System.Windows.Forms.GroupBox gbGender;
        private System.Windows.Forms.GroupBox gbAgeRange;
        private System.Windows.Forms.NumericUpDown nudMaxAge;
        private System.Windows.Forms.NumericUpDown nudMinAge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

