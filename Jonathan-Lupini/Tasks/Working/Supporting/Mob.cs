using System.Drawing;

namespace Jonathan_Lupini.Tasks.Working.Supporting
{
    /// <summary>
    /// Class modeling a Mob, it extends the Character class.
    /// </summary>
    public class Mob : Character
    {
        private readonly IPathfinding pathfinding = new SimplePathfinding();
        public Mob(Point position, char symbol) : base(position, symbol)
        {
        }

        /// <summary>
        /// Method called every time the game is updated, it decides if the mob
        /// should attack or move.
        /// </summary>
        public void Update(Level level)
        {
            Character hero = level.GetCharacter('H') ?? throw new InvalidOperationException();
            if (IsInRange(hero)) Attack(hero);
            else pathfinding.MoveMob(this, level);
        }
    }
}
