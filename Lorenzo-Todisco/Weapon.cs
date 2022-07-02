namespace LorenzoTodisco
{
    class Weapon : AbstractItem
    {

        public int Damage { get; set; }
        public int Range { get; set; }

        public Weapon(string name, int damage, int range, Position pos) : base(name, pos)
        {
            Damage = damage;
            Range = range;
        }

        public override string ToString()
        {
            return base.ToString() + $", Damage: {Damage}, Range: {Range}";
        }
    }
}