namespace Francesco_Carlucci
{
    public class LevelsList
    {
        public List<Level> Levels { get; }
        private int _counter;

        public LevelsList(List<Level> levels)
        {
            Levels = levels;
            _counter = 0;
        }

        public Level GetCurrentLevel() => Levels[_counter];

        public bool HasNextLevel() => _counter < Levels.Count - 1;

        public Level GetNextLevel() => HasNextLevel() ? Levels[++_counter] : throw new NotSupportedException();

        public bool IsGameOver() => !HasNextLevel();

        public override string ToString()
        {
            string s = "\nLevelsList: ";
            if (!Levels.Any())
            {
                s += "total levels = " + Levels.Count + ", current level = "
                    + (Levels.IndexOf(GetCurrentLevel()) + 1);
            }
            else
            {
                s += "no levels";
            }
            return s;
        }
    }
}
