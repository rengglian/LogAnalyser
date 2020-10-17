using Infrastructure.Prism;
using PatternGenerator.ControlViews;
using System.Windows.Controls;

namespace PatternGenerator.Views
{
    /// <summary>
    /// Interaction logic for PatternGeneratorView.xaml
    /// </summary>
    /// 
    [DependentView(typeof(StackPanelView), "ContentRegionTop")]
    public partial class PatternGeneratorView : UserControl, ISupportDataContext
    {
        public PatternGeneratorView()
        {
            InitializeComponent();
        }
    }
}
