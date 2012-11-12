using System;
using System.ComponentModel;
using System.Windows.Forms;
using EfficientlyLazy.IdentityGenerator.Entity;

namespace EfficientlyLazy.IdentityGenerator.UI
{
    public partial class frmMain : Form
    {
        private readonly BackgroundWorker _worker;

        private class GenParams
        {
            public string Filename { get; set; }
            public string Delimiter { get; set; }
            public int Number { get; set; }

            public bool IncludeName { get; set; }
            public bool IncludeSSN { get; set; }
            public bool IncludeDOB { get; set; }
            public bool IncludeAddress { get; set; }
            public GenderFilter Genders { get; set; }

            public int MinimumAge { get; set; }
            public int MaximumAge { get; set; }
        }

        public frmMain()
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.DoWork += WorkerDoWork;
            _worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.Enabled = true;
            }

            UseWaitCursor = false;
            Refresh();
            Application.DoEvents();

            MessageBox.Show("Identities Generated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var gp = (GenParams)e.Argument;
            
            var generatorOptions = Generator.Configure();

            if (gp.IncludeName) generatorOptions.IncludeName(gp.Genders);
            if (gp.IncludeAddress) generatorOptions.IncludeAddress();
            if (gp.IncludeDOB) generatorOptions.IncludeDOB(gp.MinimumAge, gp.MaximumAge);
            if (gp.IncludeSSN) generatorOptions.IncludeSSN();

            var generator = generatorOptions.Build();

            generator.GenerateToFile(gp.Number, gp.Delimiter, gp.Filename);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            string filename;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (*.csv)|*.csv";

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                filename = sfd.FileName;
            }

            foreach (Control control in Controls)
            {
                control.Enabled = false;
            }

            UseWaitCursor = true;
            Refresh();
            Application.DoEvents();

            GenderFilter gf;

            if (rbGenderMale.Checked)
            {
                gf = GenderFilter.Male;
            }
            else if (rbGenderFemale.Checked)
            {
                gf = GenderFilter.Female;
            }
            else
            {
                gf = GenderFilter.Both;
            }

            _worker.RunWorkerAsync(new GenParams
                                       {
                                           Delimiter = txtDelimiter.Text,
                                           Filename = filename,
                                           Number = (int)nudRecords.Value,
                                           IncludeAddress = cbxIncludeAddress.Checked,
                                           IncludeDOB = cbxIncludeDOB.Checked,
                                           Genders = gf,
                                           IncludeSSN = cbxIncludeSSN.Checked,
                                           MaximumAge = (int)nudMaxAge.Value,
                                           MinimumAge = (int)nudMinAge.Value
                                       });
        }

        private void cbxIncludeDOB_CheckedChanged(object sender, EventArgs e)
        {
            gbAgeRange.Enabled = cbxIncludeDOB.Checked;
        }
    }
}
