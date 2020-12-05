using CameraCalibration.Helper;
using CameraClibration.Helper;
using Infrastructure.Helper;
using Infrastructure.Prism.Events;
using OpenCvSharp.Extensions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CameraCalibration.ViewModels
{
    public class CameraCalibrationViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private bool exportRawCorners;
        public bool ExportRawCorners
        {
            get { return exportRawCorners; }
            set { SetProperty(ref exportRawCorners, value); }
        }

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

        public Dictionary<string, Options> analyseOpt;
        public Dictionary<string, Options> AnalyseOpt
        {
            get { return analyseOpt; }
            set { SetProperty(ref analyseOpt, value); }
        }

        private ChessBoardImage chessboardImage;

        public DelegateCommand OpenImageCommand { get; set; }
        public DelegateCommand AnalyseImageCommand { get; set;}
        public DelegateCommand AddCrossCommand { get; set; }
        public CameraCalibrationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AnalyseOpt = AnalyseOptions.Get();

            OpenImageCommand = new DelegateCommand(OpenImageHandler);
            AnalyseImageCommand = new DelegateCommand(AnalyseImageHandler);
            AddCrossCommand = new DelegateCommand(AddCrossHandler);

            CrossPosX = 0;
            CrossPosY = 0;
            ExportRawCorners = false;

        }

        private void OpenImageHandler()
        {
            chessboardImage = new ChessBoardImage();
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }

        private void AnalyseImageHandler()
        {
            var pt = new Point(AnalyseOpt["Center X"].Value, AnalyseOpt["Center Y"].Value);
            var sq = new Point(AnalyseOpt["Features X"].Value, AnalyseOpt["Features Y"].Value); 
            var size = new Point(AnalyseOpt["ROI Width"].Value, AnalyseOpt["ROI Height"].Value);
            Cam = ChessBoard.Find(chessboardImage.ImageMat, sq, pt, size, ExportRawCorners);
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
            var json = JsonSerializer.Serialize(Cam);
            _eventAggregator.GetEvent<CameraCalibrationSendEvent>().Publish(json);
        }

        private void AddCrossHandler()
        {
            OpenCvSharp.Point center = new OpenCvSharp.Point(1000*CrossPosX / Cam["X um / px"], 1000 * CrossPosY / Cam["Y um / px"]);
            Crosshair.Draw(chessboardImage.ImageMat, center);
            ImageData = ImageTypeConverter.Convert(chessboardImage.ImageMat.ToBitmap());
        }
    }
}
