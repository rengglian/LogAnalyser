using ImageAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper
{
    public class ImageList : IImageList
    {
        public string Title { get; set; } = "";
        public BitmapImage ImageData { get; set; } = new BitmapImage();
        public int Counter { get; set; } = -1;
        public ImageList(string title, int counter, BitmapImage imageData)
        {
            Title = title;
            ImageData = imageData;
            Counter = counter;

        }
    }
}
