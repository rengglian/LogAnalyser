using OpenCvSharp;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Interfaces
{
    public interface ICalibrationImage
    {
        Mat ImageMat { get; set; }
        Mat EditedMat { get; set; }
        BitmapImage EditedBitmap { get; set; }
        void Substract(Mat img);
        BitmapImage GetBitmapImage();
    }
}