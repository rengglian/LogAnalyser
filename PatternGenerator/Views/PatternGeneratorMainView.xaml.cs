using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace PatternGenerator.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 
    public partial class PatternGeneratorMainView : UserControl
    {
        readonly IContainerExtension _container;
        readonly IRegionManager _regionManager;
        IRegion _region;

        PatternGeneratorView _patternGeneratorView;

        public PatternGeneratorMainView(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();

            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _patternGeneratorView = _container.Resolve<PatternGeneratorView>();


            _region = _regionManager.Regions["ContentRegionPatternGeneratorView"];

            _region.Add(_patternGeneratorView);
        }
    }
}