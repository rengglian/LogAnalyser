﻿using ImageAnalysis.Helper;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using ImageAnalysis.ImageProcessing;
using Prism.Events;
using System.Text.Json;
using Infrastructure.Prism.Events;
using ImageAnalysis.Extension;
using System.Windows;
using System.Windows.Media;
using Infrastructure.Helper;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : BindableBase
    {
        public DelegateCommand OpenImageCommand { get; set; }
        public DelegateCommand<string> SubstractImageCommand { get; set; }
        public DelegateCommand<string> BlurImageCommand { get; set; }
        public DelegateCommand FindCirclesCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand { get; set; }

        private ObservableCollection<CalibrationImage> calibImg;
        public ObservableCollection<CalibrationImage> CalibImg
        {
            get { return calibImg; }
            set { SetProperty(ref calibImg, value); }
        }

        private CalibrationImage selectedSubA { get; set; }
        public CalibrationImage SelectedSubA
        {
            get { return selectedSubA; }
            set { selectedSubA = value; }
        }

        private CalibrationImage selectedSubB { get; set; }
        public CalibrationImage SelectedSubB
        {
            get { return selectedSubB; }
            set { selectedSubB = value; }
        }

        private CalibrationImage selectedImgList { get; set; }
        public CalibrationImage SelectedImgList
        {
            get { return selectedImgList; }
            set { selectedImgList = value; }
        }

        private ObservableCollection<Spot> spots;
        public ObservableCollection<Spot> Spots
        {
            get { return spots; }
            set { SetProperty(ref spots, value); }
        }

        private ObservableCollection<Spot> target;
        public ObservableCollection<Spot> Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }

        private Dictionary<string, double> calibMatrix;
        public Dictionary<string, double> CalibMatrix
        {
            get { return calibMatrix; }
            set { SetProperty(ref calibMatrix, value); }
        }

        public Dictionary<string, Options> props;
        public Dictionary<string, Options> Props
        {
            get { return props; }
            set { SetProperty(ref props, value); }
        }

        public ImageAnalysisViewModel(IEventAggregator eventAggregator)
        {
            CalibImg = new ObservableCollection<CalibrationImage>();

            Props = Properties.Get();

            OpenImageCommand = new DelegateCommand(OpenImageHandler);
            SubstractImageCommand = new DelegateCommand<string>(SubstractImageHandler);
            BlurImageCommand = new DelegateCommand<string>(BlurImageHandler);
            FindCirclesCommand = new DelegateCommand(FindCirclesHandler);
            DeleteCommand = new DelegateCommand<int?>(DeleteHandler);

            eventAggregator.GetEvent<PatternSendEvent>().Subscribe(OnMessageReceived);
        }

        private void OnMessageReceived(string message)
        {
            var json = JsonSerializer.Deserialize<List<Point>>(message);
            var unique_items = new HashSet<Point>(json);
            List<Spot> spots = new List<Spot>();
            foreach (Point pt in unique_items)
            {
                var scaledX = pt.X * 1 / 30.129 + 720 / 2;
                var scaledY = pt.Y * 1 / 30.129 + 576 / 2;
                spots.Add(new Spot(new Point(scaledX, scaledY), 5, Brushes.AliceBlue));
            }

            spots.Add(new Spot(new Point(720 / 2, 576 / 2), 5, Brushes.AliceBlue));

            Target = PatternAnalyser.SortList(spots, false).ToObservableCollection();

            CalibMatrix = AffineMatrix.CalculateMatrix(Spots, Target);
        }
        private void OpenImageHandler()
        {
            CalibImg.Add(new CalibrationImage());
        }

        private void DeleteHandler(int? item)
        {
            if (item >= 0)
            {
                CalibImg.RemoveAt((int)item);
            }
        }

        private void SubstractImageHandler(string src)
        {
            var img = (CalibrationImage)SelectedSubA.Clone();
            img.Substract(SelectedSubB.ImageMat);
            img.Description = src;
            CalibImg.Add(img);
        }

        private void BlurImageHandler(string src)
        {
            var img = (CalibrationImage)SelectedImgList.Clone();
            img.Blur();
            img.Description = src;
            CalibImg.Add(img);
        }

        private void FindCirclesHandler()
        {
            Spots = FindObjects.Circles(SelectedImgList.ImageMat).ToObservableCollection();
        }
    }
}
