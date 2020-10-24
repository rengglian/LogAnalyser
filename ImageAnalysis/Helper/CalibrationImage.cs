using ImageAnalysis.Interfaces;
using ImageAnalysis.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper
{
    public class CalibrationImage : ICalibrationImage
    {

        public Mat ImageMat { get; set; }

        public CalibrationImage() {
            this.ImageMat = ImageReader.Read();
        }

        public CalibrationImage(Mat img)
        {
            this.ImageMat = img.Clone();
        }

        public void Substract(Mat img)
        {
            Cv2.Subtract(this.ImageMat, img, this.ImageMat);
        }

        public void Blur()
        {
            Cv2.GaussianBlur(this.ImageMat, this.ImageMat, new Size(7, 7), 20);
        }

        public BitmapImage GetBitmapImage()
        {
            return BitmapToBitmapImage.Convert(this.ImageMat.ToBitmap());
        }
    }
}
