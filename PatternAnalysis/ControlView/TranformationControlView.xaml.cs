using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for SubstractControlView.xaml
    /// </summary>
    public partial class TranformationControlView : UserControl, ISupportDataContext
    {
        public TranformationControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
