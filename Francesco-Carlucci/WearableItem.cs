using LorenzoTodisco;

namespace Francesco_Carlucci
{
    public class WearableItem : AbstractItem
    {
        public double? Health = null;
        public double? Power = null;
        public int? Exp = null;

        public WearableItem(string name, Position position, double? health, double? power, int? exp) : base(name, position)
        {
            Health = health;
            Power = power;
            Exp = exp;
        }

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

        public override string ToString()
        {
            return $"\nWearable item: {Name} [{Position}]"
                   + $"\nHealth: {Health ?? 0}"
                   + $"\nPower: {Power ?? 0}"
                   + $"\nExp: {Exp ?? 0}";
        }

        public class Builder
        {
            private readonly string _name;
            private readonly Position _position;
            private double? _health = null;
            private double? _power = null;
            private int? _exp = null;

            public Builder(string name, Position position)
            {
                _name = name;
                _position = position;
            }

            public Builder Health(double mod)
            {
                if (mod is > 0 and < 1)
                {
                    _health = mod;
                }
                return this;
            }
            
            public Builder Power(double mod)
            {
                if (mod is > 0 and < 1)
                {
                    _power = mod;
                }
                return this;
            }
            
            public Builder Exp(int mod)
            {
                if (mod is > 0 and < 1000)
                {
                    _exp = mod;
                }
                return this;
            }

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
