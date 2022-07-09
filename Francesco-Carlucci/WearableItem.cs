using LorenzoTodisco;

namespace Francesco_Carlucci
{
    /// <summary>
    /// Class to model a wearable item, like a ring, an amulet or an armor that gives some buff.
    /// </summary>
    public class WearableItem : AbstractItem
    {
        public double? Health = null;
        public double? Power = null;
        public int? Exp = null;

        /// <summary>
        /// Constructor for a WearableItem.
        /// </summary>
        /// <param name="name">of the item</param>
        /// <param name="position">of the item</param>
        /// <param name="health">to add to the character</param>
        /// <param name="power">to add to the weapon of the character</param>
        /// <param name="exp">to add to the character</param>
        public WearableItem(string name, Position position, double? health, double? power, int? exp) : base(name, position)
        {
            Health = health;
            Power = power;
            Exp = exp;
        }

        /// <summary>
        /// The selected pg wear the item getting its buffs.
        /// </summary>
        /// <param name="pg">the player that gets the buffs</param>
        public void Wear(Character pg)
        {
            if (Health.HasValue)
            {
                pg.MaxHp += (int)(pg.MaxHp * Health.Value);
                pg.CurrentHp = pg.MaxHp;
            }
            if (Power.HasValue)
            {
                Weapon weapon = pg.GetCurrentWeapon();
                weapon.Damage += (int)(weapon.Damage * Power.Value);
            }
            if (Exp.HasValue)
            {
                pg.Exp += Exp.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>a string describing the WearableItem</returns>
        public override string ToString()
        {
            return $"\nWearable item: {Name} [{Position}]"
                   + $"\nHealth: {Health ?? 0}"
                   + $"\nPower: {Power ?? 0}"
                   + $"\nExp: {Exp ?? 0}";
        }

        /// <summary>
        /// Class to build a WearableItem.
        /// </summary>
        public class Builder
        {
            private readonly string _name;
            private readonly Position _position;
            private double? _health = null;
            private double? _power = null;
            private int? _exp = null;

            /// <summary>
            /// Constructor for the WearableItem Builder.
            /// </summary>
            /// <param name="name">of the item</param>
            /// <param name="position">of the item</param>
            public Builder(string name, Position position)
            {
                _name = name;
                _position = position;
            }

            /// <summary>
            /// Increase the pg maximum health.
            /// </summary>
            /// <param name="mod">to apply to the pg to increase maximum life ranging from 0 to 1 excluded</param>
            /// <returns>this for chaining</returns>
            public Builder Health(double mod)
            {
                if (mod is > 0 and < 1)
                {
                    _health = mod;
                }
                return this;
            }

            /// <summary>
            /// Increase the damage of the player's current weapon.
            /// </summary>
            /// <param name="mod">to apply to the pg to icrease the damage ranging from 0 to 1 excluded</param>
            /// <returns>this for chaining</returns>
            public Builder Power(double mod)
            {
                if (mod is > 0 and < 1)
                {
                    _power = mod;
                }
                return this;
            }

            /// <summary>
            /// Add experience to the pg.
            /// </summary>
            /// <param name="e">experience to add to the pg ranging from 1 to 999</param>
            /// <returns>this for chaining</returns>
            public Builder Exp(int e)
            {
                if (e is > 0 and < 1000)
                {
                    _exp = e;
                }
                return this;
            }

            /// <summary>
            /// Build a WearableItem with the chosen attributes.
            /// </summary>
            /// <returns>a WearableItem</returns>
            /// <exception cref="NotSupportedException">if no attributes are added</exception>
            public WearableItem Build()
            {
                if(_name == null || _position == null
                    || (!_health.HasValue && !_power.HasValue && !_exp.HasValue))
                {
                    throw new NotSupportedException("No attributes added");
                }
                return new WearableItem(_name, _position, _health, _power, _exp);
            }
        }
    }
}
