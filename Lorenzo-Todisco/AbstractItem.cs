namespace LorenzoTodisco
{
    abstract class AbstractItem
    {
        private Position _pos;
        public string Name { get; set; }
        public Position Position { get => _pos; }

        public AbstractItem(string name, Position position)
        {
            Name = name;
            _pos = position;
        }

        public AbstractItem(string name, int x, int y)
        {
            Name = name;
            _pos = new Position(x, y);
        }

        public void setPos(Position p)
        {
            _pos.SetPosition(p);
        }

        public void setPos(int x, int y)
        {
            _pos.SetPosition(x, y);
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