namespace LorenzoTodisco
{
    class Position
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Position(Position pos)
        {
            this.X = pos.X;
            this.Y = pos.Y;
        }

        public void SetPosition(Position pos)
        {
            this.X = pos.X;
            this.Y = pos.Y;
        }

        public void SetPosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position GetPosition()
        {
            return new Position(this.X, this.Y);
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Position pos = (Position)obj;
                return (X == pos.X) && (Y == pos.Y);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}