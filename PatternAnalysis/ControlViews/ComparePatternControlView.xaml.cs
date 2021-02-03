using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for ComparePatternControlView.xaml
    /// </summary>
    public partial class ComparePatternControlView : UserControl, ISupportDataContext
    {
        public ComparePatternControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
