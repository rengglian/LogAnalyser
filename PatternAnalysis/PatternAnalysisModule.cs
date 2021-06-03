using Infrastructure.Prism;
using PatternAnalysis.Dialogs;
using PatternAnalysis.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PatternAnalysis
{
    public class PatternAnalysisModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(PatternAnalysisView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PatternAnalysisView>();

            containerRegistry.RegisterDialog<PatternCompareDialog, PatternCompareDialogViewModel>();
            containerRegistry.RegisterDialog<MovementDialog, MovementDialogViewModel>();
        }
    }
}
