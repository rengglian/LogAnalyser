using ImageAnalysis.Interfaces;
using ImageAnalysis.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper
{
    public class CalibrationImage : ICalibrationImage
    {

        public Mat ImageMat { get; set; }

        public Mat EditedMat { get; set; }

        public BitmapImage EditedBitmap { get; set; } = new BitmapImage();

        public CalibrationImage() {
            this.ImageMat = ImageReader.Read();
        }

        public void Substract(Mat img)
        {
            this.EditedMat = this.ImageMat.Clone();
            Cv2.Subtract(this.ImageMat, img, this.EditedMat);
            this.EditedBitmap = BitmapToBitmapImage.Convert(this.EditedMat.ToBitmap());
            var test = Cv2.HoughCircles(this.EditedMat, HoughMethods.Gradient, 1, 10, 70, 10, 3, 10);
            Console.WriteLine("test");
        }

        public BitmapImage GetBitmapImage()
        {
            return BitmapToBitmapImage.Convert(this.ImageMat.ToBitmap());
        }

    }
}
