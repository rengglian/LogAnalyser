using ImageAnalysis.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageAnalysis.Views
{
    /// <summary>
    /// Interaction logic for ImageAnaylsisView.xaml
    /// </summary>
    public partial class ImageAnaylsisView : UserControl
    {
        public ImageAnaylsisView()
        {
            InitializeComponent();
            this.DataContext = new ImageAnalysisViewModel();
        }
    }
}
