using Infrastructure.Prism;
using PatternGenerator.ControlViews;
using System.Windows.Controls;

namespace PatternGenerator.Views
{
    /// <summary>
    /// Interaction logic for PatternGeneratorView.xaml
    /// </summary>
    /// 
    [DependentView(typeof(ShapeControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(UpdateControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(SaveControlView), RegionNames.ContentRegionTop)]
    [DependentView(typeof(InfoControlView), RegionNames.ContentRegionTop)]
    public partial class PatternGeneratorView : UserControl, ISupportDataContext
    {
        public PatternGeneratorView()
        {
            InitializeComponent();
        }
    }
}
