namespace LorenzoTodisco
{
    class HealthPotion : AbstractConsumableItem
    {
        public HealthPotion(string name, Position position, float modifier) : base(name, position, modifier) { }

        public override bool CanUse(Character pg)
        {
            return pg.CurrentHp < pg.MaxHp;
        }

        public override void Use(Character pg)
        {
            pg.CurrentHp = pg.CurrentHp + (int)(pg.MaxHp * Modifier);
        }
    }
}
