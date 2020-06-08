namespace DatasetCreationTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelClass = new System.Windows.Forms.Label();
            this.textBoxClass = new System.Windows.Forms.TextBox();
            this.labelRectHeight = new System.Windows.Forms.Label();
            this.numericUpDownRectHeight = new System.Windows.Forms.NumericUpDown();
            this.labelRectWidth = new System.Windows.Forms.Label();
            this.numericUpDownRectWidth = new System.Windows.Forms.NumericUpDown();
            this.pictureBoxWorkingImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorkingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Location = new System.Drawing.Point(3, 4);
            this.buttonOpenFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(112, 31);
            this.buttonOpenFolder.TabIndex = 0;
            this.buttonOpenFolder.Text = "Open Folder";
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.labelClass);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxClass);
            this.splitContainer1.Panel1.Controls.Add(this.labelRectHeight);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownRectHeight);
            this.splitContainer1.Panel1.Controls.Add(this.labelRectWidth);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownRectWidth);
            this.splitContainer1.Panel1.Controls.Add(this.buttonOpenFolder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxWorkingImage);
            this.splitContainer1.Size = new System.Drawing.Size(914, 600);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(266, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = " ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(242, 20);
            this.toolStripStatusLabel1.Text = "Click \"Open Folder\" to load images";
            // 
            // labelClass
            // 
            this.labelClass.AutoSize = true;
            this.labelClass.Location = new System.Drawing.Point(3, 56);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(42, 20);
            this.labelClass.TabIndex = 4;
            this.labelClass.Text = "Class";
            // 
            // textBoxClass
            // 
            this.textBoxClass.Location = new System.Drawing.Point(49, 45);
            this.textBoxClass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxClass.Name = "textBoxClass";
            this.textBoxClass.Size = new System.Drawing.Size(252, 27);
            this.textBoxClass.TabIndex = 3;
            // 
            // labelRectHeight
            // 
            this.labelRectHeight.AutoSize = true;
            this.labelRectHeight.Location = new System.Drawing.Point(3, 133);
            this.labelRectHeight.Name = "labelRectHeight";
            this.labelRectHeight.Size = new System.Drawing.Size(124, 20);
            this.labelRectHeight.TabIndex = 2;
            this.labelRectHeight.Text = "Rectangle Height";
            // 
            // numericUpDownRectHeight
            // 
            this.numericUpDownRectHeight.Location = new System.Drawing.Point(201, 123);
            this.numericUpDownRectHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownRectHeight.Name = "numericUpDownRectHeight";
            this.numericUpDownRectHeight.Size = new System.Drawing.Size(101, 27);
            this.numericUpDownRectHeight.TabIndex = 1;
            // 
            // labelRectWidth
            // 
            this.labelRectWidth.AutoSize = true;
            this.labelRectWidth.Location = new System.Drawing.Point(3, 95);
            this.labelRectWidth.Name = "labelRectWidth";
            this.labelRectWidth.Size = new System.Drawing.Size(119, 20);
            this.labelRectWidth.TabIndex = 2;
            this.labelRectWidth.Text = "Rectangle Width";
            // 
            // numericUpDownRectWidth
            // 
            this.numericUpDownRectWidth.Location = new System.Drawing.Point(201, 84);
            this.numericUpDownRectWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownRectWidth.Name = "numericUpDownRectWidth";
            this.numericUpDownRectWidth.Size = new System.Drawing.Size(101, 27);
            this.numericUpDownRectWidth.TabIndex = 1;
            // 
            // pictureBoxWorkingImage
            // 
            this.pictureBoxWorkingImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxWorkingImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxWorkingImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxWorkingImage.Name = "pictureBoxWorkingImage";
            this.pictureBoxWorkingImage.Size = new System.Drawing.Size(643, 600);
            this.pictureBoxWorkingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWorkingImage.TabIndex = 0;
            this.pictureBoxWorkingImage.TabStop = false;
            this.pictureBoxWorkingImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxWorkingImage_Paint);
            this.pictureBoxWorkingImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWorkingImage_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Dataset Creation Tool";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorkingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxWorkingImage;
        private System.Windows.Forms.Label labelRectHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownRectHeight;
        private System.Windows.Forms.Label labelRectWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownRectWidth;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.TextBox textBoxClass;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

