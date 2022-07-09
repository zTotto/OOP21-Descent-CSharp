namespace Francesco_Carlucci
{
    /// <summary>
    /// Class that manages game levels.
    /// </summary>
    public class LevelsList
    {
        public List<Level> Levels { get; }
        private int _counter;

        /// <summary>
        /// Constructor for a levels list.
        /// </summary>
        /// <param name="levels">to add to the game</param>
        public LevelsList(List<Level> levels)
        {
            Levels = levels;
            _counter = 0;
        }

        /// <summary>
        /// Returns the current level.
        /// </summary>
        /// <returns>the current level</returns>
        public Level GetCurrentLevel() => Levels[_counter];

        /// <summary>
        /// Checks if there is another level.
        /// </summary>
        /// <returns>true if there is at least another level</returns>
        public bool HasNextLevel() => _counter < Levels.Count - 1;

        /// <summary>
        /// Returns the next level.
        /// </summary>
        /// <returns>the next level if there is one</returns>
        /// <exception cref="NotSupportedException">if there are no more levels</exception>
        public Level GetNextLevel() => HasNextLevel() ? Levels[++_counter] : throw new NotSupportedException();

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <returns>true if there are no more levels</returns>
        public bool IsGameOver() => !HasNextLevel();

        /// <summary>
        ///
        /// </summary>
        /// <returns>a string describing the levels list</returns>
        public override string ToString()
        {
            string s = "\nLevelsList: ";
            if (!Levels.Any())
            {
                s += $"total levels = {Levels.Count},"
                     + $"current level = {Levels.IndexOf(GetCurrentLevel()) + 1}";
            }
            else
            {
                s += "no levels";
            }
            return s;
        }
    }
}
