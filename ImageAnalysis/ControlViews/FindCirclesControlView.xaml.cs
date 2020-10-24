using Infrastructure.Prism;
using System.Windows.Controls;

namespace ImageAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for FindCirclesControlView.xaml
    /// </summary>
    public partial class FindCirclesControlView : UserControl, ISupportDataContext
    {
        public FindCirclesControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
