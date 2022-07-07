namespace LorenzoTodisco
{
    public class Character
    {
        private int _weaponIndex = 0;
        private int _hp;
        private bool _isDead;
        public int MaxHp { get; }
        public int CurrentHp
        {
            get
            {
                return _hp;
            }
            set
            {
                if (value <= 0)
                {
                    _hp = 0;
                    _isDead = true;
                }
                else if (value > MaxHp)
                {
                    _hp = MaxHp;
                }
                else
                {
                    _hp = value;
                }
            }
        }
        public int Speed { get; }
        public Position Pos { get; }
        public Inventory Inv { get; } = new Inventory();
        public List<Weapon> Weapons { get; } = new List<Weapon>();
        public Boolean IsDead { get => _isDead; }

        public Character(Weapon startingWeapon, int maxHp, int speed)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;
            Speed = speed;
            Pos = new Position(0, 0);
            Weapons.Add(startingWeapon);
        }

        public Weapon GetCurrentWeapon()
        {
            return Weapons.ElementAt(_weaponIndex);
        }

        private void SetCurrentWeapon(int index)
        {
            if (index >= 0 && index < Weapons.Count)
            {
                _weaponIndex = index;
            }
        }

        public void SwitchWeapon()
        {
            if (_weaponIndex < Weapons.Count - 1)
            {
                this.SetCurrentWeapon(++_weaponIndex);
            }
            else
            {
                this.SetCurrentWeapon(0);
            }
        }

        public Boolean CanHit(Character enemy)
        {
            return Math.Abs(enemy.Pos.X - this.Pos.X) <= GetCurrentWeapon().Range &&
                Math.Abs(enemy.Pos.Y - this.Pos.Y) <= GetCurrentWeapon().Range && !IsDead;
        }

        public void HitEnemy(Character enemy)
        {
            if (CanHit(enemy))
            {
                enemy.CurrentHp -= GetCurrentWeapon().Damage;
            }
        }

        public void PickUpItem(AbstractItem i)
        {
            if (Math.Abs(i.Position.X - this.Pos.X) < Speed &&
                Math.Abs(i.Position.Y - this.Pos.Y) < Speed && !IsDead)
            {
                if (i is Weapon weapon)
                {
                    Weapons.Add(weapon);
                }
                else
                {
                    this.Inv.AddItem(i);
                }
            }
        }

        private void UsePotionByType(string type)
        {
            foreach (Pair<AbstractItem, int> p in Inv.Inv)
            {
                if (p.First.GetType().Name.Contains(type) && ((AbstractConsumableItem)p.First).CanUse(this))
                {
                    ((AbstractConsumableItem)p.First).Use(this);
                    Inv.RemoveItem(p.First);
                    return;
                }
            }
        }

        public void UseHealthPotion()
        {
            UsePotionByType("Health");
        }

        public void Move(Dir dir)
        {
            if (!IsDead)
            {
                switch (dir)
                {
                    case Dir.Up:
                        Pos.Y = (Pos.Y + Speed);
                        break;
                    case Dir.Down:
                        Pos.Y = (Pos.Y - Speed);
                        break;
                    case Dir.Right:
                        Pos.X = (Pos.X + Speed);
                        break;
                    case Dir.Left:
                        Pos.X = (Pos.X - Speed);
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"Hp: {CurrentHp}/{MaxHp}, Weapon: {GetCurrentWeapon().Name}, " +
                $"Damage: {GetCurrentWeapon().Damage}, Range: {GetCurrentWeapon().Range}," +
                $" Position: {Pos}";
        }

        public string WeaponsToString()
        {
            string msg = "\nWeapons: ";
            foreach (Weapon weapon in Weapons)
            {
                msg += $"\n{weapon.Name}, Dmg: {weapon.Damage}, Range: {weapon.Range}";
            }
            return msg + "\n";
        }
    }
}
