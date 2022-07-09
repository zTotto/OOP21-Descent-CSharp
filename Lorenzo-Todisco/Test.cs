using NUnit.Framework;

namespace LorenzoTodisco
{
    [TestFixture]
    public class Test
    {
        Weapon weapon = new Weapon("Greatsword", 20, 4, new Position(2, 0));

        [Test]
        public void TestMovement()
        {
            Character Hero = new Character(new Weapon("Longsword", 23, 4, new Position(0, 0)), 100, 1);
            for (int i = 0; i < 10; i++)
            {
                Hero.Move(Dir.Right);
            }
            Assert.AreEqual(Hero.Pos.X, 10);
        }

        [Test]
        public void TestItemPickUp()
        {
            Character Hero = new Character(new Weapon("Longsword", 23, 4, new Position(0, 0)), 100, 1);
            HealthPotion potion = new HealthPotion("Pozione", new Position(0, 0), 0.25f);
            Hero.Move(Dir.Right);
            Hero.Move(Dir.Right);
            Hero.PickUpItem(potion);
            Assert.IsFalse(Hero.Inv.Contains(potion));
            Hero.Move(Dir.Left);
            Hero.Move(Dir.Left);
            Hero.PickUpItem(potion);
            Assert.IsTrue(Hero.Inv.Contains(potion));
        }

        [Test]
        public void TestDamage()
        {
            Character Hero = new Character(new Weapon("Longsword", 23, 4, new Position(0, 0)), 100, 1);
            Character Mob = new Character(new Weapon("Fists", 10, 2, new Position(0, 0)), 100, 1);
            Hero.HitEnemy(Mob);
            Assert.AreEqual(Mob.CurrentHp, Mob.MaxHp - Hero.GetCurrentWeapon().Damage);
        }
    }
}
