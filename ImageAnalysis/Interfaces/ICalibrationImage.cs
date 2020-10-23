using OpenCvSharp;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Interfaces
{
    public interface ICalibrationImage
    {
        Mat ImageMat { get; set; }
        void Substract(Mat img);
        BitmapImage GetBitmapImage();
    }
}