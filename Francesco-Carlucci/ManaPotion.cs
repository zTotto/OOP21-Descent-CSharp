using LorenzoTodisco;

namespace Francesco_Carlucci
{
    class ManaPotion : AbstractConsumableItem
    {
        public ManaPotion(string name, Position position, float modifier) : base(name, position, modifier) { }

        public override bool CanUse(Character pg)
        {
            return true;
        }

        public override void Use(Character pg)
        {
            pg.MaxMana += (int) (pg.MaxMana * Modifier);
            pg.CurrentMana = pg.MaxMana;
        }
    }
}
