using LorenzoTodisco;

namespace Francesco_Carlucci
{
    /// <summary>
    /// Consumable Item that increases maximum Mana.
    /// </summary>
    class ManaPotion : AbstractConsumableItem
    {
        /// <summary>
        /// Constructor for a Mana potion.
        /// </summary>
        /// <param name="name">of the item</param>
        /// <param name="position">of the item</param>
        /// <param name="modifier">to apply to the user of the item</param>
        public ManaPotion(string name, Position position, float modifier) : base(name, position, modifier) { }

        /// <summary>
        /// This potion can be always used.
        /// </summary>
        /// <param name="pg">the character on which the potion could be used</param>
        /// <returns>true</returns>
        public override bool CanUse(Character pg) => true;

        /// <summary>
        /// Uses the potion on the selected character.
        /// </summary>
        /// <param name="pg">the character on which the potion will be used</param>
        public override void Use(Character pg)
        {
            pg.MaxMana += (int) (pg.MaxMana * Modifier);
            pg.CurrentMana = pg.MaxMana;
        }
    }
}
