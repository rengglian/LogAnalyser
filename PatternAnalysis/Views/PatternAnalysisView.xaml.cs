using PatternAnalysis.ViewModels;
using System.Windows.Controls;

namespace PatternAnalysis.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PatternAnalysisView : UserControl
    {
        public PatternAnalysisView()
        {
            InitializeComponent();
            this.DataContext = new PatternAnalysisViewModel();
        }
    }
}
