using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace PatternAnalysis.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PatternAnalysisMainView : UserControl
    {
        IContainerExtension _container;
        IRegionManager _regionManager;
        IRegion _region;

        PatternAnalysisView _patternAnalysisView;

        public PatternAnalysisMainView(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();

            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _patternAnalysisView = _container.Resolve<PatternAnalysisView>();


            _region = _regionManager.Regions["ContentRegionPatternView"];

            _region.Add(_patternAnalysisView);
        }
    }
}