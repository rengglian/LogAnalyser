using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PatternGenerator.ViewModels
{
    public class PatternGeneratorViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Title { get; set; } = "Pattern Generator";

        public PatternGeneratorViewModel()
        {

        }

    }
}
