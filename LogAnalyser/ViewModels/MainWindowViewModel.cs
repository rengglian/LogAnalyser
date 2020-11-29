using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace LogAnalyser.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "LogAnalyser";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                Title = navigatePath;
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
            }
        }
    }
}