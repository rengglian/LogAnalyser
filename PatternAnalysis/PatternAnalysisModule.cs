using PatternAnalysis.Dialogs;
using PatternAnalysis.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PatternAnalysis
{
    public class PatternAnalysisModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PatternAnalysisView>();

            containerRegistry.RegisterDialog<PatternCompareDialog, PatternCompareDialogViewModel>();
            containerRegistry.RegisterDialog<MovementDialog, MovementDialogViewModel>();
        }
    }
}
