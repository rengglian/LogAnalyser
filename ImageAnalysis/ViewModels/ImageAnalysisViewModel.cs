using ImageAnalysis.Helper;
using ImageAnalysis.Interfaces;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using ImageAnalysis.ImageProcessing;
using System;
using Prism.Events;
using System.Text.Json;
using Infrastructure.Prism.Events;

namespace ImageAnalysis.ViewModels
{
    public class ImageAnalysisViewModel : BindableBase
    {

        public DelegateCommand<string> OpenImageCommand { get; set; }
        public DelegateCommand<string> SubstractImageCommand { get; set; }
        public DelegateCommand<string> BlurImageCommand { get; set; }
        public DelegateCommand FindCirclesCommand { get; set; }
        public DelegateCommand DeleteCommand { get; private set; }

        private readonly Dictionary<string, ICalibrationImage> images;

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

        private ObservableCollection<IImageList> imgList;
        public ObservableCollection<IImageList> ImgList
        {
            get { return imgList; }
            set { SetProperty(ref imgList, value); }
        }

        private IImageList selectedImage { get; set; }
        public IImageList SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; }
        }

        private Dictionary<string, double> calibMatrix;
        public Dictionary<string, double> CalibMatrix
        {
            get { return calibMatrix; }
            set { SetProperty(ref calibMatrix, value); }
        }

        private IImageList selectedSubA { get; set; }
        public IImageList SelectedSubA
        {
            get { return selectedSubA; }
            set { selectedSubA = value; }
        }

        private IImageList selectedSubB { get; set; }
        public IImageList SelectedSubB
        {
            get { return selectedSubB; }
            set { selectedSubB = value; }
        }

        public ImageAnalysisViewModel(IEventAggregator eventAggregator)
        {
            ImgList = new ObservableCollection<IImageList>();
            images = new Dictionary<string, ICalibrationImage>();

            OpenImageCommand = new DelegateCommand<string>(OpenImageHandler);
            SubstractImageCommand = new DelegateCommand<string>(SubstractImageHandler);
            BlurImageCommand = new DelegateCommand<string>(BlurImageHandler);
            FindCirclesCommand = new DelegateCommand(FindCirclesHandler);
            DeleteCommand = new DelegateCommand(DeleteHandler);
            
            eventAggregator.GetEvent<PatternSendEvent>().Subscribe(OnMessageReceived);

        }

        private void OnMessageReceived(string message)
        {
            var json = JsonSerializer.Deserialize<List<System.Windows.Point>>(message);
            var unique_items = new HashSet<System.Windows.Point>(json);
            List<Spot> spots = new List<Spot>();
            foreach (System.Windows.Point pt in unique_items)
            {
                var scaledX = pt.X * 1 / 30.129 + 720 / 2;
                var scaledY = pt.Y * 1 / 30.129 + 576 / 2;
                spots.Add(new Spot(new System.Windows.Point(scaledX, scaledY), 5, System.Windows.Media.Brushes.AliceBlue));
            }

            spots.Add(new Spot(new System.Windows.Point(720 / 2, 576 / 2), 5, System.Windows.Media.Brushes.AliceBlue));

            var sorted = PatternAnalyser.SortList(spots, false);
            Target = new ObservableCollection<Spot>();
            sorted.ForEach(sp =>
            {
                Target.Add(sp);
            });

            CalibMatrix = AffineMatrix.CalculateMatrix(Spots, Target);
        }


        private void DeleteHandler()
        {

        }

        private void FindCirclesHandler()
        {
            Spots = new ObservableCollection<Spot>();
            var sp = FindObjects.Circles(images[selectedImage.Title].ImageMat);
            sp.ForEach(sp =>
            {
                Spots.Add(sp);
            });
        }

        private void BlurImageHandler(string src)
        {
            var str = src + selectedImage.Title;

            if (!images.ContainsKey(str))
            {
                images.Add(str, new CalibrationImage(images[selectedImage.Title].ImageMat));
            }
            else
            {
                images[str] = new CalibrationImage(images[selectedImage.Title].ImageMat);
            }
            images[str].Blur();
            ImgList.Add(new ImageList(str, images[str].GetBitmapImage()));
        }

        private void SubstractImageHandler(string src)
        {
            if (!images.ContainsKey(src))
            {
                images.Add(src, new CalibrationImage(images[selectedSubA.Title].ImageMat));
            }
            else
            {
                images[src] = new CalibrationImage(images[selectedSubA.Title].ImageMat);
            }
            images[src].Substract(images[selectedSubB.Title].ImageMat);
            ImgList.Add(new ImageList(src, images[src].GetBitmapImage()));
        }

        private void OpenImageHandler(string src)
        {
            if (!images.ContainsKey(src))
            {
                images.Add(src, new CalibrationImage());
            }
            else
            {
                images[src] = new CalibrationImage();
            }
            ImgList.Add(new ImageList(src, images[src].GetBitmapImage()));
        }

    }
}
