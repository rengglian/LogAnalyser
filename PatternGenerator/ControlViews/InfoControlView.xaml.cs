using Infrastructure.Prism;
using System;
using System.Windows.Controls;

namespace PatternGenerator.ControlViews
{
    /// <summary>
    /// Interaction logic for StackPanelView.xaml
    /// </summary>
    public partial class InfoControlView : UserControl, ISupportDataContext
    {
        public InfoControlView()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(UserControl));
        }
    }
}
