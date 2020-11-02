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
            ImageMat = ImageReader.Read();
            if (ImageMat.Channels() > 1) FlipX();
        }

        public CalibrationImage(Mat img)
        {
            ImageMat = img.Clone();
        }

        public void Substract(Mat img)
        {
            Cv2.Absdiff(ImageMat, img, ImageMat);
        }

        public void Blur()
        {
            Cv2.GaussianBlur(ImageMat, ImageMat, new Size(7, 7), 20);
        }

        private void FlipX()
        {
            Cv2.Flip(ImageMat, ImageMat, FlipMode.Y);
        }

        public BitmapImage GetBitmapImage()
        {
            return BitmapToBitmapImage.Convert(ImageMat.ToBitmap());
        }
    }
}
