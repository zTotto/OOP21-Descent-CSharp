using Jonathan_Lupini.Tasks.Supporting;

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
}

