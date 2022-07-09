using System.Drawing;

namespace Jonathan_Lupini.Tasks.Supporting
{
    public class Level
    {
        public Map Map { get; }
        public List<Character> Characters { get; } = new List<Character>();
        public Level(Map map)
        {
            Map = map;
        }

        public bool ValidMovement(Point pos)
        {
            if (Map.IsWall(pos)) return false;
            if (IsCharacter(pos)) return false;
            return true;
        }

        public bool ValidMovement(Character pg, Direction dir)
        {
            return ValidMovement(pg.DirToPos(dir));
        }

        private bool IsCharacter(Point pos)
        {
            return Characters.Any(pg => pg.Position == pos);
        }

        public void PrintLevel()
        {
            for (int y = 0; y < Map.Rows; y++)
            {
                for (int x = 0; x < Map.Columns; x++)
                {
                    char toPrint = Map.MapStatus[new Point(x, y)];
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
        public void PrintLevelWithSight(Character c1, Character c2)
        {
            for (int y = 0; y < Map.Rows; y++)
            {
                for (int x = 0; x < Map.Columns; x++)
                {
                    char toPrint = Map.MapStatus[new Point(x, y)];
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
