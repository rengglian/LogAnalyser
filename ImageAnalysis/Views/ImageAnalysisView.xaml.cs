using ImageAnalysis.ControlViews;
using Infrastructure.Prism;
using System.Windows.Controls;

namespace ImageAnalysis.Views
{
    /// <summary>
    /// Interaction logic for ImageAnaylsisView.xaml
    /// </summary>
    /// 
    [DependentView(typeof(OpenControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(SubstractControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(BlurControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(FindCirclesControlView), RegionNames.ContentRegionTop)]
    public partial class ImageAnalysisView : UserControl, ISupportDataContext
    {
        public ImageAnalysisView()
        {
            InitializeComponent();
        }
    }
}
