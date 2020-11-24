using Infrastructure.Prism;
using System.Windows.Controls;

namespace TraceAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for OpenControlView.xaml
    /// </summary>
    public partial class OpenControlView : UserControl, ISupportDataContext
    {
        public OpenControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
