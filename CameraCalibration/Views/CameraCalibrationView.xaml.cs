using CameraCalibration.ControlViews;
using Infrastructure.Prism;
using System.Windows.Controls;

namespace CameraCalibration.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 
    [DependentView(typeof(OpenControlView), RegionNames.ContentRegionTop)]
    public partial class CameraCalibrationView : UserControl, ISupportDataContext
    {
        public CameraCalibrationView()
        {
            InitializeComponent();
        }
    }
}