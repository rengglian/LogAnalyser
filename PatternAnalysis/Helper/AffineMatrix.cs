//using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PatternAnalysis.Helper
{
    public class AffineMatrix
    {
        public static Dictionary<string, double> TransformToDict(OpenCvSharp.Mat cvMat)
        {
            var dict = new Dictionary<string, double>();
            dict.Add("m11", cvMat.At<double>(0, 0));
            dict.Add("m12", cvMat.At<double>(0, 1));
            dict.Add("m21", cvMat.At<double>(1, 0));
            dict.Add("m22", cvMat.At<double>(1, 1));
            dict.Add("m13", Math.Round(cvMat.At<double>(0, 2), 0));
            dict.Add("m23", Math.Round(cvMat.At<double>(1, 2), 0));

            return dict;
        }

        public static Dictionary<string, double> TransformToDict(OpenCvSharp.Mat cvMat, Point center)
        {
            var dict = TransformToDict(cvMat);
            dict.Add("a13", Math.Round(dict["m11"] * center.X + dict["m12"] * center.Y, 0));
            dict.Add("a23", Math.Round(dict["m21"] * center.X + dict["m22"] * center.Y, 0));
            return dict;
        }
    }
}
