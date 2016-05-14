using System;
using System.ComponentModel;

namespace zxy_tile_slicer
{
    partial class Form1
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblW = new System.Windows.Forms.Label();
            this.lblBaseWidth = new System.Windows.Forms.Label();
            this.lblBaseHeight = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBaseZoom = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboZoomSelect = new System.Windows.Forms.ComboBox();
            this.btnDestination = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxProcessing = new System.Windows.Forms.TextBox();
            this.progressProcessing = new System.Windows.Forms.ProgressBar();
            this.tbxSourceFile = new System.Windows.Forms.TextBox();
            this.tbxDestination = new System.Windows.Forms.TextBox();
            this.tbxError = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpProjection = new System.Windows.Forms.GroupBox();
            this.rbtnEquirectangular = new System.Windows.Forms.RadioButton();
            this.rbtnMerator = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grpHelp = new System.Windows.Forms.GroupBox();
            this.tbxHelp = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpProjection.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(4, 63);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(95, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(245, 127);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(12, 124);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(38, 13);
            this.lblW.TabIndex = 3;
            this.lblW.Text = "Width:";
            this.lblW.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblBaseWidth
            // 
            this.lblBaseWidth.AutoSize = true;
            this.lblBaseWidth.Location = new System.Drawing.Point(75, 124);
            this.lblBaseWidth.Name = "lblBaseWidth";
            this.lblBaseWidth.Size = new System.Drawing.Size(35, 13);
            this.lblBaseWidth.TabIndex = 4;
            this.lblBaseWidth.Text = "label3";
            // 
            // lblBaseHeight
            // 
            this.lblBaseHeight.AutoSize = true;
            this.lblBaseHeight.Location = new System.Drawing.Point(75, 145);
            this.lblBaseHeight.Name = "lblBaseHeight";
            this.lblBaseHeight.Size = new System.Drawing.Size(35, 13);
            this.lblBaseHeight.TabIndex = 6;
            this.lblBaseHeight.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Height:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblBaseZoom
            // 
            this.lblBaseZoom.AutoSize = true;
            this.lblBaseZoom.Location = new System.Drawing.Point(75, 169);
            this.lblBaseZoom.Name = "lblBaseZoom";
            this.lblBaseZoom.Size = new System.Drawing.Size(35, 13);
            this.lblBaseZoom.TabIndex = 8;
            this.lblBaseZoom.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Max Zoom:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Zoom:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cboZoomSelect
            // 
            this.cboZoomSelect.FormattingEnabled = true;
            this.cboZoomSelect.Location = new System.Drawing.Point(78, 191);
            this.cboZoomSelect.Name = "cboZoomSelect";
            this.cboZoomSelect.Size = new System.Drawing.Size(121, 21);
            this.cboZoomSelect.TabIndex = 11;
            // 
            // btnDestination
            // 
            this.btnDestination.Location = new System.Drawing.Point(4, 89);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(95, 23);
            this.btnDestination.TabIndex = 12;
            this.btnDestination.Text = "Save Location";
            this.btnDestination.UseVisualStyleBackColor = true;
            this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 228);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Create Tiles";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxProcessing
            // 
            this.tbxProcessing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxProcessing.Location = new System.Drawing.Point(4, 262);
            this.tbxProcessing.Multiline = true;
            this.tbxProcessing.Name = "tbxProcessing";
            this.tbxProcessing.ReadOnly = true;
            this.tbxProcessing.Size = new System.Drawing.Size(221, 43);
            this.tbxProcessing.TabIndex = 15;
            // 
            // progressProcessing
            // 
            this.progressProcessing.Location = new System.Drawing.Point(4, 310);
            this.progressProcessing.Name = "progressProcessing";
            this.progressProcessing.Size = new System.Drawing.Size(221, 23);
            this.progressProcessing.TabIndex = 16;
            this.progressProcessing.Visible = false;
            // 
            // tbxSourceFile
            // 
            this.tbxSourceFile.Location = new System.Drawing.Point(106, 67);
            this.tbxSourceFile.Margin = new System.Windows.Forms.Padding(2);
            this.tbxSourceFile.Name = "tbxSourceFile";
            this.tbxSourceFile.ReadOnly = true;
            this.tbxSourceFile.Size = new System.Drawing.Size(539, 20);
            this.tbxSourceFile.TabIndex = 17;
            // 
            // tbxDestination
            // 
            this.tbxDestination.Location = new System.Drawing.Point(106, 92);
            this.tbxDestination.Margin = new System.Windows.Forms.Padding(2);
            this.tbxDestination.Name = "tbxDestination";
            this.tbxDestination.ReadOnly = true;
            this.tbxDestination.Size = new System.Drawing.Size(539, 20);
            this.tbxDestination.TabIndex = 18;
            // 
            // tbxError
            // 
            this.tbxError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxError.Location = new System.Drawing.Point(4, 339);
            this.tbxError.Margin = new System.Windows.Forms.Padding(2);
            this.tbxError.Multiline = true;
            this.tbxError.Name = "tbxError";
            this.tbxError.ReadOnly = true;
            this.tbxError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxError.Size = new System.Drawing.Size(221, 157);
            this.tbxError.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Projection Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpProjection
            // 
            this.grpProjection.BackColor = System.Drawing.Color.Transparent;
            this.grpProjection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpProjection.Controls.Add(this.rbtnEquirectangular);
            this.grpProjection.Controls.Add(this.rbtnMerator);
            this.grpProjection.Location = new System.Drawing.Point(107, 32);
            this.grpProjection.Name = "grpProjection";
            this.grpProjection.Size = new System.Drawing.Size(200, 34);
            this.grpProjection.TabIndex = 23;
            this.grpProjection.TabStop = false;
            // 
            // rbtnEquirectangular
            // 
            this.rbtnEquirectangular.AutoSize = true;
            this.rbtnEquirectangular.Location = new System.Drawing.Point(78, 11);
            this.rbtnEquirectangular.Name = "rbtnEquirectangular";
            this.rbtnEquirectangular.Size = new System.Drawing.Size(99, 17);
            this.rbtnEquirectangular.TabIndex = 23;
            this.rbtnEquirectangular.Text = "Equirectangular";
            this.rbtnEquirectangular.UseVisualStyleBackColor = true;
            // 
            // rbtnMerator
            // 
            this.rbtnMerator.AutoSize = true;
            this.rbtnMerator.Checked = true;
            this.rbtnMerator.Location = new System.Drawing.Point(5, 11);
            this.rbtnMerator.Name = "rbtnMerator";
            this.rbtnMerator.Size = new System.Drawing.Size(67, 17);
            this.rbtnMerator.TabIndex = 22;
            this.rbtnMerator.TabStop = true;
            this.rbtnMerator.Text = "Mercator";
            this.rbtnMerator.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(654, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "&About";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // grpHelp
            // 
            this.grpHelp.Controls.Add(this.tbxHelp);
            this.grpHelp.Controls.Add(this.button1);
            this.grpHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grpHelp.Location = new System.Drawing.Point(236, 114);
            this.grpHelp.Margin = new System.Windows.Forms.Padding(2);
            this.grpHelp.Name = "grpHelp";
            this.grpHelp.Padding = new System.Windows.Forms.Padding(2);
            this.grpHelp.Size = new System.Drawing.Size(407, 382);
            this.grpHelp.TabIndex = 25;
            this.grpHelp.TabStop = false;
            this.grpHelp.Text = "About";
            // 
            // tbxHelp
            // 
            this.tbxHelp.Location = new System.Drawing.Point(9, 32);
            this.tbxHelp.Margin = new System.Windows.Forms.Padding(2);
            this.tbxHelp.Multiline = true;
            this.tbxHelp.Name = "tbxHelp";
            this.tbxHelp.ReadOnly = true;
            this.tbxHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxHelp.Size = new System.Drawing.Size(395, 346);
            this.tbxHelp.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(346, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 505);
            this.Controls.Add(this.grpHelp);
            this.Controls.Add(this.grpProjection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxError);
            this.Controls.Add(this.tbxDestination);
            this.Controls.Add(this.tbxSourceFile);
            this.Controls.Add(this.progressProcessing);
            this.Controls.Add(this.tbxProcessing);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDestination);
            this.Controls.Add(this.cboZoomSelect);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblBaseZoom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblBaseHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblBaseWidth);
            this.Controls.Add(this.lblW);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpProjection.ResumeLayout(false);
            this.grpProjection.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpHelp.ResumeLayout(false);
            this.grpHelp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblW;
        private System.Windows.Forms.Label lblBaseWidth;
        private System.Windows.Forms.Label lblBaseHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblBaseZoom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboZoomSelect;
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxProcessing;
        private System.Windows.Forms.ProgressBar progressProcessing;
		private System.Windows.Forms.TextBox tbxSourceFile;
		private System.Windows.Forms.TextBox tbxDestination;
		private System.Windows.Forms.TextBox tbxError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpProjection;
        private System.Windows.Forms.RadioButton rbtnEquirectangular;
        private System.Windows.Forms.RadioButton rbtnMerator;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.GroupBox grpHelp;
		private System.Windows.Forms.TextBox tbxHelp;
		private System.Windows.Forms.Button button1;
    }
}

