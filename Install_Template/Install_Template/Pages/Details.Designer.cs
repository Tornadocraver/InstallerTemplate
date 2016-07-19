namespace Install_Template
{
    partial class Details
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
            this.label1 = new System.Windows.Forms.Label();
            this.textLoc = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonInst = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkShort = new System.Windows.Forms.CheckBox();
            this.checkStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Installation Path:";
            // 
            // textLoc
            // 
            this.textLoc.Location = new System.Drawing.Point(103, 16);
            this.textLoc.Name = "textLoc";
            this.textLoc.ReadOnly = true;
            this.textLoc.Size = new System.Drawing.Size(302, 20);
            this.textLoc.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(411, 14);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(56, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonInst
            // 
            this.buttonInst.Location = new System.Drawing.Point(392, 58);
            this.buttonInst.Name = "buttonInst";
            this.buttonInst.Size = new System.Drawing.Size(75, 23);
            this.buttonInst.TabIndex = 3;
            this.buttonInst.Text = "Install";
            this.buttonInst.UseVisualStyleBackColor = true;
            this.buttonInst.Click += new System.EventHandler(this.buttonInst_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(311, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkShort
            // 
            this.checkShort.AutoSize = true;
            this.checkShort.Checked = true;
            this.checkShort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkShort.Location = new System.Drawing.Point(40, 42);
            this.checkShort.Name = "checkShort";
            this.checkShort.Size = new System.Drawing.Size(184, 17);
            this.checkShort.TabIndex = 5;
            this.checkShort.Text = "Create Shortcut (.lnk) on Desktop";
            this.checkShort.UseVisualStyleBackColor = true;
            // 
            // checkStart
            // 
            this.checkStart.AutoSize = true;
            this.checkStart.Location = new System.Drawing.Point(40, 65);
            this.checkStart.Name = "checkStart";
            this.checkStart.Size = new System.Drawing.Size(171, 17);
            this.checkStart.TabIndex = 6;
            this.checkStart.Text = "Open after Installation Finished";
            this.checkStart.UseVisualStyleBackColor = true;
            // 
            // Details
            // 
            this.AcceptButton = this.buttonInst;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(479, 93);
            this.Controls.Add(this.checkStart);
            this.Controls.Add(this.checkShort);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonInst);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textLoc);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Installation Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Details_FormClosing);
            this.Load += new System.EventHandler(this.Details_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textLoc;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonInst;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkShort;
        private System.Windows.Forms.CheckBox checkStart;
    }
}