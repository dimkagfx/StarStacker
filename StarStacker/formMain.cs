using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace StarStacker
{
    public partial class formMain : Form
    {
        PointF rotationCenter = new PointF(0,0);
        ThreadWorker worker;

        public formMain()
        {
            InitializeComponent();

            worker = new ThreadWorker();
            worker.pictureProcessed += new ThreadWorker.pictureProcessedEventHandler(worker_pictureProcessed);
            worker.pictureProcessingComplete += new ThreadWorker.pictureProcessingCompleteEventHandler(worker_pictureProcessingComplete);
        }

        

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog initialCatalogue = new FolderBrowserDialog();
            if (initialCatalogue.ShowDialog() == DialogResult.OK)
            {
                lstPictures.Items.Clear();

                foreach (string filename in Directory.GetFiles(initialCatalogue.SelectedPath, "*.jpg"))
                    lstPictures.Items.Add(filename);

                if (lstPictures.Items.Count == 0)
                    return;

                Image<Bgr, Byte> image = new Image<Bgr, Byte>(lstPictures.Items[0].ToString());
                
                float ratio = (float)image.Width / (float)image.Height;
                imgCanvas.Height = (int)(imgCanvas.Width / ratio);
                imgCanvas.Image = image;
            }

        }

        private void imgCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            int offsetX = (int)(e.Location.X / imgCanvas.ZoomScale);
            int offsetY = (int)(e.Location.Y / imgCanvas.ZoomScale);
            int horizontalScrollBarValue = imgCanvas.HorizontalScrollBar.Visible ? (int)imgCanvas.HorizontalScrollBar.Value : 0;
            int verticalScrollBarValue = imgCanvas.VerticalScrollBar.Visible ? (int)imgCanvas.VerticalScrollBar.Value : 0;
            lblSelectedPoint.Text = "Selected point : " + Convert.ToString(offsetX + horizontalScrollBarValue) + "." + Convert.ToString(offsetY + verticalScrollBarValue);
            rotationCenter = new PointF(-offsetX + horizontalScrollBarValue, offsetY + verticalScrollBarValue);
        }

        private void btnStack_Click(object sender, EventArgs e)
        {
            List<string> fileNames = new List<string>();

            foreach (string filename in lstPictures.Items)
                fileNames.Add(filename);

            
            worker.startStackThread(fileNames);
            setButtonState(false);
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            List<string> fileNames = new List<string>();

            foreach (string filename in lstPictures.Items)
                fileNames.Add(filename);

            worker.startRotateThread(fileNames,rotationCenter);
            setButtonState(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                saveJpeg(saveFile.FileName, imgCanvas.Image.Bitmap, 100);
            }
            MessageBox.Show("Save successful");
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality

            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        private void setButtonState(bool state)
        {
            if (btnStack.InvokeRequired)
            {
                this.Invoke((MethodInvoker) delegate {
                    setButtonState(state);
                });
            }
            else
            {
                btnOpen.Enabled = state;
                btnRotate.Enabled = state;
                btnSave.Enabled = state;
                btnStack.Enabled = state;
            }
        }

        void worker_pictureProcessed(object sender, ThreadWorker.PictureProcessedEventArgs e)
        {
            imgCanvas.Image.Dispose();
            imgCanvas.Image = e.processedImage;
        }

        void worker_pictureProcessingComplete(object sender, EventArgs e)
        {
            setButtonState(true);
            MessageBox.Show("Processing complete");
        }
    }
}
