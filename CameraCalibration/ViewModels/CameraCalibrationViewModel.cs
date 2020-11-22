using CameraCalibration.Helper;
using CameraClibration.Helper;
using OpenCvSharp.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CameraCalibration.ViewModels
{
    public class CameraCalibrationViewModel : BindableBase
    {

        private float crossPosX;
        public float CrossPosX
        {
            get { return crossPosX; }
            set { SetProperty(ref crossPosX, value); }
        }

        private float crossPosY;
        public float CrossPosY
        {
            get { return crossPosY; }
            set { SetProperty(ref crossPosY, value); }
        }

        private BitmapImage imageData;
        public BitmapImage ImageData
        {
            get { return imageData; }
            set { SetProperty(ref imageData, value); }
        }

        private Dictionary<string, double> cam;
        public Dictionary<string, double> Cam
        {
            get { return cam; }
            set { SetProperty(ref cam, value); }
        }

        private ChessBoardImage chessboardImage;

        public DelegateCommand OpenImageCommand { get; set; }
        public DelegateCommand AnalyseImageCommand { get; set;}
        public DelegateCommand AddCrossCommand { get; set; }
        public CameraCalibrationViewModel()
        {
            OpenImageCommand = new DelegateCommand(OpenImageHandler);
            AnalyseImageCommand = new DelegateCommand(AnalyseImageHandler);
            AddCrossCommand = new DelegateCommand(AddCrossHandler);

            CrossPosX = 0;
            CrossPosY = 0;
        }

        private void OpenImageHandler()
        {
            chessboardImage = new ChessBoardImage();
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }

        private void AnalyseImageHandler()
        {
            var pt = new Point(0,0);
            var sq = new Point(16, 16);
            var size = 520;
            Cam = ChessBoard.Find(chessboardImage.ImageMat, sq, pt, size);
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }

        private void AddCrossHandler()
        {
            OpenCvSharp.Point center = new OpenCvSharp.Point(1000*CrossPosX / Cam["X um / px"], 1000 * CrossPosY / Cam["Y um / px"]);
            Crosshair.Draw(chessboardImage.ImageMat, center);
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }
    }
}
