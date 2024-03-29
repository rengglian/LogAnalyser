﻿using OpenCvSharp;
using PatternAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PatternAnalysis.Helper;

public class AffineMatrix
{
    public static Dictionary<string, double> CalculateMatrix(IPattern from, IPattern to)
    {
        Point2f[] from_ = new Point2f[from.Points.Count];
        Point2f[] to_ = new Point2f[to.Points.Count];

        int i = 0;
        from.Points.ForEach(pt =>
        {
            from_[i++] = new Point2f((float)pt.X, (float)pt.Y);
        });

        i = 0;

        to.Points.ForEach(pt =>
        {
            to_[i++] = new Point2f((float)pt.X, (float)pt.Y);
        });

        Mat inliers = new();

        Mat affineMatrix = Cv2.EstimateAffine2D(InputArray.Create(from_), InputArray.Create(to_), inliers, RobustEstimationAlgorithms.RANSAC, 300.0, 2000, 0.99, 10);

        PrintMatAsync(inliers);
    

        return TransformToDict(affineMatrix);
    }
    private static Dictionary<string, double> TransformToDict(Mat cvMat)
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

    public static Dictionary<string, double> Decompose(Dictionary<string, double> affineMatrix)
    {
        var decomposedMatrix = new DecomposeMatrix();

        var delta = affineMatrix["m11"] * affineMatrix["m22"] - affineMatrix["m12"] * affineMatrix["m21"];

        if (affineMatrix["m11"] != 0 || affineMatrix["m12"] != 0) {
            var r = Math.Sqrt(Math.Pow(affineMatrix["m11"], 2) + Math.Pow(affineMatrix["m12"], 2));
            decomposedMatrix.Rotation = affineMatrix["m12"] > 0 ? Math.Acos(affineMatrix["m11"] / r) : -Math.Acos(affineMatrix["m11"] / r);
            decomposedMatrix.Scale = new Point2d(r, delta / r);
            decomposedMatrix.Shear = new Point2d(180 / Math.PI * Math.Atan((affineMatrix["m11"] * affineMatrix["m21"] + affineMatrix["m12"] * affineMatrix["m22"]) / (Math.Pow(r, 2))), 0.0);
        }
        else if (affineMatrix["m21"] != 0 || affineMatrix["m22"] != 0)
        {
            var s = Math.Sqrt(Math.Pow(affineMatrix["m21"], 2) + Math.Pow(affineMatrix["m22"], 2));
            decomposedMatrix.Rotation = Math.PI / 2 - (affineMatrix["m22"] > 0 ? Math.Acos(-affineMatrix["m21"] / s) : -Math.Acos(affineMatrix["m21"] / s));
            decomposedMatrix.Scale = new Point2d(delta / s, s);
            decomposedMatrix.Shear = new Point2d(0.0, 180 / Math.PI * Math.Atan((affineMatrix["m11"] * affineMatrix["m21"] + affineMatrix["m12"] * affineMatrix["m22"]) / (Math.Pow(s, 2))));
        }

        decomposedMatrix.Rotation *= 180 / Math.PI;

        decomposedMatrix.Translation = new Point2d(affineMatrix["m13"], affineMatrix["m23"]);

        var dict = new Dictionary<string, double>
        {
            { "ΔX", decomposedMatrix.Translation.X },
            { "ΔY", decomposedMatrix.Translation.Y },
            { "Scale X", decomposedMatrix.Scale.X },
            { "Scale Y", decomposedMatrix.Scale.Y },
            { "Shear X", decomposedMatrix.Shear.X },
            { "Shear Y", decomposedMatrix.Shear.Y },
            { "Rotation", decomposedMatrix.Rotation }
        };

        return dict;
    }

    public static List<System.Windows.Point> CalculateBack(List<System.Windows.Point> pts, Dictionary<string, double> affineMatrix)
    {
        List<System.Windows.Point> calc_points = new();

        pts.ForEach(pt =>
        {
            var x = affineMatrix["m11"] * pt.X + affineMatrix["m12"] * pt.Y + affineMatrix["m13"];
            var y = affineMatrix["m21"] * pt.X + affineMatrix["m22"] * pt.Y + affineMatrix["m23"];

            calc_points.Add(new System.Windows.Point(x, y));
        });

        return calc_points;
    }

    private static void PrintMatAsync(Mat mat)
    {
        int inliers = 0;
        for (var rowIndex = 0; rowIndex < mat.Rows; rowIndex++)
        {
            for (var colIndex = 0; colIndex < mat.Cols; colIndex++)
            {
                if (mat.At<bool>(rowIndex, colIndex)) inliers++;
                
            }
            
        }
        Debug.WriteLine(inliers);
    }

}
