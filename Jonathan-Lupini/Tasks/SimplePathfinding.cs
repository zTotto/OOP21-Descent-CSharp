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
            Point mobPos = mob.Position;

            if (!LineOfSight.IsTargetSeen(level, mob, hero))
            {
                SimpleMove(mob, level);
            }

            else if (herox > mobx)
            {
                MoveMob(mob, Direction.Right, level);
                if (heroy > moby && !HasCharacterMoved(mobPos, mob.Position))
                {
                    MoveMob(mob, Direction.Down, level);
                    if (!HasCharacterMoved(mobPos, mob.Position))
                    {
                        UnstuckMob(mob, level);
                    }
                }
                else if (heroy < moby && !HasCharacterMoved(mobPos, mob.Position))
                {
                    MoveMob(mob, Direction.Up, level);
                    if (!HasCharacterMoved(mobPos, mob.Position))
                    {
                        UnstuckMob(mob, level);
                    }
                }
                else if (!HasCharacterMoved(mobPos, mob.Position))
                {
                    UnstuckMob(mob, level);
                }
            }
            else if (herox < mobx)
            {
                MoveMob(mob, Direction.Left, level);
                if (heroy > moby && !HasCharacterMoved(mobPos, mob.Position))
                {
                    MoveMob(mob, Direction.Down, level);
                    if (!HasCharacterMoved(mobPos, mob.Position))
                    {
                        UnstuckMob(mob, level);
                    }
                }
                else if (heroy < moby && !HasCharacterMoved(mobPos, mob.Position))
                {
                    MoveMob(mob, Direction.Up, level);
                    if (!HasCharacterMoved(mobPos, mob.Position))
                    {
                        UnstuckMob(mob, level);
                    }
                }
                else if (!HasCharacterMoved(mobPos, mob.Position))
                {
                    UnstuckMob(mob, level);
                }
            }
            else
            {
                if (heroy > moby)
                {
                    MoveMob(mob, Direction.Down, level);
                }
                else if (heroy < moby)
                {
                    MoveMob(mob, Direction.Up, level);
                }

                if (!HasCharacterMoved(mobPos, mob.Position))
                {
                    UnstuckMob(mob, level);
                }
            }
        }

        /// <summary>
        /// Moves the mob towards a random direction.
        /// </summary>
        void SimpleMove(Mob mob, Level level)
        {
            Direction dir = IPathfinding.RandomDirection();
            if (level.ValidMovement(mob, dir)) mob.Position = mob.DirToPos(dir);
        }

        /// <summary>
        /// Returns true if the 2 positions are equivalent, false otherwise.
        /// </summary>
        bool HasCharacterMoved(Point pos1, Point pos2)
        {
            return !pos1.Equals(pos2);
        }

        /// <summary>
        /// If the movement is valid moves the mob towards a given direction.
        /// </summary>
        void MoveMob(Mob mob, Direction dir, Level level)
        {
            if (level.ValidMovement(mob, dir)) mob.Position = mob.DirToPos(dir);
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
