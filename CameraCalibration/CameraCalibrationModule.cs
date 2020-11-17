using CameraCalibration.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace CameraCalibration
{
    public class CameraCalibrationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CameraCalibrationView>();
        }
    }
}
