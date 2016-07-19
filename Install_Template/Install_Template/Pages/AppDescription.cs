using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Install_Template
{
    public partial class AppDescription : Form
    {
        public AppDescription()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void ShowAgain()
        {
            Program.ad.Show();
            Program.d.Dispose();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Program.ad.Hide();
            Program.d = new Details();
            Program.d.Show();
        }

        private void AppDescription_Load(object sender, EventArgs e)
        {
            this.Text = Assembly.GetExecutingAssembly().GetName().Name + " Installer";
        }

        private void AppDescription_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit the installation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { e.Cancel = false; }
            else
            { e.Cancel = true; }
        }
    }
}
