using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace CameraClibration.Helper
{
    public class ChessBoardImage : ICloneable
    {
        public string Title { get; set; } = "";

        public string Description { get; set; } = "";
        
        public Mat ImageMat { get; set; }
        public ChessBoardImage()
        {
            Read();
        }

        public override string ToString()
        {
            return Title;
        }

        private bool Read()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg"
            };

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filename = openFileDialog.FileName;
                Title = Path.GetFileName(filename);
                Description = "From File";
                ImageMat = new Mat(filename, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                return true;
            }
            return false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
