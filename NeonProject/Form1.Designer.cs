using System.Windows.Forms;

namespace NeonProject
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chooseImage = new System.Windows.Forms.Button();
            this.imagePathTextBox = new System.Windows.Forms.TextBox();
            this.chosenImage = new System.Windows.Forms.PictureBox();
            this.trackBarThreads = new System.Windows.Forms.TrackBar();
            this.threadLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.pictureBoxNeon = new System.Windows.Forms.PictureBox();
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelOriginal = new System.Windows.Forms.Label();
            this.applyNeon = new System.Windows.Forms.Button();
            this.t1Asm = new System.Windows.Forms.Label();
            this.t2Asm = new System.Windows.Forms.Label();
            this.t4Asm = new System.Windows.Forms.Label();
            this.t8Asm = new System.Windows.Forms.Label();
            this.t16Asm = new System.Windows.Forms.Label();
            this.t32Asm = new System.Windows.Forms.Label();
            this.t64Asm = new System.Windows.Forms.Label();
            this.TestsButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chosenImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreads)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNeon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chooseImage);
            this.flowLayoutPanel1.Controls.Add(this.imagePathTextBox);
            this.flowLayoutPanel1.Controls.Add(this.chosenImage);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 417);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 222);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // chooseImage
            // 
            this.chooseImage.Location = new System.Drawing.Point(3, 3);
            this.chooseImage.Name = "chooseImage";
            this.chooseImage.Size = new System.Drawing.Size(197, 23);
            this.chooseImage.TabIndex = 3;
            this.chooseImage.Text = "Choose Image";
            this.chooseImage.UseVisualStyleBackColor = true;
            this.chooseImage.Click += new System.EventHandler(this.chooseImage_Click);
            // 
            // imagePathTextBox
            // 
            this.imagePathTextBox.Location = new System.Drawing.Point(3, 32);
            this.imagePathTextBox.Name = "imagePathTextBox";
            this.imagePathTextBox.Size = new System.Drawing.Size(197, 20);
            this.imagePathTextBox.TabIndex = 2;
            // 
            // chosenImage
            // 
            this.chosenImage.Location = new System.Drawing.Point(3, 58);
            this.chosenImage.Name = "chosenImage";
            this.chosenImage.Size = new System.Drawing.Size(197, 155);
            this.chosenImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.chosenImage.TabIndex = 1;
            this.chosenImage.TabStop = false;
            // 
            // trackBarThreads
            // 
            this.trackBarThreads.Location = new System.Drawing.Point(3, 16);
            this.trackBarThreads.Maximum = 0;
            this.trackBarThreads.Name = "trackBarThreads";
            this.trackBarThreads.Size = new System.Drawing.Size(284, 45);
            this.trackBarThreads.TabIndex = 1;
            this.trackBarThreads.Scroll += new System.EventHandler(this.trackBarThreads_Scroll);
            // 
            // threadLabel
            // 
            this.threadLabel.Location = new System.Drawing.Point(3, 0);
            this.threadLabel.Name = "threadLabel";
            this.threadLabel.Size = new System.Drawing.Size(284, 13);
            this.threadLabel.TabIndex = 2;
            this.threadLabel.Text = "Number of Threads";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.threadLabel);
            this.flowLayoutPanel2.Controls.Add(this.trackBarThreads);
            this.flowLayoutPanel2.Controls.Add(this.timeLabel);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(471, 417);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(287, 113);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(3, 64);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(33, 13);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "Time:";
            // 
            // pictureBoxNeon
            // 
            this.pictureBoxNeon.Location = new System.Drawing.Point(406, 48);
            this.pictureBoxNeon.Name = "pictureBoxNeon";
            this.pictureBoxNeon.Size = new System.Drawing.Size(352, 307);
            this.pictureBoxNeon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxNeon.TabIndex = 4;
            this.pictureBoxNeon.TabStop = false;
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.Location = new System.Drawing.Point(27, 48);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(345, 307);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 5;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(515, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "NEON EFFECT";
            // 
            // labelOriginal
            // 
            this.labelOriginal.AutoSize = true;
            this.labelOriginal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelOriginal.Font = new System.Drawing.Font("Impact", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOriginal.Location = new System.Drawing.Point(130, 9);
            this.labelOriginal.Name = "labelOriginal";
            this.labelOriginal.Size = new System.Drawing.Size(120, 36);
            this.labelOriginal.TabIndex = 7;
            this.labelOriginal.Text = "ORIGINAL";
            // 
            // applyNeon
            // 
            this.applyNeon.Location = new System.Drawing.Point(338, 433);
            this.applyNeon.Name = "applyNeon";
            this.applyNeon.Size = new System.Drawing.Size(75, 23);
            this.applyNeon.TabIndex = 8;
            this.applyNeon.Text = "Convert";
            this.applyNeon.UseVisualStyleBackColor = true;
            this.applyNeon.Click += new System.EventHandler(this.applyNeon_Click);
            // 
            // t1Asm
            // 
            this.t1Asm.AutoSize = true;
            this.t1Asm.Location = new System.Drawing.Point(335, 506);
            this.t1Asm.Name = "t1Asm";
            this.t1Asm.Size = new System.Drawing.Size(35, 13);
            this.t1Asm.TabIndex = 9;
            this.t1Asm.Text = "label1";
            // 
            // t2Asm
            // 
            this.t2Asm.AutoSize = true;
            this.t2Asm.Location = new System.Drawing.Point(337, 519);
            this.t2Asm.Name = "t2Asm";
            this.t2Asm.Size = new System.Drawing.Size(35, 13);
            this.t2Asm.TabIndex = 10;
            this.t2Asm.Text = "label1";
            // 
            // t4Asm
            // 
            this.t4Asm.AutoSize = true;
            this.t4Asm.Location = new System.Drawing.Point(337, 532);
            this.t4Asm.Name = "t4Asm";
            this.t4Asm.Size = new System.Drawing.Size(35, 13);
            this.t4Asm.TabIndex = 11;
            this.t4Asm.Text = "label1";
            // 
            // t8Asm
            // 
            this.t8Asm.AutoSize = true;
            this.t8Asm.Location = new System.Drawing.Point(337, 545);
            this.t8Asm.Name = "t8Asm";
            this.t8Asm.Size = new System.Drawing.Size(35, 13);
            this.t8Asm.TabIndex = 12;
            this.t8Asm.Text = "label1";
            // 
            // t16Asm
            // 
            this.t16Asm.AutoSize = true;
            this.t16Asm.Location = new System.Drawing.Point(335, 558);
            this.t16Asm.Name = "t16Asm";
            this.t16Asm.Size = new System.Drawing.Size(35, 13);
            this.t16Asm.TabIndex = 13;
            this.t16Asm.Text = "label1";
            // 
            // t32Asm
            // 
            this.t32Asm.AutoSize = true;
            this.t32Asm.Location = new System.Drawing.Point(337, 571);
            this.t32Asm.Name = "t32Asm";
            this.t32Asm.Size = new System.Drawing.Size(35, 13);
            this.t32Asm.TabIndex = 14;
            this.t32Asm.Text = "label1";
            // 
            // t64Asm
            // 
            this.t64Asm.AutoSize = true;
            this.t64Asm.Location = new System.Drawing.Point(337, 584);
            this.t64Asm.Name = "t64Asm";
            this.t64Asm.Size = new System.Drawing.Size(35, 13);
            this.t64Asm.TabIndex = 15;
            this.t64Asm.Text = "label1";
            // 
            // TestsButton
            // 
            this.TestsButton.Location = new System.Drawing.Point(340, 480);
            this.TestsButton.Name = "TestsButton";
            this.TestsButton.Size = new System.Drawing.Size(75, 23);
            this.TestsButton.TabIndex = 16;
            this.TestsButton.Text = "Run tests";
            this.TestsButton.UseVisualStyleBackColor = true;
            this.TestsButton.Click += new System.EventHandler(this.TestsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.TestsButton);
            this.Controls.Add(this.t64Asm);
            this.Controls.Add(this.t32Asm);
            this.Controls.Add(this.t16Asm);
            this.Controls.Add(this.t8Asm);
            this.Controls.Add(this.t4Asm);
            this.Controls.Add(this.t2Asm);
            this.Controls.Add(this.t1Asm);
            this.Controls.Add(this.applyNeon);
            this.Controls.Add(this.labelOriginal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxOriginal);
            this.Controls.Add(this.pictureBoxNeon);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "NeonApp";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chosenImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreads)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNeon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox imagePathTextBox;
        private System.Windows.Forms.PictureBox chosenImage;
        private System.Windows.Forms.Button chooseImage;
        private System.Windows.Forms.TrackBar trackBarThreads;
        private System.Windows.Forms.Label threadLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.PictureBox pictureBoxNeon;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelOriginal;
        private Button applyNeon;
        private Label t1Asm;
        private Label t2Asm;
        private Label t4Asm;
        private Label t8Asm;
        private Label t16Asm;
        private Label t32Asm;
        private Label t64Asm;
        private Button TestsButton;
    }
}

