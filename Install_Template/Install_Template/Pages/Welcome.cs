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
using Micahz;

namespace Install_Template
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MicahzBools.HasConnection())
            {
                System.Diagnostics.Process.Start(linkLabel1.Text);
            }
        } //PLEASE leave this block of code in for obvious reasons.

        private void buttonCon_Click(object sender, EventArgs e)
        {
            Program.w.Hide();
            Program.ad = new AppDescription();
            Program.ad.Show();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            Program.w = this;
            Image i = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Install_Template.Images.Pic.png"));
            pictureBox1.Image = (Image)i.Clone();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            i.Dispose();
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit the installation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { e.Cancel = false; }
            else
            { e.Cancel = true; }
        }
    }
}
