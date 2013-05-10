using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace StarStacker
{
    

    class ThreadWorker
    {
        public delegate void pictureProcessedEventHandler(object sender, PictureProcessedEventArgs e);
        public event pictureProcessedEventHandler pictureProcessed;

        public delegate void pictureProcessingCompleteEventHandler(object sender, EventArgs e);
        public event pictureProcessingCompleteEventHandler pictureProcessingComplete;

        const double stellarDay = 86164.098903691;

        public void startStackThread(List<string> filesList)
        {
            Thread worker = new Thread(new ParameterizedThreadStart(stackPhotos));
            worker.Start(filesList);
        }

        public void startRotateThread(List<string> filesList, PointF rotationPoint)
        {
            Thread worker = new Thread(new ParameterizedThreadStart(rotatePhotos));
            worker.Start(new object[]{filesList, rotationPoint});
        }

        private void stackPhotos(object filesList)
        {
            List<string> fileNames = (List<string>)filesList;
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(fileNames.First());

            foreach (string filename in fileNames)
            {
                Image<Bgr, Byte> nextImage = new Image<Bgr, byte>(filename);
                image = image.Max(nextImage);
                nextImage.Dispose();
                pictureProcessed(this, new PictureProcessedEventArgs(image));
            }
            pictureProcessingComplete(this, new EventArgs());
        }

        private void rotatePhotos(object parameters)
        {
            object[] paramsArray = (object[])parameters;
            List<string> fileNames = (List<string>)paramsArray[0];
            PointF rotationCenter = (PointF)paramsArray[1];
            Bitmap referencePic = new Bitmap(fileNames.First());
            Image<Bgr, Byte> referenceImage = new Image<Bgr, Byte>(referencePic);

            byte[] timeTakenRaw = referencePic.GetPropertyItem(36867).Value;
            string timeTaken = System.Text.Encoding.ASCII.GetString(timeTakenRaw, 0, timeTakenRaw.Length - 1);
            DateTime referenceTime = DateTime.ParseExact(timeTaken, "yyyy:MM:d H:m:s", System.Globalization.CultureInfo.InvariantCulture);
            
            referencePic.Dispose();

            Bgr background = new Bgr(0, 0, 0);

            foreach (string filename in fileNames)
            {
                Bitmap currentPic = new Bitmap(filename);
                timeTakenRaw = currentPic.GetPropertyItem(36867).Value;
                timeTaken = System.Text.Encoding.ASCII.GetString(timeTakenRaw, 0, timeTakenRaw.Length - 1);
                DateTime date = DateTime.ParseExact(timeTaken, "yyyy:MM:d H:m:s", System.Globalization.CultureInfo.InvariantCulture);
                double secondsShift = (date - referenceTime).TotalSeconds;
                double rotationAngle = secondsShift / stellarDay * 360;
                RotationMatrix2D<double> rotationMatrix = new RotationMatrix2D<double>(rotationCenter, -rotationAngle, 1);

                using (Image<Bgr, Byte> rotatedImage = new Image<Bgr, Byte>(currentPic))
                {
                    referenceImage = referenceImage.Max(rotatedImage.WarpAffine<double>(rotationMatrix, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC, Emgu.CV.CvEnum.WARP.CV_WARP_FILL_OUTLIERS, background));
                }
                pictureProcessed(this, new PictureProcessedEventArgs(referenceImage));
                currentPic.Dispose();
            }
            pictureProcessingComplete(this, new EventArgs());
        }

        public class PictureProcessedEventArgs : EventArgs
        {
            private Image<Bgr, Byte> _processedImage;

            public PictureProcessedEventArgs(Image<Bgr, Byte> image)
            {
                _processedImage = image;
            }

            public Image<Bgr, Byte> processedImage
            {
                get{return _processedImage;}
            }
            
        }
    }
}
