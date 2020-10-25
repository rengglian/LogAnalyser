﻿using ImageAnalysis.Helper;
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
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand OpenTargetCommand { get; private set; }

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

        private IImageList selectedImage { get; set; }
        public IImageList SelectedImage
        {
            get { return this.selectedImage; }
            set { this.selectedImage = value; }
        }

        private IImageList selectedSubA { get; set; }
        public IImageList SelectedSubA
        {
            get { return this.selectedSubA; }
            set { this.selectedSubA = value; }
        }

        private IImageList selectedSubB { get; set; }
        public IImageList SelectedSubB
        {
            get { return this.selectedSubB; }
            set { this.selectedSubB = value; }
        }

        public ImageAnalysisViewModel()
        {
            this.ImgList = new ObservableCollection<IImageList>();
            this.images = new Dictionary<string, ICalibrationImage>();

            this.OpenImageCommand = new DelegateCommand<string>(OpenImageHandler);
            this.SubstractImageCommand = new DelegateCommand<string>(SubstractImageHandler);
            this.BlurImageCommand = new DelegateCommand<string>(BlurImageHandler);
            this.FindCirclesCommand = new DelegateCommand(FindCirclesHandler);
            this.DeleteCommand = new DelegateCommand(DeleteHandler);
            this.OpenTargetCommand = new DelegateCommand(OpenTargetHandler);

        }

        private void OpenTargetHandler()
        {
            
        }

        private void DeleteHandler()
        {

        }

        private void FindCirclesHandler()
        {
            this.Spots = new ObservableCollection<Spot>();
            var sp = FindObjects.Circles(this.images[selectedImage.Title].ImageMat);
            sp.ForEach(sp =>
            {
                this.Spots.Add(sp);
            });
        }

        private void BlurImageHandler(string src)
        {
            var str = src + this.selectedImage.Title;

            if (!this.images.ContainsKey(str))
            {
                this.images.Add(str, new CalibrationImage(this.images[selectedImage.Title].ImageMat));
            }
            else
            {
                this.images[str] = new CalibrationImage(this.images[selectedImage.Title].ImageMat);
            }
            this.images[str].Blur();
            this.ImgList.Add(new ImageList(str, this.images[str].GetBitmapImage()));
        }

        private void SubstractImageHandler(string src)
        {
            if (!this.images.ContainsKey(src))
            {
                this.images.Add(src, new CalibrationImage(this.images[selectedSubA.Title].ImageMat));
            }
            else
            {
                this.images[src] = new CalibrationImage(this.images[selectedSubA.Title].ImageMat);
            }
            this.images[src].Substract(this.images[selectedSubB.Title].ImageMat);
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
