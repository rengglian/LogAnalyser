using ImageAnalysis.Helper;
using ImageAnalysis.Interfaces;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Documents;
using System.Collections.Generic;
using ImageAnalysis.ImageProcessing;
using OpenCvSharp;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : BindableBase
    {

        public DelegateCommand OpenImageCommand { get; set; }
        public DelegateCommand OpenSecondImageCommand { get; set; }

        public DelegateCommand SubstractImageCommand { get; set; }

        private List<ICalibrationImage> images;

        private ObservableCollection<Spot> spots;

        public ObservableCollection<Spot> Spots
        {
            get { return spots; }
            set { SetProperty(ref spots, value); }
        }

        private ObservableCollection<IImageList> imgList;
        public ObservableCollection<IImageList> ImgList
        {
            get { return imgList; }
            set { SetProperty(ref imgList, value); }
        }

        private IImageList img;
        public IImageList Img
        {
            get { return img; }
            set { SetProperty(ref img, value); }
        }

        public IImageList SelectedImage
        {
            get { return this.Img; }
            set
            {
                this.Img = value;
            }
        }

        public ImageAnalysisViewModel()
        {
            this.ImgList = new ObservableCollection<IImageList>();
            this.OpenImageCommand = new DelegateCommand(OpenImageHandler);
            this.OpenSecondImageCommand = new DelegateCommand(OpenSecondImageHandler);
            this.SubstractImageCommand = new DelegateCommand(SubstractImageHandler);
            this.images = new List<ICalibrationImage>();
        }

        private void SubstractImageHandler()
        {
            this.images.Add(new CalibrationImage(this.images[0].ImageMat));
            this.images[2].Substract(this.images[1].ImageMat);
            this.ImgList.Add(new ImageList("Substract Image", this.images[2].GetBitmapImage()));
            this.Spots = new ObservableCollection<Spot>();
            var sp = FindObjects.Circles(this.images[2].ImageMat);
            sp.ForEach(sp =>
            {
                this.Spots.Add(sp);
            });
        }

        private void OpenSecondImageHandler()
        {
            this.images.Add(new CalibrationImage());
            //this.SourceImage = new CalibrationImage();
            this.ImgList.Add(new ImageList("Src Image", this.images[1].GetBitmapImage()));
        }

        private void OpenImageHandler()
        {
            this.images.Add(new CalibrationImage());
            //this.SourceImage = new CalibrationImage();
            this.ImgList.Add(new ImageList("Src Image", this.images[0].GetBitmapImage()));

        }

    }
}
