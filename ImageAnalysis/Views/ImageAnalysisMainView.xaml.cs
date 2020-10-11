using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace ImageAnalysis.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ImageAnalysisMainView : UserControl
    {
        IContainerExtension _container;
        IRegionManager _regionManager;
        IRegion _region;

        ImageAnaylsisView _imageAnaylsisView;

        public ImageAnalysisMainView (IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();

            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _imageAnaylsisView = _container.Resolve<ImageAnaylsisView>();


            _region = _regionManager.Regions["ContentRegionImageAnalysisView"];

            _region.Add(_imageAnaylsisView);
        }

    }
}