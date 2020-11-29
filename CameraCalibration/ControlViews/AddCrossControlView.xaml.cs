using Infrastructure.Prism;
using System.Windows.Controls;

namespace CameraCalibration.ControlViews
{
    /// <summary>
    /// Interaction logic for AddCrossControlView.xaml
    /// </summary>
    public partial class AddCrossControlView : UserControl, ISupportDataContext
    {
        public AddCrossControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
