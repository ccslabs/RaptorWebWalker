namespace RaptorWebWalker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblAuthorized = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLicenseNumber = new System.Windows.Forms.Label();
            this.timerSeconds = new System.Windows.Forms.Timer(this.components);
            this.Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customGroupBox1 = new RaptorWebWalker.Controls.CustomGroupBox();
            this.lblRuntimeSinceLastBoot = new System.Windows.Forms.Label();
            this.lblTotalRuntime = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContentObjects = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDiscovered = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProcessed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.customGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAuthorized
            // 
            this.lblAuthorized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAuthorized.BackColor = global::RaptorWebWalker.Properties.Settings.Default.ErrorBackColour;
            this.lblAuthorized.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::RaptorWebWalker.Properties.Settings.Default, "ErrorForeColour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblAuthorized.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::RaptorWebWalker.Properties.Settings.Default, "ErrorBackColour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblAuthorized.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RaptorWebWalker.Properties.Settings.Default, "AuthorisedStatus", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblAuthorized.ForeColor = global::RaptorWebWalker.Properties.Settings.Default.ErrorForeColour;
            this.lblAuthorized.Location = new System.Drawing.Point(151, 193);
            this.lblAuthorized.Name = "lblAuthorized";
            this.lblAuthorized.Size = new System.Drawing.Size(114, 13);
            this.lblAuthorized.TabIndex = 1;
            this.lblAuthorized.Text = global::RaptorWebWalker.Properties.Settings.Default.IsAuthorised;
            this.lblAuthorized.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Agent of The Computer Crime Service. (CCS-LABS.COM)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::RaptorWebWalker.Properties.Resources.hand_print_blue;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(69, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(115, 167);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(422, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "...";
            // 
            // lblLicenseNumber
            // 
            this.lblLicenseNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLicenseNumber.Location = new System.Drawing.Point(193, 180);
            this.lblLicenseNumber.Name = "lblLicenseNumber";
            this.lblLicenseNumber.Size = new System.Drawing.Size(341, 13);
            this.lblLicenseNumber.TabIndex = 11;
            this.lblLicenseNumber.Text = "License:";
            this.lblLicenseNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timerSeconds
            // 
            this.timerSeconds.Enabled = true;
            this.timerSeconds.Interval = 1000;
            this.timerSeconds.Tick += new System.EventHandler(this.timerSeconds_Tick);
            // 
            // Notify
            // 
            this.Notify.ContextMenuStrip = this.cmsMenu;
            this.Notify.Icon = ((System.Drawing.Icon)(resources.GetObject("Notify.Icon")));
            this.Notify.Visible = true;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(120, 26);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // customGroupBox1
            // 
            this.customGroupBox1.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox1.Controls.Add(this.lblRuntimeSinceLastBoot);
            this.customGroupBox1.Controls.Add(this.lblTotalRuntime);
            this.customGroupBox1.Controls.Add(this.label29);
            this.customGroupBox1.Controls.Add(this.label28);
            this.customGroupBox1.Controls.Add(this.label27);
            this.customGroupBox1.Controls.Add(this.label23);
            this.customGroupBox1.Controls.Add(this.label24);
            this.customGroupBox1.Controls.Add(this.label25);
            this.customGroupBox1.Controls.Add(this.label26);
            this.customGroupBox1.Controls.Add(this.label19);
            this.customGroupBox1.Controls.Add(this.label20);
            this.customGroupBox1.Controls.Add(this.label21);
            this.customGroupBox1.Controls.Add(this.label22);
            this.customGroupBox1.Controls.Add(this.label15);
            this.customGroupBox1.Controls.Add(this.label16);
            this.customGroupBox1.Controls.Add(this.label17);
            this.customGroupBox1.Controls.Add(this.label18);
            this.customGroupBox1.Controls.Add(this.label11);
            this.customGroupBox1.Controls.Add(this.label12);
            this.customGroupBox1.Controls.Add(this.label13);
            this.customGroupBox1.Controls.Add(this.label14);
            this.customGroupBox1.Controls.Add(this.label10);
            this.customGroupBox1.Controls.Add(this.label9);
            this.customGroupBox1.Controls.Add(this.label8);
            this.customGroupBox1.Controls.Add(this.label4);
            this.customGroupBox1.Controls.Add(this.label7);
            this.customGroupBox1.Controls.Add(this.lblContentObjects);
            this.customGroupBox1.Controls.Add(this.label6);
            this.customGroupBox1.Controls.Add(this.lblDiscovered);
            this.customGroupBox1.Controls.Add(this.label5);
            this.customGroupBox1.Controls.Add(this.lblProcessed);
            this.customGroupBox1.Controls.Add(this.label2);
            this.customGroupBox1.Location = new System.Drawing.Point(72, 12);
            this.customGroupBox1.Name = "customGroupBox1";
            this.customGroupBox1.Size = new System.Drawing.Size(465, 152);
            this.customGroupBox1.TabIndex = 3;
            this.customGroupBox1.TabStop = false;
            this.customGroupBox1.Text = "Information ";
            // 
            // lblRuntimeSinceLastBoot
            // 
            this.lblRuntimeSinceLastBoot.AutoSize = true;
            this.lblRuntimeSinceLastBoot.Location = new System.Drawing.Point(214, 121);
            this.lblRuntimeSinceLastBoot.Name = "lblRuntimeSinceLastBoot";
            this.lblRuntimeSinceLastBoot.Size = new System.Drawing.Size(13, 13);
            this.lblRuntimeSinceLastBoot.TabIndex = 64;
            this.lblRuntimeSinceLastBoot.Text = "0";
            // 
            // lblTotalRuntime
            // 
            this.lblTotalRuntime.AutoSize = true;
            this.lblTotalRuntime.Location = new System.Drawing.Point(104, 121);
            this.lblTotalRuntime.Name = "lblTotalRuntime";
            this.lblTotalRuntime.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRuntime.TabIndex = 63;
            this.lblTotalRuntime.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(214, 105);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(82, 13);
            this.label29.TabIndex = 62;
            this.label29.Text = "Since Last Boot";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(104, 105);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(31, 13);
            this.label28.TabIndex = 61;
            this.label28.Text = "Total";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(15, 121);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 13);
            this.label27.TabIndex = 60;
            this.label27.Text = "Run Time";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(440, 82);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(13, 13);
            this.label23.TabIndex = 59;
            this.label23.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(440, 68);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(13, 13);
            this.label24.TabIndex = 58;
            this.label24.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(440, 27);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(14, 13);
            this.label25.TabIndex = 57;
            this.label25.Text = "C";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(440, 54);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(13, 13);
            this.label26.TabIndex = 56;
            this.label26.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(408, 82);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 13);
            this.label19.TabIndex = 55;
            this.label19.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(408, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(13, 13);
            this.label20.TabIndex = 54;
            this.label20.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(408, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 13);
            this.label21.TabIndex = 53;
            this.label21.Text = "R";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(408, 54);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(13, 13);
            this.label22.TabIndex = 52;
            this.label22.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(373, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(373, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(373, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 49;
            this.label17.Text = "A";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(373, 54);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(339, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 47;
            this.label11.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(339, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(339, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "U";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(339, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 44;
            this.label14.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "P/H";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Total";
            // 
            // lblContentObjects
            // 
            this.lblContentObjects.AutoSize = true;
            this.lblContentObjects.Location = new System.Drawing.Point(104, 82);
            this.lblContentObjects.Name = "lblContentObjects";
            this.lblContentObjects.Size = new System.Drawing.Size(13, 13);
            this.lblContentObjects.TabIndex = 38;
            this.lblContentObjects.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Content Objects";
            // 
            // lblDiscovered
            // 
            this.lblDiscovered.AutoSize = true;
            this.lblDiscovered.Location = new System.Drawing.Point(104, 68);
            this.lblDiscovered.Name = "lblDiscovered";
            this.lblDiscovered.Size = new System.Drawing.Size(13, 13);
            this.lblDiscovered.TabIndex = 36;
            this.lblDiscovered.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Discovered";
            // 
            // lblProcessed
            // 
            this.lblProcessed.AutoSize = true;
            this.lblProcessed.Location = new System.Drawing.Point(104, 54);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new System.Drawing.Size(13, 13);
            this.lblProcessed.TabIndex = 34;
            this.lblProcessed.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Processed ";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(546, 215);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLicenseNumber);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.customGroupBox1);
            this.Controls.Add(this.lblAuthorized);
            this.Controls.Add(this.pictureBox1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::RaptorWebWalker.Properties.Settings.Default, "ForeColour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ForeColor = global::RaptorWebWalker.Properties.Settings.Default.ForeColour;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.customGroupBox1.ResumeLayout(false);
            this.customGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAuthorized;
        private System.Windows.Forms.Label label1;
        private Controls.CustomGroupBox customGroupBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblContentObjects;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDiscovered;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProcessed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLicenseNumber;
        private System.Windows.Forms.Label lblRuntimeSinceLastBoot;
        private System.Windows.Forms.Label lblTotalRuntime;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Timer timerSeconds;
        private System.Windows.Forms.NotifyIcon Notify;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
    }
}

