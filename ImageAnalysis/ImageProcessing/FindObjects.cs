using ImageAnalysis.Helper;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;
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
                spots.Add(new Spot(new System.Windows.Point(tmp[i].Center.X - tmp[i].Radius, tmp[i].Center.Y - tmp[i].Radius), tmp[i].Radius * 2));
            }
            return spots;
        }
    }
}
