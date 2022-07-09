using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CorradoStortini
{
    /// <summary>
    /// The test class for the Command pattern
    /// </summary>
    [TestFixture]
    public class Test
    {
        private readonly InputHandler _input = new InputHandler();
        private Character _character;

        /// <summary>
        /// A test method for the movement
        /// </summary>
        [Test]
        public void TestMovement()
        {
            _character = new Character(100, 100, 100); //XPos and YPos are at 0
            Assert.AreEqual(_character.XPos, 0); 
            Assert.AreEqual(_character.YPos, 0);
            
            Assert.IsFalse(_character.IsMoving);

            _input.AddCommand(KeyBinding.UP, new Movement(Direction.UP));
            ICommand action1 = _input.HandleInput(KeyBinding.UP);
            Assert.IsNotNull(action1);

            action1.ExecuteCommand(_character); //Move the character in the Up direction by 1
            Assert.AreEqual(_character.YPos, 1); //YPos = 1
            //The character is moving (It is needed when the character movement is update every time the game render)
            Assert.IsTrue(_character.IsMoving);

            _input.AddCommand(KeyBinding.LEFT, new Movement(Direction.LEFT))
                  .HandleInput(KeyBinding.LEFT)
                  .ExecuteCommand(_character);
            Assert.AreEqual(_character.XPos, -1);

            _input.AddCommand(KeyBinding.DOWN, new Movement(Direction.DOWN))
                  .AddCommand(KeyBinding.RIGHT, new Movement(Direction.RIGHT));

            Assert.IsNotNull(_input.HandleInput(KeyBinding.DOWN));
            Assert.IsNotNull(_input.HandleInput(KeyBinding.RIGHT));

            _input.HandleInput(KeyBinding.DOWN).ExecuteCommand(_character);
            Assert.AreEqual(_character.YPos, 0);

            _input.HandleInput(KeyBinding.DOWN).ExecuteCommand(_character);
            Assert.AreEqual(_character.YPos, -1);

            _input.HandleInput(KeyBinding.RIGHT).ExecuteCommand(_character);
            Assert.AreEqual(_character.XPos, 0);

            _input.HandleInput(KeyBinding.RIGHT).ExecuteCommand(_character);
            Assert.AreEqual(_character.XPos, 1);
        }

        /// <summary>
        /// A test method for the skills
        /// </summary>
        [Test]
        public void TestSkills()
        {
            _character = new Character(100, 100, 100); //Mana = 100, Hp = 100, Speed = 100, Level = 1

            Assert.AreEqual(_character.MaxMana, 100);
            Assert.AreEqual(_character.MaxHp, 100);
            Assert.AreEqual(_character.InitialSpeed, 100);

            Assert.AreEqual(_character.Mana, 100);
            Assert.AreEqual(_character.Hp, 100);
            Assert.AreEqual(_character.Speed, 100);

            Assert.AreEqual(_character.Level, 1);

            _input.AddCommand(KeyBinding.INCREASES_SPEED, new SpeedUpSkill(10, 10));
            _input.AddCommand(KeyBinding.HEAL, new HealSkill(20, 10));

            //SpeedUp skill

            //10 will be added to the speed of the character until the mana is enough.
            //Normally the speed wouldn't increase if the user does not press the key of the skill, but that's something 
            //that cannot be done because there is not a render method
            //If the speed is increased, the next call of the executeCommand will turn back the speed to the initialSpeed
            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(_character);

            //Level of the character is 1 so it cannot use the skill
            Assert.AreEqual(_character.Mana, 100);
            Assert.AreEqual(_character.Speed, 100);

            _character.IncrementLevel(); //Now the character is at level 2 and can use the skill IncreasesSpeed
            Assert.AreEqual(_character.Level, 2);

            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(_character);
            Assert.AreEqual(_character.Mana, 90);
            Assert.AreEqual(_character.Speed, 110);

            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(_character);
            Assert.AreEqual(_character.Mana, 90);
            Assert.AreEqual(_character.Speed, 100);

            for (int i = 0; i < 18; i++)
            {
                _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(_character);
            }

            Assert.AreEqual(_character.Mana, 0);
            Assert.AreEqual(_character.Speed, 100);

            //The mana is at 0 so the character won't speed up anymore
            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(_character);
            Assert.AreEqual(_character.Mana, 0);
            Assert.AreEqual(_character.Speed, 100);

            //Heal skill
            _character.Mana = _character.MaxMana;
            _character.Hp = 60;

            //The character is at level 2 so it cannot use the Heal skill
            _input.HandleInput(KeyBinding.HEAL).ExecuteCommand(_character);
            Assert.AreEqual(_character.Mana, _character.MaxMana);
            Assert.AreEqual(_character.Hp, 60);

            _character.IncrementLevel(); //Level = 3
            _character.IncrementLevel(); //Level = 4
            Assert.AreEqual(_character.Level, 4);

            //The skill uses 20 mana to heal 10 hp
            _input.HandleInput(KeyBinding.HEAL).ExecuteCommand(_character);
            Assert.AreEqual(_character.Mana, 80); //Mana = 100 - 20
            Assert.AreEqual(_character.Hp, 70); //Hp = 60 + 10

            //Try to use the skill 4 times (Hp are at 70, so the character will have 100 hp at 3rd time)
            for(int i = 0; i < 4; i++)
            {
                _input.HandleInput(KeyBinding.HEAL).ExecuteCommand(_character);
            }

            //With 100 hp, the skill won't be used again
            Assert.AreEqual(_character.Mana, 20);
            Assert.AreEqual(_character.Hp, 100);

            //With 0 mana, the skill won't be used again
            _character.Mana = 0;
            _character.Hp = 60;

            _input.HandleInput(KeyBinding.HEAL);
            Assert.AreEqual(_character.Mana, 0);
            Assert.AreEqual(_character.Hp, 60);
        }

        

        /// <summary>
        /// Test method for general commands
        /// </summary>
        [Test]
        public void TestGeneralCommands()
        {
            _input.AddCommand(KeyBinding.USE_HEALTH_POTION, new UseHealthPotion())
                  .AddCommand(KeyBinding.USE_MANA_POTION, new UseManaPotion());

            _character = new Character(100, 100, 100);
            _character.NumHealthPotion = 4;
            _character.NumManaPotion = 4;

            Assert.AreEqual(_character.NumHealthPotion, 4);
            Assert.AreEqual(_character.NumManaPotion, 4);


            //Health potion

            _input.HandleInput(KeyBinding.USE_HEALTH_POTION).ExecuteCommand(_character);
            Assert.AreEqual(_character.NumHealthPotion, 3);

            _input.HandleInput(KeyBinding.USE_HEALTH_POTION).ExecuteCommand(_character);
            _input.HandleInput(KeyBinding.USE_HEALTH_POTION).ExecuteCommand(_character);
            _input.HandleInput(KeyBinding.USE_HEALTH_POTION).ExecuteCommand(_character);
            Assert.AreEqual(_character.NumHealthPotion, 0);

            _input.HandleInput(KeyBinding.USE_HEALTH_POTION).ExecuteCommand(_character);
            Assert.AreEqual(_character.NumHealthPotion, 0);

            //Mana Potion

            _input.HandleInput(KeyBinding.USE_MANA_POTION).ExecuteCommand(_character);
            Assert.AreEqual(_character.NumManaPotion, 3);

            _input.HandleInput(KeyBinding.USE_MANA_POTION).ExecuteCommand(_character);
            _input.HandleInput(KeyBinding.USE_MANA_POTION).ExecuteCommand(_character);
            _input.HandleInput(KeyBinding.USE_MANA_POTION).ExecuteCommand(_character);
            Assert.AreEqual(_character.NumManaPotion, 0);

            _input.HandleInput(KeyBinding.USE_MANA_POTION).ExecuteCommand(_character);
            Assert.AreEqual(_character.NumManaPotion, 0);


        }

        private class UseHealthPotion : ICommand
        {
            public void ExecuteCommand(Character character)
            {
                character.UseHealthPotion();
            }
        }

        private class UseManaPotion : ICommand
        {
            public void ExecuteCommand(Character character)
            {
                character.UseManaPotion();
            }
        }
    }

    
}
