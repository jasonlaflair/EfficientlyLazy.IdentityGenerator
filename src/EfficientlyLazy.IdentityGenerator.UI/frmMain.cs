using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
            public List<PropertyInfo> PropertyInfos { get; set; } 
        }

        public frmMain()
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.DoWork += WorkerDoWork;
            _worker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            lbExcluded.DisplayMember = "Name";
            lbIncluded.DisplayMember = "Name";

            foreach (var pi in typeof(Identity).GetProperties())
            {
                lbExcluded.Items.Add(pi);
            }
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

            Generator.GenerateIdentities(gp.Number, gp.Delimiter, gp.Filename, gp.PropertyInfos);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void lbIncluded_DoubleClick(object sender, EventArgs e)
        {
            cmdExclude.PerformClick();
        }

        private void cmdIncludeAll_Click(object sender, EventArgs e)
        {
            var items = lbExcluded.Items.Cast<PropertyInfo>().ToList();

            foreach (var pi in items)
            {
                lbIncluded.Items.Add(pi);
                lbExcluded.Items.Remove(pi);
            }
        }

        private void cmdInclude_Click(object sender, EventArgs e)
        {
            var items = lbExcluded.SelectedItems.Cast<PropertyInfo>().ToList();

            foreach (var pi in items)
            {
                lbIncluded.Items.Add(pi);
                lbExcluded.Items.Remove(pi);
            }
        }

        private void lbExcluded_DoubleClick(object sender, EventArgs e)
        {
            cmdInclude.PerformClick();
        }

        private void cmdExcludeAll_Click(object sender, EventArgs e)
        {
            var items = lbIncluded.Items.Cast<PropertyInfo>().ToList();

            foreach (var pi in items)
            {
                lbExcluded.Items.Add(pi);
                lbIncluded.Items.Remove(pi);
            }
        }

        private void cmdExclude_Click(object sender, EventArgs e)
        {
            var items = lbIncluded.SelectedItems.Cast<PropertyInfo>().ToList();

            foreach (var pi in items)
            {
                lbExcluded.Items.Add(pi);
                lbIncluded.Items.Remove(pi);
            }
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            if (lbIncluded.SelectedItems.Count != 1 || lbIncluded.SelectedIndex == 0)
            {
                return;
            }

            var idx = lbIncluded.SelectedIndex;
            var item = (PropertyInfo)lbIncluded.SelectedItem;

            lbIncluded.Items.Remove(item);

            lbIncluded.Items.Insert(idx - 1, item);

            lbIncluded.SelectedItem = item;
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            if (lbIncluded.SelectedItems.Count != 1 || lbIncluded.SelectedIndex == lbIncluded.Items.Count - 1)
            {
                return;
            }

            var idx = lbIncluded.SelectedIndex;
            var item = (PropertyInfo)lbIncluded.SelectedItem;

            lbIncluded.Items.Remove(item);

            lbIncluded.Items.Insert(idx + 1, item);

            lbIncluded.SelectedItem = item;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            if (lbIncluded.Items.Count == 0)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                                           Number = (int)nudRecords.Value,
                                           PropertyInfos = lbIncluded.Items.Cast<PropertyInfo>().ToList()
                                       });
        }
    }
}
