using ImageAnalysis.Helper;
using OpenCvSharp;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Interfaces
{
    public interface ICalibrationImage
    {
        Mat ImageMat { get; set; }
        Mat EditedMat { get; set; }
        BitmapImage EditedBitmap { get; set; }
        public List<Spot> Spots { get; set; }
        void Substract(Mat img);
        BitmapImage GetBitmapImage();
    }
}