using System;
using System.ComponentModel;
using System.Windows.Forms;

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

            public bool IncludeSSN { get; set; }
            public bool IncludeDOB { get; set; }
            public bool IncludeAddress { get; set; }
            public bool IncludeMale { get; set; }
            public bool IncludeFemale { get; set; }

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
            var gp = (GenParams) e.Argument;

            var generator = Generator.SetOptions()
                .IncludeAddress(gp.IncludeAddress)
                .IncludeDOB(gp.IncludeDOB)
                .IncludeGenderMale(gp.IncludeMale)
                .IncludeGenderFemale(gp.IncludeFemale)
                .IncludeSSN(gp.IncludeSSN)
                .SetAgeRange(gp.MinimumAge, gp.MaximumAge)
                .CreateGenerator();

            generator.GenerateIdentities(gp.Number, gp.Delimiter, gp.Filename);
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

            _worker.RunWorkerAsync(new GenParams
                                       {
                                           Delimiter = txtDelimiter.Text,
                                           Filename = filename,
                                           Number = (int) nudRecords.Value,
                                           IncludeAddress = cbxIncludeAddress.Checked,
                                           IncludeDOB = cbxIncludeDOB.Checked,
                                           IncludeFemale = rbGenderBoth.Checked || rbGenderFemale.Checked,
                                           IncludeMale = rbGenderBoth.Checked || rbGenderMale.Checked,
                                           IncludeSSN = cbxIncludeSSN.Checked,
                                           MaximumAge = (int) nudMaxAge.Value,
                                           MinimumAge = (int) nudMinAge.Value
                                       });
        }

        private void cbxIncludeDOB_CheckedChanged(object sender, EventArgs e)
        {
            gbAgeRange.Enabled = cbxIncludeDOB.Checked;
        }
    }
}
