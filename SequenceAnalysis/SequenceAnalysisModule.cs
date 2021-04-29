using Prism.Ioc;
using Prism.Modularity;
using SequenceAnalysis.Views;

namespace SequenceAnalysis
{
    public class SequenceAnalysisModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SequenceAnalysisView>();
        }
    }
}
