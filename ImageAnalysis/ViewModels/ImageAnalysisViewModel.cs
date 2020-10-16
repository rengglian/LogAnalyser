using ImageAnalysis.Helper;
using ImageAnalysis.Interfaces;
using OpenCvSharp.Extensions;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : BindableBase
    {

        public DelegateCommand OpenImageCommand { get; set; }

        public ICalibrationImage SourceImage { get; set; } = new CalibrationImage();

        public ICalibrationImage BgSourceImage { get; set; } = new CalibrationImage();

        private ObservableCollection<IImageList> imgList;
        public ObservableCollection<IImageList> ImgList
        {
            get { return imgList; }
            set { SetProperty(ref imgList, value); }
        }

        public ImageAnalysisViewModel()
        {
            ImgList = new ObservableCollection<IImageList>();
            this.OpenImageCommand = new DelegateCommand(OpenImageHandler);
        }

        private void OpenImageHandler()
        {
            this.SourceImage.OpenSrcImageMat();

            this.ImgList.Add(new ImageList("test", BitmapToBitmapImage.Convert(this.SourceImage.ImageMat.ToBitmap())));

        }

    }
}
