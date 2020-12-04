using ImageAnalysis.Helper;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using ImageAnalysis.ImageProcessing;
using Prism.Events;
using System.Text.Json;
using Infrastructure.Prism.Events;

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

        public ImageAnalysisViewModel(IEventAggregator eventAggregator)
        {
            CalibImg = new ObservableCollection<CalibrationImage>();

            OpenImageCommand = new DelegateCommand(OpenImageHandler);
            SubstractImageCommand = new DelegateCommand<string>(SubstractImageHandler);
            BlurImageCommand = new DelegateCommand<string>(BlurImageHandler);
            FindCirclesCommand = new DelegateCommand(FindCirclesHandler);
            DeleteCommand = new DelegateCommand<int?>(DeleteHandler);

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
            var img = (CalibrationImage)selectedSubA.Clone();
            img.Substract(selectedSubB.ImageMat);
            img.Description = src;
            CalibImg.Add(img);
        }

        private void BlurImageHandler(string src)
        {
            var img = (CalibrationImage)selectedSubA.Clone();
            img.Blur();
            img.Description = src;
            CalibImg.Add(img);
        }

        private void FindCirclesHandler()
        {
            Spots = new ObservableCollection<Spot>();
            var sp = FindObjects.Circles(selectedSubA.ImageMat);
            sp.ForEach(sp =>
            {
                Spots.Add(sp);
            });
        }
    }
}
