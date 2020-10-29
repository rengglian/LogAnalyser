using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternGenerator.ControlViews
{
    /// <summary>
    /// Interaction logic for ShapeControlView.xaml
    /// </summary>
    public partial class ShapeControlView : UserControl, ISupportDataContext
    {
        public ShapeControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
