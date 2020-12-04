using Infrastructure.Helper;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;

namespace ImageAnalysis.Helper
{
    public class PatternMessage
    {
        public static List<Spot> Parse(string message, Dictionary<string, Options> props)
        {
            var json = JsonSerializer.Deserialize<List<Point>>(message);
            var unique_items = new HashSet<Point>(json);
            
            var offset = new Point(props["Image Width"].Value / 2.0, props["Image Height"].Value / 2.0);
            var scale = new Point(1.0 / props["X um / px"].Value, 1.0 / props["Y um / px"].Value);

            List<Spot> spots = new List<Spot>();
            foreach (Point pt in unique_items)
            {
                var scaledX = pt.X * scale.X + offset.X;
                var scaledY = pt.Y * scale.Y + offset.Y;
                spots.Add(new Spot(new Point(scaledX, scaledY), 5, Brushes.AliceBlue));
            }

            spots.Add(new Spot(new Point(offset.X, offset.Y), 5, Brushes.AliceBlue));
            return spots;
        }
    }
}
