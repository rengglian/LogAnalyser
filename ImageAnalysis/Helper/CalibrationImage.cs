using ImageAnalysis.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper
{
    public class CalibrationImage : ICloneable
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public BitmapImage ImageData { get; set; } = new BitmapImage();
        public Mat ImageMat { get; set; }

        public CalibrationImage() {
            var image = ImageReader.Read();
            if (image != null)
            {
                ImageMat = image.ImageMat;
                if (ImageMat.Channels() > 1) FlipX();
                Title = image.FileName;
                Description = "From File";
                Update();
            }
        }

        public void Update()
        {
            ImageData = ImageTypeConverter.Convert(ImageMat.ToBitmap());
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return Title + " - " + Description;
        }

        public CalibrationImage(Mat img)
        {
            ImageMat = img.Clone();
        }

        public void Substract(Mat img)
        {
            Cv2.Absdiff(ImageMat, img, ImageMat);
            Update();
        }

        public void Blur()
        {
            Cv2.GaussianBlur(ImageMat, ImageMat, new Size(7, 7), 20);
            Update();
        }

        private void FlipX()
        {
            Cv2.Flip(ImageMat, ImageMat, FlipMode.Y);
        }
    }
}
