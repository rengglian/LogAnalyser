using ImageAnalysis.Interfaces;
using ImageAnalysis.IO;
using OpenCvSharp;

namespace ImageAnalysis.Helper
{
    public class CalibrationImage : ICalibrationImage
    {

        public Mat ImageMat { get; set; }

        public Mat BgImageMat { get; set; }

        public CalibrationImage() { }

        public void OpenBgImageMat()
        {
            BgImageMat = ImageReader.Read();
        }

        public void OpenSrcImageMat()
        {
            ImageMat = ImageReader.Read();
        }

    }
}
