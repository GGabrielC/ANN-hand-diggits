namespace WindowsFormsApp1
{
    partial class App
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
            this.TrainNet = new System.Windows.Forms.Button();
            this.ImgIndex = new System.Windows.Forms.NumericUpDown();
            this.ImageIndex = new System.Windows.Forms.Label();
            this.DiggitLabel = new System.Windows.Forms.Label();
            this.DiggitImage = new System.Windows.Forms.PictureBox();
            this.buttunDraw = new System.Windows.Forms.Button();
            this.nudTrainIterations = new System.Windows.Forms.NumericUpDown();
            this.nudTrainIterationSize = new System.Windows.Forms.NumericUpDown();
            this.labelTrainIterations = new System.Windows.Forms.Label();
            this.labelTrainIterationSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiggitImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainIterationSize)).BeginInit();
            this.SuspendLayout();
            // 
            // TrainNet
            // 
            this.TrainNet.Location = new System.Drawing.Point(224, 20);
            this.TrainNet.Name = "TrainNet";
            this.TrainNet.Size = new System.Drawing.Size(86, 72);
            this.TrainNet.TabIndex = 1;
            this.TrainNet.Text = "TrainNet";
            this.TrainNet.UseVisualStyleBackColor = true;
            this.TrainNet.Click += new System.EventHandler(this.button_TrainNet_Click);
            // 
            // ImgIndex
            // 
            this.ImgIndex.Location = new System.Drawing.Point(34, 146);
            this.ImgIndex.Name = "ImgIndex";
            this.ImgIndex.Size = new System.Drawing.Size(79, 22);
            this.ImgIndex.TabIndex = 2;
            this.ImgIndex.ValueChanged += new System.EventHandler(this.ImgIndex_ValueChanged);
            // 
            // ImageIndex
            // 
            this.ImageIndex.AutoSize = true;
            this.ImageIndex.Location = new System.Drawing.Point(34, 126);
            this.ImageIndex.Name = "ImageIndex";
            this.ImageIndex.Size = new System.Drawing.Size(79, 17);
            this.ImageIndex.TabIndex = 3;
            this.ImageIndex.Text = "ImageIndex";
            this.ImageIndex.Click += new System.EventHandler(this.label1_Click);
            // 
            // DiggitLabel
            // 
            this.DiggitLabel.AutoSize = true;
            this.DiggitLabel.Location = new System.Drawing.Point(31, 171);
            this.DiggitLabel.Name = "DiggitLabel";
            this.DiggitLabel.Size = new System.Drawing.Size(55, 17);
            this.DiggitLabel.TabIndex = 4;
            this.DiggitLabel.Text = "Label:  ";
            // 
            // DiggitImage
            // 
            this.DiggitImage.Location = new System.Drawing.Point(73, 191);
            this.DiggitImage.Name = "DiggitImage";
            this.DiggitImage.Size = new System.Drawing.Size(97, 97);
            this.DiggitImage.TabIndex = 5;
            this.DiggitImage.TabStop = false;
            this.DiggitImage.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buttunDraw
            // 
            this.buttunDraw.Location = new System.Drawing.Point(131, 146);
            this.buttunDraw.Name = "buttunDraw";
            this.buttunDraw.Size = new System.Drawing.Size(75, 23);
            this.buttunDraw.TabIndex = 6;
            this.buttunDraw.Text = "Draw";
            this.buttunDraw.UseVisualStyleBackColor = true;
            this.buttunDraw.Click += new System.EventHandler(this.buttun_Draw_Click);
            // 
            // nudTrainIterations
            // 
            this.nudTrainIterations.Location = new System.Drawing.Point(131, 20);
            this.nudTrainIterations.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudTrainIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTrainIterations.Name = "nudTrainIterations";
            this.nudTrainIterations.Size = new System.Drawing.Size(87, 22);
            this.nudTrainIterations.TabIndex = 7;
            this.nudTrainIterations.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudTrainIterationSize
            // 
            this.nudTrainIterationSize.Location = new System.Drawing.Point(131, 70);
            this.nudTrainIterationSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTrainIterationSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTrainIterationSize.Name = "nudTrainIterationSize";
            this.nudTrainIterationSize.Size = new System.Drawing.Size(87, 22);
            this.nudTrainIterationSize.TabIndex = 8;
            this.nudTrainIterationSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelTrainIterations
            // 
            this.labelTrainIterations.AutoSize = true;
            this.labelTrainIterations.Location = new System.Drawing.Point(59, 20);
            this.labelTrainIterations.Name = "labelTrainIterations";
            this.labelTrainIterations.Size = new System.Drawing.Size(66, 17);
            this.labelTrainIterations.TabIndex = 9;
            this.labelTrainIterations.Text = "Iterations";
            // 
            // labelTrainIterationSize
            // 
            this.labelTrainIterationSize.AutoSize = true;
            this.labelTrainIterationSize.Location = new System.Drawing.Point(39, 70);
            this.labelTrainIterationSize.Name = "labelTrainIterationSize";
            this.labelTrainIterationSize.Size = new System.Drawing.Size(86, 17);
            this.labelTrainIterationSize.TabIndex = 10;
            this.labelTrainIterationSize.Text = "iterationSize";
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTrainIterationSize);
            this.Controls.Add(this.labelTrainIterations);
            this.Controls.Add(this.nudTrainIterationSize);
            this.Controls.Add(this.nudTrainIterations);
            this.Controls.Add(this.buttunDraw);
            this.Controls.Add(this.DiggitImage);
            this.Controls.Add(this.DiggitLabel);
            this.Controls.Add(this.ImageIndex);
            this.Controls.Add(this.ImgIndex);
            this.Controls.Add(this.TrainNet);
            this.Name = "App";
            this.Text = "App";
            this.Load += new System.EventHandler(this.App_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImgIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiggitImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainIterationSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button TrainNet;
        private System.Windows.Forms.NumericUpDown ImgIndex;
        private System.Windows.Forms.Label ImageIndex;
        private System.Windows.Forms.Label DiggitLabel;
        private System.Windows.Forms.PictureBox DiggitImage;
        private System.Windows.Forms.Button buttunDraw;
        private System.Windows.Forms.NumericUpDown nudTrainIterations;
        private System.Windows.Forms.NumericUpDown nudTrainIterationSize;
        private System.Windows.Forms.Label labelTrainIterations;
        private System.Windows.Forms.Label labelTrainIterationSize;
    }
}

