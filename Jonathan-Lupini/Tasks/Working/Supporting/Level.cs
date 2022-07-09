using System.Drawing;

namespace Jonathan_Lupini.Tasks.Working.Supporting
{
    /// <summary>
    /// Class modeling a level of the game.
    /// </summary>
    public class Level
    {
        public DescentMap DescentMap { get; }
        public List<Character> Characters { get; } = new List<Character>();
        public Level(DescentMap descentMap)
        {
            DescentMap = descentMap;
        }

        /// <summary>
        /// Returns True if the given point isn't a wall and isn't already occupied by
        /// another character, false otherwise.
        /// </summary>
        public bool ValidMovement(Character pg, Direction dir)
        {
            if (DescentMap.IsWall(pg.DirToPos(dir))) return false;
            if (IsCharacter(pg.DirToPos(dir))) return false;
            return true;
        }

        /// <summary>
        /// Returns True if a given point is occupied by a character.
        /// </summary>
        private bool IsCharacter(Point pos)
        {
            return Characters.Any(pg => pg.Position == pos);
        }

        /// <summary>
        /// Prints the game level to the console.
        /// </summary>
        public void PrintLevel()
        {
            for (int y = 0; y < DescentMap.Rows; y++)
            {
                for (int x = 0; x < DescentMap.Columns; x++)
                {
                    char toPrint = DescentMap.MapStatus[new Point(x, y)];
                    foreach (var character in Characters)
                    {
                        if (new Point(x, y).Equals(character.Position))
                        {
                            toPrint = character.Symbol;
                            if (character.Hp <= 0) toPrint = '*';
                            continue;
                        }
                    }
                    Console.Write(toPrint);
                }
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Prints the game level to the console with lines of sight drawn.
        /// </summary>
        public void PrintLevelWithSight(Character c1, Character c2)
        {
            for (int y = 0; y < DescentMap.Rows; y++)
            {
                for (int x = 0; x < DescentMap.Columns; x++)
                {
                    char toPrint = DescentMap.MapStatus[new Point(x, y)];
                    foreach (var character in Characters)
                    {
                        if (new Point(x, y).Equals(character.Position))
                        {
                            toPrint = character.Symbol;
                            continue;
                        }
                    }
                    foreach (var tile in LineOfSight.Line(c1.Position, c2.Position))
                    {
                        if (new Point(x, y).Equals(tile) && c1.Position != tile && c2.Position != tile)
                        {
                            toPrint = 'S';
                        }
                    }
                    Console.Write(toPrint);
                }
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Returns a character that has a given symbol if present, returns null otherwise
        /// </summary>
        public Character? GetCharacter(char symbol)
        {
            foreach (var pg in Characters)
            {
                if (pg.Symbol.Equals(symbol))
                {
                    return pg;
                }
            }
            return null;
        }
    }


}
