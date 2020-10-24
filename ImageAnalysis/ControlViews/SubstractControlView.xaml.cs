using Infrastructure.Prism;
using System.Windows.Controls;

namespace ImageAnalysis.ControlViews
{
    /// <summary>
    /// Interaction logic for SubstractControlView.xaml
    /// </summary>
    public partial class SubstractControlView : UserControl, ISupportDataContext
    {
        public SubstractControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
