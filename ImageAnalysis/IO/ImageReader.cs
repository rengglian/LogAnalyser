using Microsoft.Win32;
using OpenCvSharp;
using System.IO;

namespace ImageAnalysis.IO;

public class ImageReader
{
    public static MatFromFile Read()
    {

        OpenFileDialog openFileDialog = new()
        {
            DefaultExt = ".png",
            Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg"
        };

        bool? result = openFileDialog.ShowDialog();

        if (result == true)
        {
            string filename = openFileDialog.FileName;

            MatFromFile data = new()
            {
                FileName = Path.GetFileName(filename),
                ImageMat = new Mat(filename, ImreadModes.AnyDepth | ImreadModes.AnyColor)
            };

            return data;
        }
        return null;
    }
}
