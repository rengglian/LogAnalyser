using Infrastructure.Oxyplot;
using OxyPlot;
using OxyPlot.Series;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PatternAnalysis.Dialogs
{
    class PatternCompareDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Pattern Compare Dialog";

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

        public DelegateCommand CloseDialogCommand { get; }

        public event Action<IDialogResult> RequestClose;

        public PatternCompareDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand(CloseDialog);

            PlotModelPatternA = PlotModelHelper.CreateScatterPlot();
            PlotModelPatternB = PlotModelHelper.CreateScatterPlot();
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
            var patternA = JsonSerializer.Deserialize<List<System.Windows.Point>>(parameters.GetValue<string>("patternA"));
            var patternB = JsonSerializer.Deserialize<List<System.Windows.Point>>(parameters.GetValue<string>("patternB"));


            PlotModelPatternA.Series.Add(generateScatterSeries(patternA, OxyColors.DarkOrchid));
            PlotModelPatternB.Series.Add(generateScatterSeries(patternB, OxyColors.DarkKhaki));

            PlotModelPatternA.InvalidatePlot(true);
            PlotModelPatternB.InvalidatePlot(true);
        }

        private ScatterSeries generateScatterSeries(List<System.Windows.Point> points, OxyColor color)
        {
            var pts = new List<DataPoint>();

            points.ForEach(pt =>
            {
                pts.Add(new DataPoint(pt.X, pt.Y));
            });

            var series = PlotModelHelper.CreateScatterSerie(pts, color);

            return series;
        }
    }
}
