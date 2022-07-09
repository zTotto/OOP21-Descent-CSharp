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
        private Character character;

        /// <summary>
        /// A test method for the movement
        /// </summary>
        [Test]
        public void TestMovement()
        {
            character = new Character(100, 100, 100); //XPos and YPos are at 0
            Assert.AreEqual(character.XPos, 0); 
            Assert.AreEqual(character.YPos, 0);
            
            Assert.IsFalse(character.IsMoving);

            _input.AddCommand(KeyBinding.UP, new Movement(Direction.UP));
            ICommand action1 = _input.HandleInput(KeyBinding.UP);
            Assert.IsNotNull(action1);

            action1.ExecuteCommand(character); //Move the character in the Up direction by 1
            Assert.AreEqual(character.YPos, 1); //YPos = 1
            //The character is moving (It is needed when the character movement is update every time the game render)
            Assert.IsTrue(character.IsMoving);

            _input.AddCommand(KeyBinding.LEFT, new Movement(Direction.LEFT))
                  .HandleInput(KeyBinding.LEFT)
                  .ExecuteCommand(character);
            Assert.AreEqual(character.XPos, -1);

            _input.AddCommand(KeyBinding.DOWN, new Movement(Direction.DOWN))
                  .AddCommand(KeyBinding.RIGHT, new Movement(Direction.RIGHT));

            Assert.IsNotNull(_input.HandleInput(KeyBinding.DOWN));
            Assert.IsNotNull(_input.HandleInput(KeyBinding.RIGHT));

            _input.HandleInput(KeyBinding.DOWN).ExecuteCommand(character);
            Assert.AreEqual(character.YPos, 0);

            _input.HandleInput(KeyBinding.DOWN).ExecuteCommand(character);
            Assert.AreEqual(character.YPos, -1);

            _input.HandleInput(KeyBinding.RIGHT).ExecuteCommand(character);
            Assert.AreEqual(character.XPos, 0);

            _input.HandleInput(KeyBinding.RIGHT).ExecuteCommand(character);
            Assert.AreEqual(character.XPos, 1);
        }

        /// <summary>
        /// A test method for the skills
        /// </summary>
        [Test]
        public void TestSkills()
        {
            character = new Character(100, 100, 100); //Mana = 100, Hp = 100, Speed = 100, Level = 1

            Assert.AreEqual(character.MaxMana, 100);
            Assert.AreEqual(character.MaxHp, 100);
            Assert.AreEqual(character.InitialSpeed, 100);

            Assert.AreEqual(character.Mana, 100);
            Assert.AreEqual(character.Hp, 100);
            Assert.AreEqual(character.Speed, 100);

            Assert.AreEqual(character.Level, 1);

            _input.AddCommand(KeyBinding.INCREASES_SPEED, new SpeedUpSkill(10, 10));
            _input.AddCommand(KeyBinding.HEAL, new HealSkill(10, 20));

            //SpeedUp skill

            //10 will be added to the speed of the character until the mana is enough.
            //Normally the speed wouldn't increase if the user does not press the key of the skill, but that's something 
            //that cannot be done because there is not a render method
            //If the speed is increased, the next call of the executeCommand will turn back the speed to the initialSpeed
            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(character);

            //Level of the character is 1 so it cannot use the skill
            Assert.AreEqual(character.Mana, 100);
            Assert.AreEqual(character.Speed, 100);

            character.IncrementLevel(); //Now the character is at level 2 and can use the skill IncreasesSpeed
            Assert.AreEqual(character.Level, 2);

            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(character);
            Assert.AreEqual(character.Mana, 90);
            Assert.AreEqual(character.Speed, 110);

            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(character);
            Assert.AreEqual(character.Mana, 90);
            Assert.AreEqual(character.Speed, 100);

            for (int i = 0; i < 18; i++)
            {
                _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(character);
            }

            Assert.AreEqual(character.Mana, 0);
            Assert.AreEqual(character.Speed, 100);

            //The mana is at 0 so the character won't speed up anymore
            _input.HandleInput(KeyBinding.INCREASES_SPEED).ExecuteCommand(character);
            Assert.AreEqual(character.Mana, 0);
            Assert.AreEqual(character.Speed, 100);

            //Heal skill
            character.Mana = character.MaxMana;
            character.Hp = 50;

            //The character is at level 2 so it cannot use the Heal skill
            _input.HandleInput(KeyBinding.HEAL).ExecuteCommand(character);
            Assert.AreEqual(character.Mana, character.MaxMana);
            Assert.AreEqual(character.Hp, 50);


        }
    }
}
