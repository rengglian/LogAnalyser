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
using System.Linq;
using Prism.Services.Dialogs;

namespace PatternAnalysis.ViewModels;

public class PatternAnalysisViewModel : BindableBase
{

    private readonly IDialogService _dialogService;
    private readonly IEventAggregator _eventAggregator;

    private ObservableCollection<IPattern> patternList;
    private IPattern _selectedA;
    private IPattern _selectedB;
    private Dictionary<string, double> calibMatrix;
    private PlotModel plotModelPattern;
    private Dictionary<string, double> _decomposeMatrix;
    private List<string> _colorCodeList;
    private string _selectedColorCode;

    public DelegateCommand OpenDataSetCommand { get; set; }
    public DelegateCommand TransformMatrixCommand { get; set; }
    public DelegateCommand SendCommand { get; set; }
    public DelegateCommand CompareCommand { get; set; }
    public DelegateCommand MovementCommand { get; set; }
    public DelegateCommand<int?> DeleteCommand { get; set; }

    public ObservableCollection<IPattern> PatternList
    {
        get { return patternList; }
        set { SetProperty(ref patternList, value); }
    }
    public IPattern SelectedA
    {
        get { return _selectedA; }
        set { _selectedA = value; }
    }

    public IPattern SelectedB
    {
        get { return _selectedB; }
        set { _selectedB = value; }
    }

    public Dictionary<string, double> CalibMatrix
    {
        get { return calibMatrix; }
        set { SetProperty(ref calibMatrix, value); }
    }

    public PlotModel PlotModelPattern
    {
        get { return plotModelPattern; }
        set { SetProperty(ref plotModelPattern, value); }
    }

    public Dictionary<string, double> DecomposeMatrix
    {
        get { return _decomposeMatrix; }
        set { SetProperty(ref _decomposeMatrix, value); }
    }

    public List<string> ColorCodeList
    {
        get { return _colorCodeList; }
        set { SetProperty(ref _colorCodeList, value); }
    }

    public string SelectedColorCode
    {
        get { return _selectedColorCode; }
        set { SetProperty(ref _selectedColorCode, value); }
    }

    public PatternAnalysisViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
    {
        _dialogService = dialogService;
        _eventAggregator = eventAggregator;

        PlotModelPattern = PlotModelHelper.CreateScatterPlotInvX();
        
        PatternList = new ObservableCollection<IPattern>();

        OpenDataSetCommand = new DelegateCommand(OpenDataSetHandler);
        TransformMatrixCommand = new DelegateCommand(TransformMatrixHandler);
        SendCommand = new DelegateCommand(SendHandler);
        CompareCommand = new DelegateCommand(CompareHandler);
        DeleteCommand = new DelegateCommand<int?>(DeleteHandler);
        MovementCommand = new DelegateCommand(MovementHandler);

        ColorCodeList = PlotModelHelper.GetOxyColorListAsStrings();

        SelectedColorCode = ColorCodeList.First();
    }

    private void DeleteHandler(int? item)
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

    private void MovementHandler()
    {
        var patternA = JsonSerializer.Serialize(SelectedA.Points);
        var patternB = JsonSerializer.Serialize(SelectedB.Points);
        _dialogService.ShowMovementDialog(patternA, patternB, r => { });
    }

    private void OpenDataSetHandler()
    {

        OpenFileDialog openFileDialog = new();
        if (openFileDialog.ShowDialog() == true)
        {
            PatternList.Add(new Pattern(openFileDialog.FileName, SelectedColorCode));
                            
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
