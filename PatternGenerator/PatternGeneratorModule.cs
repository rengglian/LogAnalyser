using PatternGenerator.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PatternGenerator
{
    public class PatternGeneratorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PatternGeneratorMainView>();
        }
    }
}
