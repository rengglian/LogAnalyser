using PatternAnalysis.Helper;
using PatternAnalysis.Interfaces;
using Microsoft.Win32;
using OxyPlot;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using System.Text.Json;
using Infrastructure.Prism.Events;
using Prism.Mvvm;
using Infrastructure.Oxyplot;
using OxyPlot.Axes;
using System.Linq;
using Prism.Services.Dialogs;
using System;

namespace PatternAnalysis.ViewModels
{
    public class PatternAnalysisViewModel : BindableBase
    {

        private readonly IDialogService _dialogService;

        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand OpenDataSetCommand { get; set; }
        public DelegateCommand TransformMatrixCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand CompareCommand { get; set; }

        public DelegateCommand<int?> ListCommand { get; set; }

        private ObservableCollection<IPattern> patternList;
        public ObservableCollection<IPattern> PatternList
        {
            get { return patternList; }
            set { SetProperty(ref patternList, value); }
        }
        private IPattern _selectedA { get; set; }
        public IPattern SelectedA
        {
            get { return this._selectedA; }
            set { this._selectedA = value; }
        }

        private IPattern _selectedB { get; set; }
        public IPattern SelectedB
        {
            get { return this._selectedB; }
            set { this._selectedB = value; }
        }

        private Dictionary<string, double> calibMatrix;
        public Dictionary<string, double> CalibMatrix
        {
            get { return calibMatrix; }
            set { SetProperty(ref calibMatrix, value); }
        }

        private PlotModel plotModelPattern;
        public PlotModel PlotModelPattern
        {
            get { return plotModelPattern; }
            set { SetProperty(ref plotModelPattern, value); }
        }

        private Dictionary<string, double> _decomposeMatrix;
        public Dictionary<string, double> DecomposeMatrix
        {
            get { return _decomposeMatrix; }
            set { SetProperty(ref _decomposeMatrix, value); }
        }

        public PatternAnalysisViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            PlotModelPattern = PlotModelHelper.CreateScatterPlot();
            
            PatternList = new ObservableCollection<IPattern>();

            OpenDataSetCommand = new DelegateCommand(OpenDataSetHandler);
            TransformMatrixCommand = new DelegateCommand(TransformMatrixHandler);
            SendCommand = new DelegateCommand(SendHandler);
            CompareCommand = new DelegateCommand(CompareHandler);
            ListCommand = new DelegateCommand<int?>(ListHandler);
            
        }

        private void ListHandler(int? item)
        {
            if (item >= 0)
            {
                PatternList.RemoveAt((int)item);
                PlotModelPattern.Series.RemoveAt((int)item);
                PlotModelPattern.InvalidatePlot(true);
            }
        }

        private void CompareHandler()
        {
            var patternA = JsonSerializer.Serialize(SelectedA.Points);
            var patternB = JsonSerializer.Serialize(SelectedB.Points);
            _dialogService.ShowPatternCompareDialog(patternA, patternB, r =>{ });
        }

        private void OpenDataSetHandler()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                PatternList.Add(new Pattern(openFileDialog.FileName));
                                
                var series = PlotModelHelper.CreateScatterSerie(PatternList.Last().Points, PatternList.Last().Color);
                
                PlotModelPattern.Series.Add(series);
                PlotModelPattern.InvalidatePlot(true);
            }
        }

        private void SendHandler()
        {
            var json = JsonSerializer.Serialize(SelectedA.Points);
            _eventAggregator.GetEvent<PatternSendEvent>().Publish(json);
        }

        private void TransformMatrixHandler()
        {

            CalibMatrix = AffineMatrix.CalculateMatrix(SelectedA, SelectedB);

            DecomposeMatrix = AffineMatrix.Decompose(CalibMatrix);

            PatternList.Add(new Pattern("None"));

            PatternList.Last().Points = AffineMatrix.CalculateBack(SelectedA.Points, CalibMatrix);

            var series = PlotModelHelper.CreateScatterSerie(PatternList.Last().Points, PatternList.Last().Color);
            PlotModelPattern.Series.Add(series);
            PlotModelPattern.InvalidatePlot(true);
        }
    }
}
