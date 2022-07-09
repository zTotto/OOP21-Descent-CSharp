using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LorenzoTodisco;
using NUnit.Framework;

namespace Francesco_Carlucci
{
    /// <summary>
    /// Test class for some classes
    /// </summary>
    [TestFixture]
    public class Test
    {
        private readonly Character _character;

        /// <summary>
        /// Initialize some objects.
        /// </summary>
        public Test()
        {
            _character = new Character(new Weapon("Test", 10, 10, new Position(0, 0)), 100, 100);
            _character.MaxMana = 100;
        }

        /// <summary>
        /// Test a mana potion.
        /// </summary>
        [Test]
        public void TestManaPotion()
        {
            AbstractConsumableItem _manaPotion = new ManaPotion("Mana Potion", new Position(0, 0), (float)0.5);

            Assert.AreEqual("Mana Potion", _manaPotion.Name);
            Assert.AreEqual(0.5, _manaPotion.Modifier);
            Assert.AreEqual(new Position(0, 0), _manaPotion.Position);

            _character.Inv.AddItem(_manaPotion);
            Assert.True(_character.Inv.Contains(_manaPotion));
            Assert.AreEqual(100, _character.MaxMana);

            Assert.True(_manaPotion.CanUse(_character));
            _character.UseManaPotion();
            Assert.AreEqual(150, _character.MaxMana);
            Assert.False(_character.Inv.Contains(_manaPotion));
        }

        /// <summary>
        /// Test a WearableItem.
        /// </summary>
        [Test]
        public void TestWearableItem()
        {
            WearableItem _wearable = new WearableItem.Builder("Wearable", new Position(0, 0))
                                                .Health(0.5)
                                                .Build();

            Assert.AreEqual("Wearable", _wearable.Name);
            Assert.AreEqual(new Position(0, 0), _wearable.Position);

            _wearable.SetPos(new Position(50, 100));
            Assert.AreEqual(new Position(50, 100), _wearable.Position);

            Assert.AreEqual(100, _character.MaxHp);
            _wearable.Wear(_character);
            Assert.AreEqual(150, _character.MaxHp);

            _wearable = new WearableItem.Builder("Wearable", new Position(0, 0))
                                                .Power(0.5)
                                                .Exp(50)
                                                .Build();

            int _damage = _character.GetCurrentWeapon().Damage;
            _wearable.Wear(_character);
            Assert.True(_damage < _character.GetCurrentWeapon().Damage);
            Assert.AreEqual(50, _character.Exp);
        }

        /// <summary>
        /// Test a WearableItemBuilder.
        /// </summary>
        [Test]
        public void TestWearableItemBuilder()
        {
            Assert.Throws<NotSupportedException>(() => new WearableItem.Builder("Wearable", new Position(0, 0)).Build());
            Assert.Throws<NotSupportedException>(() => new WearableItem.Builder("Wearable", new Position(0, 0))
                                                                       .Health(0)
                                                                       .Power(0)
                                                                       .Exp(0)
                                                                       .Build());
            Assert.Throws<NotSupportedException>(() => new WearableItem.Builder("Wearable", new Position(0, 0))
                                                                       .Exp(10000)
                                                                       .Build());
            Assert.Throws<NotSupportedException>(() => new WearableItem.Builder("Wearable", new Position(0, 0))
                                                                       .Power(5)
                                                                       .Build());
        }

        /// <summary>
        /// Test a levels list.
        /// </summary>
        [Test]
        public void TestLevelsList()
        {
            List<Level> levels = new List<Level>();
            levels.Add(new Level());
            levels.Add(new Level());
            LevelsList l = new LevelsList(levels);


            Assert.AreEqual(l.GetCurrentLevel(), l.Levels[0]);
            Assert.True(l.HasNextLevel());
            Assert.AreEqual(2, l.Levels.Count());
            Assert.False(l.IsGameOver());

            Assert.AreEqual(l.GetNextLevel(), l.Levels[1]);
            Assert.False(l.HasNextLevel());
            Assert.AreEqual(2, l.Levels.Count());
            Assert.AreEqual(l.GetCurrentLevel(), l.Levels[1]);

            Assert.Throws<NotSupportedException>(() => l.GetNextLevel());

            Assert.True(l.IsGameOver());
            Assert.False(l.Levels == null);
        }
    }
}
