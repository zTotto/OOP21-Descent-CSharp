namespace OOP_Project
{
    internal class Character
    {
        private Inventory _inv = new Inventory();
        private List<Weapon> _weapons = new List<Weapon>();
        private int _weaponIndex = 0;
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int Speed { get; set; }
        public Position Pos { get; set; }
        public Inventory Inv { get => _inv; }
        public List<Weapon> Weapons { get => _weapons; }

        public Character(Weapon startingWeapon, int maxHp, int currentHp, int speed)
        {
            MaxHp = maxHp;
            CurrentHp = currentHp;
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
                Math.Abs(enemy.Pos.Y - this.Pos.Y) <= getCurrentWeapon().Range;
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
            int range = Speed / 6;
            if (Math.Abs(i.Position.X - this.Pos.X) < range && 
                Math.Abs(i.Position.Y - this.Pos.Y) < range)
            {
                this.Inv.addItem(i);
            }
        }
    }
}
