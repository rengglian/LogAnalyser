using Infrastructure.Prism;
using System.Windows.Controls;

namespace CameraCalibration.ControlViews
{
    /// <summary>
    /// Interaction logic for Analyse.xaml
    /// </summary>
    public partial class AnalyseControlView : UserControl, ISupportDataContext
    {
        public AnalyseControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
