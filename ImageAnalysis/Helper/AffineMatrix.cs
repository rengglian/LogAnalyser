using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ImageAnalysis.Helper;

public static class AffineMatrix
{
    public static Dictionary<string, double> CalculateMatrix(ObservableCollection<Spot> from, ObservableCollection<Spot> to)
    {
        Point2f[] from_ = new Point2f[from.Count];
        Point2f[] to_ = new Point2f[to.Count];

        int i = 0;
        from.ToList().ForEach(sp =>
        {
            from_[i++] = new Point2f((float)sp.Position.X, (float)sp.Position.Y);
        });

        i = 0;

        to.ToList().ForEach(sp =>
        {
            to_[i++] = new Point2f((float)sp.Position.X, (float)sp.Position.Y);
        });
        Mat affineMatrix = Cv2.EstimateAffine2D(InputArray.Create(from_), InputArray.Create(to_));

        return TransformToDict(affineMatrix);
    }
    private static Dictionary<string, double> TransformToDict(OpenCvSharp.Mat cvMat)
    {
        var dict = new Dictionary<string, double>
        {
            { "m11", cvMat.At<double>(0, 0) },
            { "m12", cvMat.At<double>(0, 1) },
            { "m21", cvMat.At<double>(1, 0) },
            { "m22", cvMat.At<double>(1, 1) },
            { "m13", Math.Round(cvMat.At<double>(0, 2), 0) },
            { "m23", Math.Round(cvMat.At<double>(1, 2), 0) }
        };

        return dict;
    }
}
