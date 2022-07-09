using System.Drawing;
using NUnit.Framework;

namespace Jonathan_Lupini.Tasks.Supporting
{
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

        public void Move(Point targetPos)
        {
            Position = targetPos;
        }

        public void Move(Direction dir)
        {
            Position = DirToPos(dir);
        }

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
        public void Attack(Character enemy)
        {
            enemy.Hp -= Damage;
        }

        public bool IsInRange(Character target)
        {
            var x = Position.X;
            var y = Position.Y;
            var xt = target.Position.X;
            var yt = target.Position.Y;
            return x >= xt - 1 && x <= xt + 1 && y >= yt - 1 && y <= yt + 1;
        }

    }

    public class Mob : Character
    {
        private readonly IPathfinding pathfinding = new SimplePathfinding();
        public Mob(Point position, char symbol) : base(position, symbol)
        {
        }

        public void Update(Level level)
        {
            Character hero = level.GetCharacter('H') ?? throw new InvalidOperationException();
            if (IsInRange(hero)) Attack(hero);
            else pathfinding.MoveMob(this, level);
        }
    }
}

