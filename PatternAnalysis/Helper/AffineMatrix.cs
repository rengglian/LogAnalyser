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
            dict.Add("m13", cvMat.At<double>(0, 2));
            dict.Add("m21", cvMat.At<double>(1, 0));
            dict.Add("m22", cvMat.At<double>(1, 1));
            dict.Add("m23", cvMat.At<double>(1, 2));

            return dict;
        }

        public static Dictionary<string, double> TransformToDict(OpenCvSharp.Mat cvMat, Point center)
        {
            var dict = TransformToDict(cvMat);
            dict["m13"] = cvMat.At<double>(0, 0) * center.X + cvMat.At<double>(0, 1) * center.Y;
            dict["m23"] = cvMat.At<double>(1, 0) * center.X + cvMat.At<double>(1, 1) * center.Y;
            return dict;
        }
    }
}
