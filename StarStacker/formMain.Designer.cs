namespace StarStacker
{
    partial class formMain
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
            this.imgCanvas = new Emgu.CV.UI.ImageBox();
            this.lstPictures = new System.Windows.Forms.ListBox();
            this.btnStack = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.lblSelectedPoint = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(10, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(57, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // imgCanvas
            // 
            this.imgCanvas.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.imgCanvas.Location = new System.Drawing.Point(286, 30);
            this.imgCanvas.Name = "imgCanvas";
            this.imgCanvas.Size = new System.Drawing.Size(687, 417);
            this.imgCanvas.TabIndex = 2;
            this.imgCanvas.TabStop = false;
            this.imgCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgCanvas_MouseClick);
            // 
            // lstPictures
            // 
            this.lstPictures.FormattingEnabled = true;
            this.lstPictures.Location = new System.Drawing.Point(10, 105);
            this.lstPictures.Margin = new System.Windows.Forms.Padding(2);
            this.lstPictures.Name = "lstPictures";
            this.lstPictures.Size = new System.Drawing.Size(262, 342);
            this.lstPictures.TabIndex = 3;
            // 
            // btnStack
            // 
            this.btnStack.Location = new System.Drawing.Point(9, 69);
            this.btnStack.Name = "btnStack";
            this.btnStack.Size = new System.Drawing.Size(58, 23);
            this.btnStack.TabIndex = 4;
            this.btnStack.Text = "Stack";
            this.btnStack.UseVisualStyleBackColor = true;
            this.btnStack.Click += new System.EventHandler(this.btnStack_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(73, 69);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(56, 23);
            this.btnRotate.TabIndex = 5;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // lblSelectedPoint
            // 
            this.lblSelectedPoint.AutoSize = true;
            this.lblSelectedPoint.Location = new System.Drawing.Point(13, 53);
            this.lblSelectedPoint.Name = "lblSelectedPoint";
            this.lblSelectedPoint.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedPoint.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(73, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 515);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblSelectedPoint);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnStack);
            this.Controls.Add(this.lstPictures);
            this.Controls.Add(this.imgCanvas);
            this.Controls.Add(this.btnOpen);
            this.Name = "formMain";
            this.Text = "Star Stacker";
            ((System.ComponentModel.ISupportInitialize)(this.imgCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private Emgu.CV.UI.ImageBox imgCanvas;
        private System.Windows.Forms.ListBox lstPictures;
        private System.Windows.Forms.Button btnStack;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Label lblSelectedPoint;
        private System.Windows.Forms.Button btnSave;
    }
}

