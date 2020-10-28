using Infrastructure.Prism;
using PatternAnalysis.ControlViews;
using System.Windows.Controls;

namespace PatternAnalysis.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    [DependentView(typeof(OpenControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(HistogramControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(TranformationControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(SendControlView), RegionNames.ContentRegionTop)]
    public partial class PatternAnalysisView : UserControl, ISupportDataContext
    {
        public PatternAnalysisView()
        {
            InitializeComponent();
        }
    }
}
