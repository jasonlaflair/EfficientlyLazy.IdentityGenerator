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
            this.lbExcluded = new System.Windows.Forms.ListBox();
            this.lbIncluded = new System.Windows.Forms.ListBox();
            this.cmdIncludeAll = new System.Windows.Forms.Button();
            this.cmdExcludeAll = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.cmdInclude = new System.Windows.Forms.Button();
            this.cmdExclude = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.nudRecords = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // lbExcluded
            // 
            this.lbExcluded.FormattingEnabled = true;
            this.lbExcluded.Location = new System.Drawing.Point(12, 12);
            this.lbExcluded.Name = "lbExcluded";
            this.lbExcluded.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbExcluded.Size = new System.Drawing.Size(189, 225);
            this.lbExcluded.TabIndex = 0;
            this.lbExcluded.DoubleClick += new System.EventHandler(this.lbExcluded_DoubleClick);
            // 
            // lbIncluded
            // 
            this.lbIncluded.FormattingEnabled = true;
            this.lbIncluded.Location = new System.Drawing.Point(248, 12);
            this.lbIncluded.Name = "lbIncluded";
            this.lbIncluded.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbIncluded.Size = new System.Drawing.Size(189, 225);
            this.lbIncluded.TabIndex = 0;
            this.lbIncluded.DoubleClick += new System.EventHandler(this.lbIncluded_DoubleClick);
            // 
            // cmdIncludeAll
            // 
            this.cmdIncludeAll.Location = new System.Drawing.Point(207, 65);
            this.cmdIncludeAll.Name = "cmdIncludeAll";
            this.cmdIncludeAll.Size = new System.Drawing.Size(35, 23);
            this.cmdIncludeAll.TabIndex = 1;
            this.cmdIncludeAll.Text = "> >";
            this.cmdIncludeAll.UseVisualStyleBackColor = true;
            this.cmdIncludeAll.Click += new System.EventHandler(this.cmdIncludeAll_Click);
            // 
            // cmdExcludeAll
            // 
            this.cmdExcludeAll.Location = new System.Drawing.Point(207, 152);
            this.cmdExcludeAll.Name = "cmdExcludeAll";
            this.cmdExcludeAll.Size = new System.Drawing.Size(35, 23);
            this.cmdExcludeAll.TabIndex = 1;
            this.cmdExcludeAll.Text = "< <";
            this.cmdExcludeAll.UseVisualStyleBackColor = true;
            this.cmdExcludeAll.Click += new System.EventHandler(this.cmdExcludeAll_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(443, 12);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(51, 23);
            this.cmdUp.TabIndex = 2;
            this.cmdUp.Text = "Up";
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(443, 41);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(51, 23);
            this.cmdDown.TabIndex = 2;
            this.cmdDown.Text = "Down";
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // cmdInclude
            // 
            this.cmdInclude.Location = new System.Drawing.Point(207, 94);
            this.cmdInclude.Name = "cmdInclude";
            this.cmdInclude.Size = new System.Drawing.Size(35, 23);
            this.cmdInclude.TabIndex = 1;
            this.cmdInclude.Text = ">";
            this.cmdInclude.UseVisualStyleBackColor = true;
            this.cmdInclude.Click += new System.EventHandler(this.cmdInclude_Click);
            // 
            // cmdExclude
            // 
            this.cmdExclude.Location = new System.Drawing.Point(207, 123);
            this.cmdExclude.Name = "cmdExclude";
            this.cmdExclude.Size = new System.Drawing.Size(35, 23);
            this.cmdExclude.TabIndex = 1;
            this.cmdExclude.Text = "<";
            this.cmdExclude.UseVisualStyleBackColor = true;
            this.cmdExclude.Click += new System.EventHandler(this.cmdExclude_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(362, 263);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // nudRecords
            // 
            this.nudRecords.Location = new System.Drawing.Point(12, 266);
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
            this.nudRecords.Size = new System.Drawing.Size(98, 20);
            this.nudRecords.TabIndex = 4;
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
            this.label1.Location = new System.Drawing.Point(9, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Records:";
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Location = new System.Drawing.Point(281, 263);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(75, 23);
            this.cmdGenerate.TabIndex = 3;
            this.cmdGenerate.Text = "Generate";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Location = new System.Drawing.Point(131, 266);
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size(70, 20);
            this.txtDelimiter.TabIndex = 6;
            this.txtDelimiter.Text = "|";
            this.txtDelimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(128, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Delimiter:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 295);
            this.Controls.Add(this.txtDelimiter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudRecords);
            this.Controls.Add(this.cmdGenerate);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdDown);
            this.Controls.Add(this.cmdUp);
            this.Controls.Add(this.cmdExclude);
            this.Controls.Add(this.cmdExcludeAll);
            this.Controls.Add(this.cmdInclude);
            this.Controls.Add(this.cmdIncludeAll);
            this.Controls.Add(this.lbIncluded);
            this.Controls.Add(this.lbExcluded);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Identity Generator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbExcluded;
        private System.Windows.Forms.ListBox lbIncluded;
        private System.Windows.Forms.Button cmdIncludeAll;
        private System.Windows.Forms.Button cmdExcludeAll;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.Button cmdInclude;
        private System.Windows.Forms.Button cmdExclude;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.NumericUpDown nudRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.Label label2;
    }
}

