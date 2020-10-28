using ImageAnalysis.Helper;
using OpenCvSharp;
using System.Collections.Generic;
using System.Windows.Media;

namespace ImageAnalysis.ImageProcessing
{
    public static class FindObjects
    {
        public static List<Spot> Circles(Mat img)
        {
            var gray = img.Clone();
            if (img.Channels() > 1) Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            //Cv2.Flip(gray, gray, FlipMode.Y);
            var obj = Cv2.HoughCircles(gray, HoughMethods.Gradient, 1, 10, 70, 10, 3, 10);

            List<Spot> spots = new List<Spot>();

            for (int i = 0; i<obj.Length; i++)
            {

                var x = obj[i].Center.X;
                var y = obj[i].Center.Y;
                var d = obj[i].Radius * 2;

                spots.Add(new Spot(new System.Windows.Point(x, y), d, Brushes.Yellow));
            }

            var sorted = PatternAnalyser.SortList(spots, true);

            return sorted;
        }
       
    }
}
