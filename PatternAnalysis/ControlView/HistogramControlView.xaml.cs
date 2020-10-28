using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for SubstractControlView.xaml
    /// </summary>
    public partial class HistogramControlView : UserControl, ISupportDataContext
    {
        public HistogramControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
