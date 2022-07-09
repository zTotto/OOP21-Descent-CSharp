using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LorenzoTodisco;
using NUnit.Framework;

namespace Francesco_Carlucci
{
    [TestFixture]
    public class Test
    {
        private readonly Character _character;

        public Test()
        {
            _character = new Character(new Weapon("Test", 10, 10, new Position(0, 0)), 100, 100);
        }

        [Test]
        public void TestManaPotion()
        {
            AbstractConsumableItem _manaPotion = new ManaPotion("Mana Potion", new Position(0, 0), (float)0.5);

            Assert.AreEqual("Mana Potion", _manaPotion.Name);
        }

    }
}
