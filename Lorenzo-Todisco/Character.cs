namespace LorenzoTodisco
{
    internal class Character
    {
        private Inventory _inv = new Inventory();
        private List<Weapon> _weapons = new List<Weapon>();
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
                else
                {
                    _hp = value;
                }
            }
        }
        public int Speed { get; }
        public Position Pos { get; }
        public Inventory Inv { get => _inv; }
        public List<Weapon> Weapons { get => _weapons; }
        public Boolean IsDead { get => _isDead; }

        public Character(Weapon startingWeapon, int maxHp, int speed)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;
            Speed = speed;
            Pos = new Position(0, 0);
            _weapons.Add(startingWeapon);
        }

        public Weapon getCurrentWeapon()
        {
            return _weapons.ElementAt(_weaponIndex);
        }

        private void setCurrentWeapon(int index)
        {
            if (index >= 0 && index < _weapons.Count)
            {
                _weaponIndex = index;
            }
        }

        public void switchWeapon()
        {
            if (_weaponIndex < _weapons.Count - 1)
            {
                this.setCurrentWeapon(++_weaponIndex);
            }
            else
            {
                this.setCurrentWeapon(0);
            }
        }

        public Boolean canHit(Character enemy)
        {
            return Math.Abs(enemy.Pos.X - this.Pos.X) <= getCurrentWeapon().Range &&
                Math.Abs(enemy.Pos.Y - this.Pos.Y) <= getCurrentWeapon().Range && !IsDead;
        }

        public void hitEnemy(Character enemy)
        {
            if (canHit(enemy))
            {
                enemy.CurrentHp = enemy.CurrentHp - getCurrentWeapon().Damage;
            }
        }

        public void pickUpItem(AbstractItem i)
        {
            if (Math.Abs(i.Position.X - this.Pos.X) < Speed &&
                Math.Abs(i.Position.Y - this.Pos.Y) < Speed && !IsDead)
            {
                if (i is Weapon)
                {
                    _weapons.Add((Weapon)i);
                }
                else
                {
                    this.Inv.addItem(i);
                }
            }
        }

        public void move(Dir dir)
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
            return $"Hp: {CurrentHp}/{MaxHp}, Weapon: {getCurrentWeapon().Name}, " +
                $"Damage: {getCurrentWeapon().Damage}, Range: {getCurrentWeapon().Range}," +
                $" Position: {Pos}";
        }

        public string WeaponsToString()
        {
            string msg = "\nWeapons: ";
            foreach (Weapon weapon in _weapons)
            {
                msg += $"\n{weapon.Name}, Dmg: {weapon.Damage}, Range: {weapon.Range}";
            }
            return msg + "\n";
        }
    }
}
