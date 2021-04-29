using Infrastructure.Oxyplot;
using Microsoft.WindowsAPICodePack.Dialogs;
using OxyPlot;
using Prism.Commands;
using Prism.Mvvm;
using SequenceAnalysis.Helper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace SequenceAnalysis.ViewModels
{
    public class SequenceAnalysisViewModel : BindableBase
    {
        public DelegateCommand OpenSequenceCommand { get; set; }

        private string seqFolderName;
        public string SeqFolderName
        {
            get { return seqFolderName; }
            set { SetProperty(ref seqFolderName, value); }
        }

        private int hotZoneRadius;
        public int HotZoneRadius
        {
            get { return hotZoneRadius; }
            set { SetProperty(ref hotZoneRadius, value); }
        }

        private PlotModel movementPlotModel;
        public PlotModel MovementPlotModel
        {
            get { return movementPlotModel; }
            set { SetProperty(ref movementPlotModel, value); }
        }

        private PlotModel diameterPlotModel;
        public PlotModel DiameterPlotModel
        {
            get { return diameterPlotModel; }
            set { SetProperty(ref diameterPlotModel, value); }
        }

        public SequenceAnalysisViewModel()
        {
            MovementPlotModel = PlotModelHelper.CreateScatterPlot();
            DiameterPlotModel = PlotModelHelper.CreateScatterPlot();
            OpenSequenceCommand = new DelegateCommand(OpenSequenceHandler);
            HotZoneRadius = 1500;
        }

        private void OpenSequenceHandler()
        {

            var openFolder = new CommonOpenFileDialog
            {
                AllowNonFileSystemItems = true,
                Multiselect = true,
                IsFolderPicker = true,
                Title = "Select folders with a sequence"
            };

            if (openFolder.ShowDialog() != CommonFileDialogResult.Ok)
            {
                MessageBox.Show("No Folder selected");
                return;
            }

            // get all the directories in selected dirctory
            var dir = openFolder.FileName;

            SeqFolderName = dir.Split("\\").Last();

            List<Frame> frames = new List<Frame>();
            foreach (string fileName in Directory.GetFiles(dir, "*.json"))
            {
                var jsonString = File.ReadAllText(fileName);
                var frame = JsonSerializer.Deserialize<Frame>(jsonString);
                frames.Add(frame);
            }

            var Centers = frames.Where(frame => frame.HasCorrespondingImage == true).Select(frame => new Point(frame.Limbus.X, frame.Limbus.Y)).ToList<Point>();
            var Sizes = frames.Where(frame => frame.HasCorrespondingImage == true).Select(frame => new Point(frame.Limbus.Height, frame.Limbus.Width)).ToList<Point>();

            var test = frames.Where(frame => frame.HasCorrespondingImage == true).Select(frame => frame).Min(frame => frame.Limbus.X);

            MovementPlotModel.Series.Clear();
            MovementPlotModel.Series.Add(PlotModelHelper.CreateScatterSerie(Centers));
            MovementPlotModel.Series.Add(PlotModelHelper.CreateHotZone(Centers[0], HotZoneRadius, OxyColors.Red));
            MovementPlotModel.InvalidatePlot(true);

            DiameterPlotModel.Series.Clear();
            DiameterPlotModel.Series.Add(PlotModelHelper.CreateScatterSerie(Sizes));
            DiameterPlotModel.Series.Add(PlotModelHelper.CreateHotZone(Sizes[0], HotZoneRadius/3, OxyColors.Blue));
            DiameterPlotModel.InvalidatePlot(true);


        }
    }
}
