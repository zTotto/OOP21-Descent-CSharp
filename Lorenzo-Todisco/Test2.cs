namespace LorenzoTodisco
{
    class Test2
    {
        static void Main(string[] args)
        {
            Weapon i1 = new Weapon("Longsword", 23, 4, new Position(0, 0));
            Weapon i2 = new Weapon("Greatsword", 20, 4, new Position(2, 0));
            Weapon i3 = new Weapon("Axe", 20, 4, new Position(0, 0));
            HealthPotion p = new HealthPotion("Pozione", new Position(0, 0), 0.25f);
            Character Ross = new Character(i1, 100, 1);
            Character Mob = new Character(new Weapon("Fists", 10, 2, new Position(0, 0)), 100, 1);
            Console.WriteLine("\n\n" + i3.GetType().Name + "\n\n");
            Console.WriteLine("Ross: " + Ross);
            Console.WriteLine("Mob: " + Mob);
            Console.WriteLine("\nRoss Inventory: " + Ross.Inv + "\n");
            Ross.PickUpItem(p);
            Ross.PickUpItem(p);
            Console.WriteLine("\nRoss Inventory: " + Ross.Inv + "\n");
            Ross.HitEnemy(Mob);
            Mob.HitEnemy(Ross);
            Console.WriteLine("Ross: " + Ross);
            Console.WriteLine("Mob: " + Mob);
            Ross.HitEnemy(Mob);
            Mob.HitEnemy(Ross);
            Console.WriteLine("Ross: " + Ross);
            Console.WriteLine("Mob: " + Mob);
            Ross.HitEnemy(Mob);
            Mob.HitEnemy(Ross);
            Console.WriteLine("Ross: " + Ross);
            Console.WriteLine("Mob: " + Mob);
            Ross.HitEnemy(Mob);
            Mob.HitEnemy(Ross);
            Console.WriteLine("Ross: " + Ross);
            Console.WriteLine("Mob: " + Mob);
            Console.WriteLine("\n\n\n");
            Ross.UseHealthPotion();
            Ross.UseHealthPotion();
            Ross.UseHealthPotion();
            Console.WriteLine("Ross: " + Ross);
            Console.WriteLine("\nRoss Inventory: " + Ross.Inv + "\n");
            Ross.PickUpItem(p);
            Console.WriteLine("\nRoss Inventory: " + Ross.Inv + "\n");
            Console.WriteLine(Ross.Inv.Contains(p));
        }
    }
}
