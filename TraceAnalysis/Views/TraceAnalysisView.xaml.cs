using Infrastructure.Prism;
using System.Windows.Controls;
using TraceAnalysis.ControlViews;

namespace TraceAnalysis.Views
{
    /// <summary>
    /// Interaction logic for TraceAnalysisView.xaml
    /// </summary>
    /// 
    [DependentView(typeof(OpenControlView), RegionNames.ContentRegionTop)]
    public partial class TraceAnalysisView : UserControl, ISupportDataContext
    {
        public TraceAnalysisView()
        {
            InitializeComponent();
        }
    }
}
