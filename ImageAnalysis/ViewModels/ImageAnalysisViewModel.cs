using ImageAnalysis.Helper;
using ImageAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Collections.ObjectModel;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand OpenImageCommand { get; set; }

        public ICalibrationImage SourceImage { get; set; } = new CalibrationImage();

        public ICalibrationImage BgSourceImage { get; set; } = new CalibrationImage();

        public ObservableCollection<IImageList> ImgList { get; set; } = new ObservableCollection<IImageList>();

        public ImageAnalysisViewModel()
        {

            this.OpenImageCommand = new BaseCommand(true, OpenImageHandler);
        }

        private void OpenImageHandler()
        {
            this.SourceImage.OpenSrcImageMat();

            this.ImgList.Add(new ImageList("test", BitmapToBitmapImage.Convert(this.SourceImage.ImageMat.ToBitmap())));
            
            this.OnPropertyChanged(nameof(this.ImgList));
            

        }

    }
}
