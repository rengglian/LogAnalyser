using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for SubstractControlView.xaml
    /// </summary>
    public partial class SendControlView : UserControl, ISupportDataContext
    {
        public SendControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
