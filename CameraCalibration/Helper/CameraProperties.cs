using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CameraCalibration.Helper
{
    public class CameraProperties : CameraResolution
    {
        public double Rotation { get; set; }
        public Point Center { get; set; }
    }
}
