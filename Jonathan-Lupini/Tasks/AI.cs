using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jonathan_Lupini.Tasks.Supporting;
using Microsoft.VisualBasic;

namespace Jonathan_Lupini.Tasks
{
    /// <summary>
    /// Interface for monster's pathfinding
    /// </summary>
    public interface IPathfinding
    {
        /// <summary>
        /// Method <c>MoveMob</c> moves a mob according to a pathfinding algorithm.
        /// </summary>
        void MoveMob(Mob mob, Level level);

        /// <summary>
        /// Utility method <c>RandomDirection</c>.
        /// <returns>Returns a random direction</returns> 
        /// </summary>
        public static Direction RandomDirection()
        {
            Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
            Random random = new Random();
            return directions[random.Next(directions.Length)];
        }
    }

    /// <summary>
    /// Implementation of Ipathfinding interface with a simple pathfinding algorithm. 
    /// </summary>
    public class SimplePathfinding : IPathfinding
    {
        public void MoveMob(Mob mob, Level level)
        {
            Character hero = level.GetCharacter('H') ?? throw new InvalidOperationException();
            var herox = hero.Position.X;
            var heroy = hero.Position.Y;
            var mobx = mob.Position.X;
            var moby = mob.Position.Y;
            var startPos = mob.Position;
            var map = level.Map;

            if (!LineOfSight.IsTargetSeen(level, mob, hero))
            {
                Direction dir = IPathfinding.RandomDirection();
                if (level.ValidMovement(mob, dir)) mob.Position = mob.DirToPos(dir);
            }

            else if (herox > mobx)
            {
                if (level.ValidMovement(mob, Direction.Right))
                {
                    mob.Position = mob.DirToPos(Direction.Right);
                }
                else if (heroy > moby && level.ValidMovement(mob, Direction.Down))
                {
                    mob.Position = mob.DirToPos(Direction.Down);
                }
                else UnstuckMob(mob, level);
            }
            else if (herox < mobx)
            {
                if (level.ValidMovement(mob, Direction.Left))
                {
                    mob.Position = mob.DirToPos(Direction.Left);
                }
                else if (heroy > moby && level.ValidMovement(mob, Direction.Down))
                {
                    mob.Position = mob.DirToPos(Direction.Down);
                }
                else UnstuckMob(mob, level);
            }
            else
            {
                if (heroy > moby && level.ValidMovement(mob, Direction.Down))
                {
                    mob.Position = mob.DirToPos(Direction.Down);
                }
                else if (level.ValidMovement(mob, Direction.Up))
                {
                    mob.Position = mob.DirToPos(Direction.Up);
                }
                else UnstuckMob(mob, level);
            }
        }

        /// <summary>
        /// Moves a mob randomly until it's position changes.
        /// </summary>
        static void UnstuckMob(Mob mob, Level level)
        {
            var startPos = mob.Position;
            Direction dir;
            Point newPos;
            do
            {
                dir = IPathfinding.RandomDirection();
                if (level.ValidMovement(mob, dir))
                {
                    mob.Position = mob.DirToPos(dir);
                }
                newPos = mob.Position;
            } while (newPos.Equals(startPos));
        }

    }

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
