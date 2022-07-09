using System.Drawing;
using System.Numerics;
using Jonathan_Lupini.Tasks.Supporting;

namespace Jonathan_Lupini.Tasks
{
    

    /// <summary>
    /// Utility class for calculating line of sight.
    /// </summary>
    public static class LineOfSight
    {
        /// <summary>
        /// Utility method.
        /// <returns>True if there are no sight-blocking obstacles between the 2 characters,
        /// false otherwise</returns> 
        /// </summary>
        public static bool IsTargetSeen(Level level, Character observer, Character target)
        {
            var tilesBetween = Line(observer.Position, target.Position);
            foreach (var tile in tilesBetween)
            {
                //Console.Write(tile+" ");
                if (level.Map.IsWall(tile)) return false;
            }
            return true;
        }

        /// <summary>
        /// Utility method.
        /// <returns>A Vector2 of the character's position</returns> 
        /// </summary>
        public static Vector2 CharacterVector(Character character)
        {
            float x = character.Position.X;
            float y = character.Position.Y;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Utility method <c>Line</c> that returns an array of points snapped to the game's grid, 
        /// indicating the line of sight between two entities.
        /// It uses a variation of Bresenham's line algorithm, taken from <see href="https://www.redblobgames.com/grids/line-drawing.html">HERE</see>
        /// </summary>
        public static Point[] Line(Point p0, Point p1)
        {
            List<Point> points = new List<Point>();
            var N = DiagonalDistance(p0, p1);
            //Console.WriteLine("N " + N);
            for (double step = 0; step <= N; step++)
            {
                double t = N == 0 ? 0.0 : step / N;
                points.Add(RoundPoint(LerpPoint(p0, p1, t)));
                //Console.WriteLine("Step = " + step + " T = " + t + " Lerp_point(" + p0 + "," + p1 + "," + t + ") = "
                //                  + LerpPoint(p0, p1, t) + " round_point(" + LerpPoint(p0, p1, t) + ") = " + RoundPoint(LerpPoint(p0, p1, t)));
            }
            return points.ToArray();
        }

        static int DiagonalDistance(Point p0, Point p1)
        {
            var dx = p1.X - p0.X;
            var dy = p1.Y - p0.Y;
            return Math.Max(Math.Abs(dx), Math.Abs(dy));
        }

        static Point RoundPoint(Tuple<double, double> p)
        {
            var (x, y) = p;
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        static Tuple<double, double> LerpPoint(Point p0, Point p1, double t)
        {
            return new(Lerp(p0.X, p1.X, t),
                Lerp(p0.Y, p1.Y, t));
        }

        private static double Lerp(int start, int end, double t)
        {
            return start + t * (end - start);
        }

    }

}
