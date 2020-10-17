using Infrastructure.Prism;
using System.Windows.Controls;

namespace PatternGenerator.ControlViews
{
    /// <summary>
    /// Interaction logic for StackPanelView.xaml
    /// </summary>
    public partial class StackPanelView : UserControl, ISupportDataContext
    {
        public StackPanelView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
