using Infrastructure.Oxyplot;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternAnalysis.Extension;
using PatternAnalysis.Helper;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PatternAnalysis.Dialogs
{
    class PatternCompareDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Pattern Compare Dialog";

        private List<Point> PatternA;
        private List<Point> PatternB;

        private PlotModel _plotModelPatternA;
        public PlotModel PlotModelPatternA
        {
            get { return _plotModelPatternA; }
            set { SetProperty(ref _plotModelPatternA, value); }
        }

        private PlotModel _plotModelPatternB;
        public PlotModel PlotModelPatternB
        {
            get { return _plotModelPatternB; }
            set { SetProperty(ref _plotModelPatternB, value); }
        }

        private PlotModel _plotModelHisto;
        public PlotModel PlotModelHisto
        {
            get { return _plotModelHisto; }
            set { SetProperty(ref _plotModelHisto, value); }
        }

        private ObservableCollection<Point> _histoTable;
        public ObservableCollection<Point> HistoTable
        {
            get { return _histoTable; }
            set { SetProperty(ref _histoTable, value); }
        }

        private ObservableCollection<Pulse> _pulseTableA;
        public ObservableCollection<Pulse> PulseTableA
        {
            get { return _pulseTableA; }
            set { SetProperty(ref _pulseTableA, value); }
        }

        private ObservableCollection<Pulse> _pulseTableB;
        public ObservableCollection<Pulse> PulseTableB
        {
            get { return _pulseTableB; }
            set { SetProperty(ref _pulseTableB, value); }
        }

        public DelegateCommand CloseDialogCommand { get; }

        public event Action<IDialogResult> RequestClose;

        public PatternCompareDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand(CloseDialog);

            PlotModelPatternA = PlotModelHelper.CreateScatterPlotInvX();
            PlotModelPatternB = PlotModelHelper.CreateScatterPlotInvX();
            PlotModelHisto = PlotModelHelper.CreateHistogramm();
        }

        private void CloseDialog()
        {
            var buttonResult = ButtonResult.OK;

            var parameters = new DialogParameters
            {
                { "myParam", "The Dialog was closed by the user." }
            };

            RequestClose?.Invoke(new DialogResult(buttonResult, parameters));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            PatternA = JsonSerializer.Deserialize<List<Point>>(parameters.GetValue<string>("patternA"));
            PatternB = JsonSerializer.Deserialize<List<Point>>(parameters.GetValue<string>("patternB"));

            var distance = Histogram.Distance(PatternA, PatternB);
            var histoSet = Histogram.CreateBins(distance, 50);

            PulseTableA = GetWrongPulses(PatternA, distance, 500);
            PulseTableB = GetWrongPulses(PatternB, distance, 500);

            HistoTable = histoSet.ToObservableCollection();

            PlotModelPatternA.Series.Add(GenerateScatterSeries(PatternA, distance));
            PlotModelPatternB.Series.Add(GenerateScatterSeries(PatternB, distance));

            PlotModelPatternA.InvalidatePlot(true);
            PlotModelPatternB.InvalidatePlot(true);

            PlotModelHisto.Series.Clear();
            PlotModelHisto.Series.Add(PlotModelHelper.CreateBarSeries(ConvertToDataPoint(histoSet)));
            PlotModelHisto.Axes.Add(new CategoryAxis { Position = AxisPosition.Bottom, Key = "y", Angle = 90, ItemsSource = histoSet, LabelField = "X" });
            PlotModelHisto.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, Key = "x" });
            PlotModelHisto.InvalidatePlot(true);

        }

        private ObservableCollection<Pulse> GetWrongPulses(List<Point> pattern, List<double> dist, double threshold)
        {
            List<int> indices = dist.Select((v, i) => new { v, i })
            .Where(x => x.v > threshold)
            .Select(x => x.i).ToList<int>();

            var result = new ObservableCollection<Pulse>();

            indices.ForEach(ind =>
            {
                if(ind>0) result.Add(new Pulse(ind-1, pattern[ind-1]));
                result.Add(new Pulse(ind, pattern[ind]));
            });

            return result;
        }

        private ScatterSeries GenerateScatterSeries(List<Point> points, List<double> dist)
        {
        
            var series = PlotModelHelper.CreateScatterSerie(points, dist);

            return series;
        }
       
        private List<DataPoint> ConvertToDataPoint(List<Point> points)
        {
            var pts = new List<DataPoint>();

            points.ForEach(pt =>
            {
                pts.Add(new DataPoint(pt.X, pt.Y));
            });

            return pts;
        }
    }
}
