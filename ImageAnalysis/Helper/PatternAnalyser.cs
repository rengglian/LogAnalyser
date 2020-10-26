using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ImageAnalysis.Helper
{
    public static class PatternAnalyser
    {
        public static List<Spot> SortList(List<Spot> unsorted)
        {
            var center = CenterSpot(unsorted);
            var corners = Corners(unsorted, center);

            var slope_X = CalculateSlope(corners[0], corners[1]);
            var slope_Y = CalculateSlope(corners[2], corners[3]);

            var offset_X = CalculateOffset(corners[0], slope_X);
            var offset_Y = CalculateOffset(corners[2], slope_Y);

            var delta_Xx = (corners[1].Position.X - corners[0].Position.X) / (13.0 - 1);
            var delta_Yx = (corners[3].Position.X - corners[2].Position.X) / (13.0 - 1);

            List<Spot> sortedX = new List<Spot>();
            List<Spot> sortedY = new List<Spot>();

            for (int i = 0; i < 13; i++)
            {
                var Xx = corners[0].Position.X + delta_Xx * i;
                var Xy = slope_X * Xx + offset_X;
                var Yx = corners[2].Position.X + delta_Yx * i;
                var Yy = slope_Y * Yx + offset_Y;

                var closest_X = unsorted.OrderBy(spot => spot.Distance(new System.Windows.Point(Xx, Xy))).First();
                var closest_Y = unsorted.OrderBy(spot => spot.Distance(new System.Windows.Point(Yx, Yy))).First();
                closest_X.Color = Brushes.Crimson;
                closest_Y.Color = Brushes.DarkOrchid;

                sortedX.Add(closest_X);
                sortedY.Add(closest_Y);
            }
            sortedX.AddRange(sortedY);

            return sortedX;
        }

        private static List<Spot> Corners(List<Spot> spots, Spot center)
        {
            List<Spot> corners = new List<Spot>
            {
                center,
                center,
                center,
                center
            };

            spots.ForEach(spot =>
            {
                if (spot.Position.X > corners[0].Position.X && spot.Position.Y >= corners[0].Position.Y) corners[0] = spot;
                if (spot.Position.X < corners[1].Position.X && spot.Position.Y <= corners[1].Position.Y) corners[1] = spot;
                if (spot.Position.X >= corners[2].Position.X && spot.Position.Y < corners[2].Position.Y) corners[2] = spot;
                if (spot.Position.X <= corners[3].Position.X && spot.Position.Y > corners[3].Position.Y) corners[3] = spot;
            });

            corners[0].Color = Brushes.DarkBlue;
            corners[1].Color = Brushes.LightBlue;
            corners[2].Color = Brushes.DarkGreen;
            corners[3].Color = Brushes.LightGreen;

            return corners;
        }

        private static double CalculateSlope(Spot start, Spot end)
        {
            return (end.Position.Y - start.Position.Y) / (end.Position.X - start.Position.X);
        }
        private static double CalculateOffset(Spot spot, double slope)
        {
            return spot.Position.Y - slope * spot.Position.X;
        }

        private static Spot CenterSpot(List<Spot> spots)
        {
            var avg_x = spots.Average(pt => pt.Position.X);
            var avg_y = spots.Average(pt => pt.Position.Y);

            var closest = spots.OrderBy(spot => spot.Distance(new System.Windows.Point(avg_x, avg_y))).First();
            closest.Color = Brushes.Red;
            return closest;
        }
    }
}
