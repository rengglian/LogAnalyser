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
            List<Spot> spots = new List<Spot>();

            var tmp = Cv2.HoughCircles(img, HoughMethods.Gradient, 1, 10, 70, 10, 3, 10);

            for (int i = 0; i<tmp.Length; i++)
            {
                var x = tmp[i].Center.X - tmp[i].Radius;
                var y = tmp[i].Center.Y - tmp[i].Radius;
                var d = tmp[i].Radius * 2;
                spots.Add(new Spot(new System.Windows.Point(x, y), d, Brushes.Yellow));
            }
            return spots;
        }
    }
}
