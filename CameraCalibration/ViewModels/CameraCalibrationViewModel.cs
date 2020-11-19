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
        public CameraCalibrationViewModel()
        {
            OpenImageCommand = new DelegateCommand(OpenImageHandler);
            AnalyseImageCommand = new DelegateCommand(AnalyseImageHandler);
        }

        private void OpenImageHandler()
        {
            chessboardImage = new ChessBoardImage();
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }

        private void AnalyseImageHandler()
        {
            var pt = new Point(0,0);
            var sq = new Point(8, 8);
            var size = 290;
            Cam = ChessBoard.Find(chessboardImage.ImageMat, sq, pt, size);
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }
    }
}
