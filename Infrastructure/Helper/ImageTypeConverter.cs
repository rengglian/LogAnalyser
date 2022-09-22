using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageAnalysis.Helper;

public class ImageTypeConverter
{
    public static BitmapImage Convert(Bitmap bitmap)
    {
        using MemoryStream stream = new();
        bitmap.Save(stream, ImageFormat.Bmp);

        stream.Position = 0;
        BitmapImage result = new();
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

        using MemoryStream outStream = new();
        BitmapEncoder enc = new BmpBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(bmpImg));
        enc.Save(outStream);
        return new Bitmap(outStream);
    }
}
