using OpenCvSharp;

namespace ImageAnalysis.Interfaces
{
    public interface ICalibrationImage
    {
        Mat BgImageMat { get; set; }
        Mat ImageMat { get; set; }

        void OpenBgImageMat();
        void OpenSrcImageMat();
    }
}