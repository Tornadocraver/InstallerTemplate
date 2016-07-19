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
using System.IO;
using Shell32;
using IWshRuntimeLibrary;

namespace Install_Template
{
    public partial class Installing : Form
    {
        Boolean skip { get; set; }

        public Boolean Shortcut { get; set; }
        public Boolean Start { get; set; }
        public string InstLoc { get; set; }

        BackgroundWorker bW = new BackgroundWorker();

        public Installing()
        {
            InitializeComponent();
        }

        private void Installing_Load(object sender, EventArgs e)
        {
            this.Text = "Installing " + Assembly.GetExecutingAssembly().GetName().Name + "... (0%)";
            bW.DoWork += bW_DoWork;
            bW.ProgressChanged += bW_ProgressChanged;
            bW.RunWorkerCompleted += bW_RunWorkerCompleted;
            bW.WorkerReportsProgress = true;
            bW.RunWorkerAsync();
        }

        void bW_DoWork(object sender, DoWorkEventArgs e)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            //Create folder
            if (!Directory.Exists(InstLoc))
            {
                Directory.CreateDirectory(InstLoc);
            }
            else
            {
                if (MessageBox.Show("This program had already been installed. Would you like to over-write it with this version?", "ERR_EXISTS", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    goto end;
            }

            //Copy files
            string mainName = thisAssembly.GetName().Name + ".exe";
            string[] EXEs = thisAssembly.GetManifestResourceNames().Where(f => f.Contains("InstallationFiles.Executables") && f.EndsWith(".exe")).ToArray();
            string[] DLLs = thisAssembly.GetManifestResourceNames().Where(f => f.Contains("InstallationFiles.Dlls") && f.EndsWith(".dll")).ToArray();
            string[] CSes = thisAssembly.GetManifestResourceNames().Where(f => f.Contains("InstallationFiles.Classes") && f.EndsWith(".cs")).ToArray();
            string[] Empty = new string[0];
            
            //Get streams from resource files
            Dictionary<string, Stream> FileStreams = new Dictionary<string, Stream>();
            if (EXEs != Empty)
                foreach (string file in EXEs)
                    FileStreams.Add(file.Replace(thisAssembly.GetName().Name + ".InstallationFiles.Executables.", ""), thisAssembly.GetManifestResourceStream(file));
            if (DLLs != Empty)
                foreach (string file in DLLs)
                    FileStreams.Add(file.Replace(thisAssembly.GetName().Name + ".InstallationFiles.Dlls.", ""), thisAssembly.GetManifestResourceStream(file));
            if (CSes != Empty)
                foreach (string file in CSes)
                    FileStreams.Add(file.Replace(thisAssembly.GetName().Name + ".InstallationFiles.Classes.", ""), thisAssembly.GetManifestResourceStream(file));

            //Calculate total bytes
            int iCount = FileStreams.Count;
            int cCount = 0;

            #region Write Files
            foreach (var file in FileStreams.Keys)
            {
                if (file.EndsWith("MAINEXE.exe"))
                {
                    using (FileStream fs = new FileStream(Path.Combine(InstLoc, mainName), FileMode.Create, FileAccess.Write))
                    {
                        using (Stream s = FileStreams[file])
                        {
                            s.CopyTo(fs);
                            fs.Close();
                            Progress(cCount++, iCount);
                        }
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream(Path.Combine(InstLoc, file), FileMode.Create, FileAccess.Write))
                    {
                        using (Stream s = FileStreams[file])
                        {
                            s.CopyTo(fs);
                            fs.Close();
                            Progress(cCount++, iCount);
                        }
                    }
                }
            }
            //Create uninstall info file
            List<string> files = new List<string>();
            foreach (string s in FileStreams.Keys)
                if (s.EndsWith("MAINEXE.exe"))
                    files.Add(mainName);
                else
                    files.Add(s);
            files.Add(this.InstLoc);
            if (Shortcut)
            {
                files.Add(CreateShortcut(Path.Combine(InstLoc, mainName))); //Create shortcut on desktop
            }
            else
            {
                files.Add("~");
            }
            System.IO.File.WriteAllLines(Path.Combine(InstLoc, "UNINSTALL_INFO.txt"), files);
            //Create uninstaller file
            using (FileStream uninstall = new FileStream(Path.Combine(InstLoc, thisAssembly.GetName().Name + "_Uninstaller.exe"), FileMode.Create, FileAccess.Write))
            {
                string[] file = thisAssembly.GetManifestResourceNames().Where(f => f.Contains(".Uninstall.") && f.EndsWith("UNINSTALL.exe")).ToArray();
                using (Stream s = thisAssembly.GetManifestResourceStream(file[0]))
                {
                    s.CopyTo(uninstall);
                    uninstall.Close();
                }
            }
            #endregion

            //Make read-only
            if (!((System.IO.File.GetAttributes(InstLoc) & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly))
            {
                System.IO.File.SetAttributes(InstLoc, System.IO.FileAttributes.ReadOnly);
            }
        end: ;
        }
        void Progress(int _current, int _total)
        {
            if (_total != 0 && _current != 0)
            {
                bW.ReportProgress((_current * 200 + _total) / (_total * 2));
            }
        }
        void bW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { progress.Value = e.ProgressPercentage; this.Text = "Installing " + Assembly.GetExecutingAssembly().GetName().Name + "... (" + e.ProgressPercentage.ToString() + "%)"; }); }
            else { progress.Value = e.ProgressPercentage; this.Text = "Installing " + Assembly.GetExecutingAssembly().GetName().Name + "... (" + e.ProgressPercentage.ToString() + "%)"; }
        }
        void bW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Installation completed successfully.", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            if (Start)
            {
                System.Diagnostics.Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Name + ".exe"));
            }
            bW.Dispose();
            this.skip = true;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Installing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!skip)
                e.Cancel = true;
            else
                this.Activate();
                e.Cancel = false;
        }

        private string CreateShortcut(string pathToOrignal)
        {
            var wsh = new IWshShell_Class();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Assembly.GetExecutingAssembly().GetName().Name + ".lnk");
            IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(path) as IWshRuntimeLibrary.IWshShortcut;
            shortcut.TargetPath = pathToOrignal;
            shortcut.Save();
            return path;
        }
    }
}
