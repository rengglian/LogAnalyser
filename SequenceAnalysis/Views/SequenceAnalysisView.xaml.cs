using SequenceAnalysis.ControlViews;
using Infrastructure.Prism;
using System.Windows.Controls;

namespace SequenceAnalysis.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 
    [DependentView(typeof(OpenControlView), RegionNames.ContentRegionTop)]
    public partial class SequenceAnalysisView : UserControl, ISupportDataContext
    {
        public SequenceAnalysisView()
        {
            InitializeComponent();
        }
    }
}