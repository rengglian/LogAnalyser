using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for MovementAnalysisControlView.xaml
    /// </summary>
    public partial class MovementAnalysisControlView : UserControl, ISupportDataContext
    {
        public MovementAnalysisControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
