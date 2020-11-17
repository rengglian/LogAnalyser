using PatternAnalysis.IO;
using OxyPlot;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using PatternAnalysis.Interfaces;
using System.IO;

namespace PatternAnalysis.Helper
{
    public class Pattern : IPattern
    {
        private readonly byte alpha = 120;
        public List<DataPoint> Points { get; set; } = new List<DataPoint>();
        public string CheckSum { get; set; } = "";
        public string FileName { get; set; } = "";
        public Point Center { get; set; } = new Point();
        public OxyColor Color { get; set; }
        public int Count { get; set; }

        public Pattern(string fileName)
        {
            if (fileName != "None")
            {
                Points = JsonReader.Read(fileName);
            }
            CheckSum = CalculateCheckSum(Points);
            Center = CalculateCenter(Points);
            FileName = Path.GetFileName(fileName);
            Color = AssignColor(FileName);
            Count = Points.Count;
        }

        private OxyColor AssignColor(string fileName)
        {
            var color = OxyColor.FromAColor((byte)(alpha/2), OxyColors.Gray);
            if (fileName.Contains("PulseList-Raw"))
            {
                color = OxyColor.FromAColor(alpha, OxyColors.Chocolate);
            }
            else if (fileName.Contains("patternPositions") || fileName.Contains("PulseList-Edited"))
            {
                color = OxyColor.FromAColor(alpha, OxyColors.Coral);
            }
            else if (fileName.Contains("pulsePosition") || fileName.Contains("PulsePositions"))
            {
                color = OxyColor.FromAColor(alpha, OxyColors.CornflowerBlue);
            }
            else if (fileName.Contains("checkerPosition") || fileName.Contains("CheckerPositions"))
            {
                color = OxyColor.FromAColor(alpha, OxyColors.SlateBlue);
            }
            
            return color;
        }

        private static string CalculateCheckSum(List<DataPoint> pointList)
        {
            var sb = new StringBuilder();
            pointList.ForEach(p =>
            {
                sb.Append((int)p.X);
                sb.Append((int)p.Y);
            });
            var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(sb.ToString()));

            var sb2 = new StringBuilder(hash.Length);
            for (var i = 0; i < hash.Length; i++)
                sb2.Append(hash[i].ToString("x2"));

            return sb2.ToString();
        }

        private static Point CalculateCenter(List<DataPoint> pointList)
        {

            Point center = new Point { X = 0, Y = 0 };

            pointList.ForEach(p =>
            {
                center.X += p.X;
                center.Y += p.Y;
            });

            center.X /= pointList.Count;
            center.Y /= pointList.Count;

            return center;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
