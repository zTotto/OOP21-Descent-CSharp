namespace LorenzoTodisco
{
    public abstract class AbstractItem
    {
        public string Name { get; set; }
        public Position Position { get; }

        public AbstractItem(string name, Position position)
        {
            Name = name;
            Position = position;
        }

        public AbstractItem(string name, int x, int y)
        {
            Name = name;
            Position = new Position(x, y);
        }

        public void SetPos(Position p)
        {
            Position.SetPosition(p);
        }

        public void SetPos(int x, int y)
        {
            Position.SetPosition(x, y);
        }
        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                AbstractItem i = (AbstractItem)obj;
                return (Name == i.Name) && (Position == i.Position);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"\n{Name} [{Position}]";
        }
    }
}