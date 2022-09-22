using System.Windows;

namespace CameraCalibration.Helper;

public class CameraProperties : CameraResolution
{
    public double Rotation { get; set; }
    public Point Center { get; set; }
}
