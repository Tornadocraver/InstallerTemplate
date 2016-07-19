using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Install_Template
{
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Path to install application";
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textLoc.Text = Path.Combine(fbd.SelectedPath, Assembly.GetExecutingAssembly().GetName().Name);
                this.AcceptButton = buttonInst;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.ad.ShowAgain();
            Program.d.Hide();
        }

        private void buttonInst_Click(object sender, EventArgs e)
        {
            Program.i = new Installing();
            Program.i.Shortcut = checkShort.Checked;
            Program.i.Start = checkStart.Checked;
            Program.i.InstLoc = textLoc.Text;
            Program.d.Hide();
            Program.i.Show();
            Program.i.Activate();
        }

        private void Details_Load(object sender, EventArgs e)
        {
            textLoc.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Assembly.GetExecutingAssembly().GetName().Name);
        }

        private void Details_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit the installation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { e.Cancel = false; }
            else
            { e.Cancel = true; }
        }
    }
}
