using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FOLauncher
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

        public class wbExt : System.Windows.Forms.WebBrowser
        {
            private BorderStyle _borderStyle;
            [
            Category("Appearance"),
            Description("The border style")
            ]

            public BorderStyle BorderStyle
            {
                get
                {
                    return _borderStyle;
                }
                set
                {
                    _borderStyle = value;
                    this.RecreateHandle();
                    Invalidate();
                }
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    const int WS_BORDER = 0x00800000;
                    const int WS_EX_STATICEDGE = 0x00020000;
                    CreateParams cp = base.CreateParams;
                    switch (_borderStyle)
                    {
                        case BorderStyle.FixedSingle:
                            cp.Style |= WS_BORDER;
                            break;
                        case BorderStyle.Fixed3D:
                            cp.ExStyle |= WS_EX_STATICEDGE;
                            break;
                    }
                    return cp;
                }
            }

            public wbExt()
            {
            }
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
            this.webBrowser = new FOLauncher.frmMain.wbExt();
            this.pnlWebPage = new System.Windows.Forms.Panel();
            this.lblWebBrowerFail = new System.Windows.Forms.Label();
            this.pctBackground = new System.Windows.Forms.PictureBox();
            this.pctPlayUp = new System.Windows.Forms.PictureBox();
            this.pctPlayDown = new System.Windows.Forms.PictureBox();
            this.pctUpdateUp = new System.Windows.Forms.PictureBox();
            this.pctUpdateDown = new System.Windows.Forms.PictureBox();
            this.pctConfigUp = new System.Windows.Forms.PictureBox();
            this.pctConfigDown = new System.Windows.Forms.PictureBox();
            this.pctExitDown = new System.Windows.Forms.PictureBox();
            this.pctExitUp = new System.Windows.Forms.PictureBox();
            this.tmrModal = new System.Windows.Forms.Timer(this.components);
            this.pctLampOff = new System.Windows.Forms.PictureBox();
            this.tmrCheckStatus = new System.Windows.Forms.Timer(this.components);
            this.pnlWebPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPlayUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPlayDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUpdateUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUpdateDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctConfigUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctConfigDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctExitDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctExitUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLampOff)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(328, 425);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // pnlWebPage
            // 
            this.pnlWebPage.Controls.Add(this.lblWebBrowerFail);
            this.pnlWebPage.Controls.Add(this.webBrowser);
            this.pnlWebPage.Location = new System.Drawing.Point(33, 23);
            this.pnlWebPage.Name = "pnlWebPage";
            this.pnlWebPage.Size = new System.Drawing.Size(328, 425);
            this.pnlWebPage.TabIndex = 0;
            // 
            // lblWebBrowerFail
            // 
            this.lblWebBrowerFail.AutoSize = true;
            this.lblWebBrowerFail.BackColor = System.Drawing.Color.Black;
            this.lblWebBrowerFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebBrowerFail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblWebBrowerFail.Location = new System.Drawing.Point(3, 9);
            this.lblWebBrowerFail.Name = "lblWebBrowerFail";
            this.lblWebBrowerFail.Size = new System.Drawing.Size(274, 20);
            this.lblWebBrowerFail.TabIndex = 1;
            this.lblWebBrowerFail.Text = "Couldn\'t retrieve changelog feed.";
            this.lblWebBrowerFail.Visible = false;
            // 
            // pctBackground
            // 
            this.pctBackground.Image = ((System.Drawing.Image)(resources.GetObject("pctBackground.Image")));
            this.pctBackground.Location = new System.Drawing.Point(-4, -5);
            this.pctBackground.Name = "pctBackground";
            this.pctBackground.Size = new System.Drawing.Size(642, 487);
            this.pctBackground.TabIndex = 3;
            this.pctBackground.TabStop = false;
            // 
            // pctPlayUp
            // 
            this.pctPlayUp.Image = ((System.Drawing.Image)(resources.GetObject("pctPlayUp.Image")));
            this.pctPlayUp.Location = new System.Drawing.Point(448, 132);
            this.pctPlayUp.Name = "pctPlayUp";
            this.pctPlayUp.Size = new System.Drawing.Size(135, 29);
            this.pctPlayUp.TabIndex = 4;
            this.pctPlayUp.TabStop = false;
            this.pctPlayUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctPlayUp_MouseDown);
            this.pctPlayUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctPlayUp_MouseUp);
            // 
            // pctPlayDown
            // 
            this.pctPlayDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pctPlayDown.Image = ((System.Drawing.Image)(resources.GetObject("pctPlayDown.Image")));
            this.pctPlayDown.Location = new System.Drawing.Point(448, 132);
            this.pctPlayDown.Name = "pctPlayDown";
            this.pctPlayDown.Size = new System.Drawing.Size(135, 29);
            this.pctPlayDown.TabIndex = 5;
            this.pctPlayDown.TabStop = false;
            this.pctPlayDown.Visible = false;
            // 
            // pctUpdateUp
            // 
            this.pctUpdateUp.Image = ((System.Drawing.Image)(resources.GetObject("pctUpdateUp.Image")));
            this.pctUpdateUp.Location = new System.Drawing.Point(448, 165);
            this.pctUpdateUp.Name = "pctUpdateUp";
            this.pctUpdateUp.Size = new System.Drawing.Size(135, 29);
            this.pctUpdateUp.TabIndex = 6;
            this.pctUpdateUp.TabStop = false;
            this.pctUpdateUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctUpdateUp_MouseDown);
            this.pctUpdateUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctUpdateUp_MouseUp);
            // 
            // pctUpdateDown
            // 
            this.pctUpdateDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pctUpdateDown.Image = ((System.Drawing.Image)(resources.GetObject("pctUpdateDown.Image")));
            this.pctUpdateDown.Location = new System.Drawing.Point(448, 165);
            this.pctUpdateDown.Name = "pctUpdateDown";
            this.pctUpdateDown.Size = new System.Drawing.Size(135, 29);
            this.pctUpdateDown.TabIndex = 7;
            this.pctUpdateDown.TabStop = false;
            this.pctUpdateDown.Visible = false;
            // 
            // pctConfigUp
            // 
            this.pctConfigUp.Image = ((System.Drawing.Image)(resources.GetObject("pctConfigUp.Image")));
            this.pctConfigUp.Location = new System.Drawing.Point(448, 200);
            this.pctConfigUp.Name = "pctConfigUp";
            this.pctConfigUp.Size = new System.Drawing.Size(135, 29);
            this.pctConfigUp.TabIndex = 8;
            this.pctConfigUp.TabStop = false;
            this.pctConfigUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctConfigUp_MouseDown);
            this.pctConfigUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctConfigUp_MouseUp);
            // 
            // pctConfigDown
            // 
            this.pctConfigDown.Image = ((System.Drawing.Image)(resources.GetObject("pctConfigDown.Image")));
            this.pctConfigDown.Location = new System.Drawing.Point(448, 200);
            this.pctConfigDown.Name = "pctConfigDown";
            this.pctConfigDown.Size = new System.Drawing.Size(135, 29);
            this.pctConfigDown.TabIndex = 9;
            this.pctConfigDown.TabStop = false;
            this.pctConfigDown.Visible = false;
            // 
            // pctExitDown
            // 
            this.pctExitDown.Image = ((System.Drawing.Image)(resources.GetObject("pctExitDown.Image")));
            this.pctExitDown.Location = new System.Drawing.Point(448, 234);
            this.pctExitDown.Name = "pctExitDown";
            this.pctExitDown.Size = new System.Drawing.Size(135, 29);
            this.pctExitDown.TabIndex = 10;
            this.pctExitDown.TabStop = false;
            this.pctExitDown.Visible = false;
            // 
            // pctExitUp
            // 
            this.pctExitUp.Image = ((System.Drawing.Image)(resources.GetObject("pctExitUp.Image")));
            this.pctExitUp.Location = new System.Drawing.Point(448, 234);
            this.pctExitUp.Name = "pctExitUp";
            this.pctExitUp.Size = new System.Drawing.Size(135, 29);
            this.pctExitUp.TabIndex = 11;
            this.pctExitUp.TabStop = false;
            this.pctExitUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctExitUp_MouseDown);
            this.pctExitUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctExitUp_MouseUp);
            // 
            // pctLampOff
            // 
            this.pctLampOff.Image = ((System.Drawing.Image)(resources.GetObject("pctLampOff.Image")));
            this.pctLampOff.Location = new System.Drawing.Point(381, 322);
            this.pctLampOff.Name = "pctLampOff";
            this.pctLampOff.Size = new System.Drawing.Size(243, 148);
            this.pctLampOff.TabIndex = 12;
            this.pctLampOff.TabStop = false;
            this.pctLampOff.Visible = false;
            // 
            // tmrCheckStatus
            // 
            this.tmrCheckStatus.Enabled = true;
            this.tmrCheckStatus.Interval = 300000;
            this.tmrCheckStatus.Tick += new System.EventHandler(this.tmrCheckStatus_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(635, 474);
            this.Controls.Add(this.pctLampOff);
            this.Controls.Add(this.pnlWebPage);
            this.Controls.Add(this.pctExitUp);
            this.Controls.Add(this.pctExitDown);
            this.Controls.Add(this.pctConfigDown);
            this.Controls.Add(this.pctConfigUp);
            this.Controls.Add(this.pctUpdateDown);
            this.Controls.Add(this.pctUpdateUp);
            this.Controls.Add(this.pctPlayDown);
            this.Controls.Add(this.pctPlayUp);
            this.Controls.Add(this.pctBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FOnline: 2238 Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.pnlWebPage.ResumeLayout(false);
            this.pnlWebPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPlayUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPlayDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUpdateUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctUpdateDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctConfigUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctConfigDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctExitDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctExitUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLampOff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private wbExt webBrowser;
        private System.Windows.Forms.Panel pnlWebPage;
        private System.Windows.Forms.PictureBox pctBackground;
        private System.Windows.Forms.PictureBox pctPlayUp;
        private System.Windows.Forms.PictureBox pctPlayDown;
        private System.Windows.Forms.PictureBox pctUpdateUp;
        private System.Windows.Forms.PictureBox pctUpdateDown;
        private System.Windows.Forms.PictureBox pctConfigUp;
        private System.Windows.Forms.PictureBox pctConfigDown;
        private System.Windows.Forms.PictureBox pctExitDown;
        private System.Windows.Forms.PictureBox pctExitUp;
        private System.Windows.Forms.Label lblWebBrowerFail;
        private System.Windows.Forms.Timer tmrModal;
        private System.Windows.Forms.PictureBox pctLampOff;
        private Timer tmrCheckStatus;
    }
}

