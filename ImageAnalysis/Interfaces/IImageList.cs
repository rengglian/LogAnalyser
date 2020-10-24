using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Interfaces
{
    public interface IImageList
    {
        BitmapImage ImageData { get; set; }
        string Title { get; set; }
        int Counter { get; set; }
    }
}