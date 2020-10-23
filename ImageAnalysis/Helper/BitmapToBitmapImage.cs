using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper
{
    class BitmapToBitmapImage
    {
        public static BitmapImage Convert(Bitmap bitmap)
        {
            using MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);

            stream.Position = 0;
            BitmapImage result = new BitmapImage();
            result.BeginInit();

            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();

            return result;
        }

        public static Bitmap Convert(BitmapImage bmpImg)
        {
            if (bmpImg == null)
                return null;

            using MemoryStream outStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmpImg));
            enc.Save(outStream);
            return new Bitmap(outStream);
        }
    }
}
