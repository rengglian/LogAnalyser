using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternGenerator.ControlViews
{
    /// <summary>
    /// Interaction logic for ShapeControlView.xaml
    /// </summary>
    public partial class SaveControlView : UserControl, ISupportDataContext
    {
        public SaveControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
