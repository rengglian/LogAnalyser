using ImageAnalysis.Helper;
using ImageAnalysis.Interfaces;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Documents;
using System.Collections.Generic;
using ImageAnalysis.ImageProcessing;
using OpenCvSharp;
using System;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : BindableBase
    {

        public DelegateCommand<string> OpenImageCommand { get; set; }
        public DelegateCommand<string> SubstractImageCommand { get; set; }
        public DelegateCommand<string> BlurImageCommand { get; set; }
        public DelegateCommand FindCirclesCommand { get; set; }

        private readonly Dictionary<string, ICalibrationImage> images;

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
            set { this.Img = value; }
        }

        public ImageAnalysisViewModel()
        {
            this.ImgList = new ObservableCollection<IImageList>();
            this.images = new Dictionary<string, ICalibrationImage>();

            this.OpenImageCommand = new DelegateCommand<string>(OpenImageHandler);
            this.SubstractImageCommand = new DelegateCommand<string>(SubstractImageHandler);
            this.BlurImageCommand = new DelegateCommand<string>(BlurImageHandler);
            this.FindCirclesCommand = new DelegateCommand(FindCirclesHandler);

        }

        private void FindCirclesHandler()
        {
            this.Spots = new ObservableCollection<Spot>();
            var sp = FindObjects.Circles(this.images[img.Title].ImageMat);
            sp.ForEach(sp =>
            {
                this.Spots.Add(sp);
            });
        }

        private void BlurImageHandler(string src)
        {
            if (!this.images.ContainsKey(src))
            {
                this.images.Add(src, new CalibrationImage(this.images[img.Title].ImageMat));
            }
            else
            {
                this.images[src] = new CalibrationImage(this.images[img.Title].ImageMat);
            }
            this.images[src].Blur();
            this.ImgList.Add(new ImageList(src, this.images[src].GetBitmapImage()));
        }

        private void SubstractImageHandler(string src)
        {
            if (!this.images.ContainsKey(src))
            {
                this.images.Add(src, new CalibrationImage(this.images["Source"].ImageMat));
            }
            else
            {
                this.images[src] = new CalibrationImage(this.images["Source"].ImageMat);
            }
            this.images[src].Substract(this.images["Background"].ImageMat);
            this.ImgList.Add(new ImageList(src, this.images[src].GetBitmapImage()));
        }

        private void OpenImageHandler(string src)
        {
            if (!this.images.ContainsKey(src))
            {
                this.images.Add(src, new CalibrationImage());
            }
            else
            {
                this.images[src] = new CalibrationImage();
            }
            //this.SourceImage = new CalibrationImage();
            this.ImgList.Add(new ImageList(src, this.images[src].GetBitmapImage()));
        }

    }
}
