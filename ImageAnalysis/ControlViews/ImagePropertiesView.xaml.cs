using Infrastructure.Prism;
using System.Windows.Controls;

namespace ImageAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for ShapeControlView.xaml
    /// </summary>
    public partial class ImagePropertiesView : UserControl, ISupportDataContext
    {
        public ImagePropertiesView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
