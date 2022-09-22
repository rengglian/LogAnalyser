using ImageAnalysis.Helper;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using ImageAnalysis.ImageProcessing;
using Prism.Events;
using Infrastructure.Prism.Events;
using ImageAnalysis.Extension;
using Infrastructure.Helper;

namespace ImageAnalysis.ViewModels;

public class ImageAnalysisViewModel : BindableBase
{
    private ObservableCollection<CalibrationImage> calibImg;
    private CalibrationImage _selectedSubA;
    private CalibrationImage _selectedSubB;
    private CalibrationImage _selectedImgList;
    private ObservableCollection<Spot> spots;
    private ObservableCollection<Spot> target;
    private Dictionary<string, double> calibMatrix;
    public Dictionary<string, Options> props;

    public DelegateCommand OpenImageCommand { get; set; }
    public DelegateCommand<string> SubstractImageCommand { get; set; }
    public DelegateCommand<string> BlurImageCommand { get; set; }
    public DelegateCommand FindCirclesCommand { get; set; }
    public DelegateCommand<int?> DeleteCommand { get; set; }

    public ObservableCollection<CalibrationImage> CalibImg
    {
        get { return calibImg; }
        set { SetProperty(ref calibImg, value); }
    }

    public CalibrationImage SelectedSubA
    {
        get { return _selectedSubA; }
        set { _selectedSubA = value; }
    }

    public CalibrationImage SelectedSubB
    {
        get { return _selectedSubB; }
        set { _selectedSubB = value; }
    }

    public CalibrationImage SelectedImgList
    {
        get { return _selectedImgList; }
        set { _selectedImgList = value; }
    }

    public ObservableCollection<Spot> Spots
    {
        get { return spots; }
        set { SetProperty(ref spots, value); }
    }

    public ObservableCollection<Spot> Target
    {
        get { return target; }
        set { SetProperty(ref target, value); }
    }

    public Dictionary<string, double> CalibMatrix
    {
        get { return calibMatrix; }
        set { SetProperty(ref calibMatrix, value); }
    }

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

        eventAggregator.GetEvent<PatternSendEvent>().Subscribe(OnPatternMessageReceived);
        eventAggregator.GetEvent<CameraCalibrationSendEvent>().Subscribe(OnCameraCalibrationMessageReceived);
    }

    private void OnPatternMessageReceived(string message)
    {
        var spots = PatternMessage.Parse(message, Props);
        Target = PatternAnalyser.SortList(spots, false).ToObservableCollection();
        CalibMatrix = AffineMatrix.CalculateMatrix(Spots, Target);
    }

    private void OnCameraCalibrationMessageReceived(string message)
    {
        Props = CameraCalibrationMessage.Parse(message);
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
