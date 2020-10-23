using ImageAnalysis.ImageProcessing;
using ImageAnalysis.Interfaces;
using ImageAnalysis.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper
{
    public class CalibrationImage : ICalibrationImage
    {

        public Mat ImageMat { get; set; }

        public Mat EditedMat { get; set; }

        public BitmapImage EditedBitmap { get; set; } = new BitmapImage();

        public List<Spot> Spots { get; set; } = new List<Spot>();

        public CalibrationImage() {
            this.ImageMat = ImageReader.Read();
        }

        public void Substract(Mat img)
        {
            this.EditedMat = this.ImageMat.Clone();
            Cv2.Subtract(this.ImageMat, img, this.EditedMat);
            this.EditedBitmap = BitmapToBitmapImage.Convert(this.EditedMat.ToBitmap());
            Spots = FindObjects.Circles(this.EditedMat);
        }

        public BitmapImage GetBitmapImage()
        {
            return BitmapToBitmapImage.Convert(this.ImageMat.ToBitmap());
        }

    }
}
