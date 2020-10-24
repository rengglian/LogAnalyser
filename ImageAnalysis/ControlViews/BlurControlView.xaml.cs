using Infrastructure.Prism;
using System.Windows.Controls;

namespace ImageAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for BlurControlView.xaml
    /// </summary>
    public partial class BlurControlView : UserControl, ISupportDataContext
    {
        public BlurControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
