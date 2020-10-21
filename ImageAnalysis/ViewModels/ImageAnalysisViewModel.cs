using ImageAnalysis.Helper;
using ImageAnalysis.Interfaces;
using OpenCvSharp.Extensions;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : BindableBase
    {

        public DelegateCommand OpenImageCommand { get; set; }
        public DelegateCommand OpenSecondImageCommand { get; set; }

        public DelegateCommand SubstractImageCommand { get; set; }

        public ICalibrationImage sourceImage;
        public ICalibrationImage SourceImage
        {
            get { return sourceImage; }
            set { SetProperty(ref sourceImage, value); }
        }

        public ICalibrationImage BgSourceImage { get; set; }

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
            this.OpenSecondImageCommand = new DelegateCommand(OpenSecondImageHandler);
            this.SubstractImageCommand = new DelegateCommand(SubstractImageHandler);
        }

        private void SubstractImageHandler()
        {
            this.SourceImage.Substract(this.BgSourceImage.ImageMat);
            this.ImgList.Add(new ImageList("Substract Image", this.SourceImage.EditedBitmap));

        }

        private void OpenSecondImageHandler()
        {
            this.BgSourceImage = new CalibrationImage();
            this.ImgList.Add(new ImageList("Background Image", this.BgSourceImage.ImageBitmap));
        }

        private void OpenImageHandler()
        {
            this.SourceImage = new CalibrationImage();
            this.ImgList.Add(new ImageList("Src Image", this.SourceImage.ImageBitmap));

        }

    }
}
