using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Text.Json;
using OxyPlot;
using Prism.Commands;
using Infrastructure.Oxyplot;

namespace PatternAnalysis.Dialogs;

class MovementDialogViewModel : BindableBase, IDialogAware
{
    public string Title => "Movement Dialog";

    private PlotModel _plotModelPatternA;
    public PlotModel PlotModelPatternA
    {
        get { return _plotModelPatternA; }
        set { SetProperty(ref _plotModelPatternA, value); }
    }

    public DelegateCommand CloseDialogCommand { get; }

    public event Action<IDialogResult> RequestClose;

    public MovementDialogViewModel()
    {
        CloseDialogCommand = new DelegateCommand(CloseDialog);

        PlotModelPatternA = PlotModelHelper.CreateScatterPlotInvX();
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
        var refPattern = JsonSerializer.Deserialize<List<Point>>(parameters.GetValue<string>("patternA"));
        var movePattern = JsonSerializer.Deserialize<List<Point>>(parameters.GetValue<string>("patternB"));

        var centers = new List<Point>();

        for (var i = 1; i<refPattern.Count; i++)
        {
            centers.Add((Point)(refPattern[i] - movePattern[i]));
        }

        PlotModelPatternA.Series.Add(PlotModelHelper.CreateScatterSerie(centers));
        //PlotModelPatternA.Series.Add(PlotModelHelper.CreateHotZone(centers[0], 1500, OxyColors.Red));
        PlotModelPatternA.InvalidatePlot(true);

    }
}
