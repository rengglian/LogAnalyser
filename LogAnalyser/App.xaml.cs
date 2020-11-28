using LogAnalyser.PrismHelper;
using LogAnalyser.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using System.Windows.Controls;

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
            moduleCatalog.AddModule<CameraCalibration.CameraCalibrationModule>();
            moduleCatalog.AddModule<TraceAnalysis.TraceAnalysisModule>();
            moduleCatalog.AddModule<SequenceAnalysis.SequenceAnalysisModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
