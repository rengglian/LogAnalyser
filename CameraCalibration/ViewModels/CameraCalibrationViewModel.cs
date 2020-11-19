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

        private ChessBoardImage chessboardImage;
        public ChessBoardImage ChessboardImage
        {
            get { return chessboardImage; }
            set { SetProperty(ref chessboardImage, value); }
        }

        public DelegateCommand OpenImageCommand { get; set; }
        public DelegateCommand AnalyseImageCommand { get; set;}
        public CameraCalibrationViewModel()
        {
            OpenImageCommand = new DelegateCommand(OpenImageHandler);
            AnalyseImageCommand = new DelegateCommand(AnalyseImageHandler);
        }

        private void OpenImageHandler()
        {
            ChessboardImage = new ChessBoardImage();
            ImageData = ImageTypeConverter.Convert(ChessboardImage.ImageMat.ToBitmap());
        }

        private void AnalyseImageHandler()
        {
            var pt = new Point(0,0);
            var sq = new Point(8, 8);
            var size = 230;
            ChessBoard.Find(ChessboardImage.ImageMat, sq, pt, size);
            ImageData = ImageTypeConverter.Convert(ChessboardImage.ImageMat.ToBitmap());
        }
    }
}
