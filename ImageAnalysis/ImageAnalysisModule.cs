using ImageAnalysis.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ImageAnalysis
{
    public class ImageAnalysisModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ImageAnalysisView>();
        }
    }
}
