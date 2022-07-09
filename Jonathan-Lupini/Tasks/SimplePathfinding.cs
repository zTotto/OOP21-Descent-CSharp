using System.Drawing;
using Jonathan_Lupini.Tasks.Supporting;

namespace Jonathan_Lupini.Tasks
{
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
}
