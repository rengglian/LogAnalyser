using LogAnalyser.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace LogAnalyser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<PatternAnalysis.PatternAnalysisModule>();
            moduleCatalog.AddModule<PatternGenerator.PatternGeneratorModule>();
            moduleCatalog.AddModule<ImageAnalysis.ImageAnalysisModule>();
        }
    }
}
