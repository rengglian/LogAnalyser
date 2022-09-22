using OpenCvSharp;

namespace PatternAnalysis.Helper;

public class DecomposeMatrix
{
    public Point2d Translation { get; set; }
    public double Rotation { get; set; }
    public Point2d Scale { get; set; }
    public Point2d Shear { get; set; }
}
