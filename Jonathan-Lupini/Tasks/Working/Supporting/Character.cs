using System.Drawing;

namespace Jonathan_Lupini.Tasks.Working.Supporting
{
    /// <summary>
    /// Class modeling a Game Character
    /// </summary>
    public class Character
    {
        public Point Position { get; set; }
        public int Hp { get; set; } = 15;

        public int Damage { get; set; } = 5;

        public char Symbol { get; }

        public Character(Point position, char symbol)
        {
            Position = position;
            Symbol = symbol;
        }

        /// <summary>
        /// Return the Point representing the position of the character if
        /// he were to move in the given direction.
        /// </summary>
        public Point DirToPos(Direction dir)
        {
            var x = Position.X;
            var y = Position.Y;
            return dir switch
            {
                Direction.Right => new Point(x + 1, y),
                Direction.Left => new Point(x - 1, y),
                Direction.Up => new Point(x, y - 1),
                Direction.Down => new Point(x, y + 1),
                Direction.Still => Position,
                _ => Position
            };
        }

        /// <summary>
        /// This character deals damage to target character.
        /// </summary>
        public void Attack(Character enemy)
        {
            enemy.Hp -= Damage;
        }

        /// <summary>
        /// Returns true if this character is adjacent to target character
        /// false otherwise
        /// </summary>
        public bool IsInRange(Character target)
        {
            var x = Position.X;
            var y = Position.Y;
            var xt = target.Position.X;
            var yt = target.Position.Y;
            return x >= xt - 1 && x <= xt + 1 && y >= yt - 1 && y <= yt + 1;
        }

    }
}

