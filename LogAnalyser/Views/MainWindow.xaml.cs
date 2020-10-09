using LogAnalyser.ViewModels;
using LogAnalyser.Views;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogAnalyser.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IContainerExtension _container;
        IRegionManager _regionManager;
        IRegion _region;

        PatternAnalysisView _patternAnalysisView;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();

            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _patternAnalysisView = _container.Resolve<PatternAnalysisView>();
            

            _region = _regionManager.Regions["ContentRegion"];

            _region.Add(_patternAnalysisView);
        }

        private void Pattern_Clicked(object sender, RoutedEventArgs e)
        {
            //activate view a
            _region.Activate(_patternAnalysisView);
        }
    }
}
