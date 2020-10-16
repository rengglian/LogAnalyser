using PatternAnalysis.IO;
using OxyPlot;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using PatternAnalysis.Interfaces;

namespace PatternAnalysis.Helper
{
    public class Pattern : IPattern
    {
        public List<DataPoint> Points { get; set; } = new List<DataPoint>();
        public string CheckSum { get; set; } = "";
        public Point Center { get; set; } = new Point();

        public Pattern(string fileName)
        {
            Points = JsonReader.Read(fileName);
            CheckSum = CalculateCheckSum(Points);
            Center = CalculateCenter(Points);
        }

        private string CalculateCheckSum(List<DataPoint> pointList)
        {
            var sb = new StringBuilder();
            pointList.ForEach(p =>
            {
                sb.Append(((int)p.X).ToString());
                sb.Append(((int)p.Y).ToString());
            });
            var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(sb.ToString()));

            var sb2 = new StringBuilder(hash.Count());
            for (var i = 0; i < hash.Count(); i++)
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

    }
}
