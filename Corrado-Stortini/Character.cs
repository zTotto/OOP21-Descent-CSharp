using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Class that model a character
    /// </summary>
    public class Character
    {
        public int XPos { get; private set; }

        public int YPos { get; private set; }

        public bool IsMoving { get; private set; }

        private int _mana;

        public int MaxMana { get; set; }

        public int Mana 
        {
            get => _mana;
            set { 
                if (value < 0)
                {
                    _mana = 0;
                }
                else if (value > MaxMana)
                {
                    _mana = MaxMana;
                }
                else
                {
                    _mana = value;
                }
            } 
        }

        public int Level { get; private set; }

        public int Speed { get; set; }

        public int InitialSpeed { get; }

        /// <summary>
        /// Increase the speed of the character by a passed value
        /// </summary>
        /// <param name="addedSpeed">Value of which the speed will be increased</param>
        /// <returns>True if the speed has been increased</returns>
        public bool IncreaseSpeed(int addedSpeed)
        {
            if (Level >= (int)LevelsToSkill.SPEEDUP)
            {
                Speed += addedSpeed;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Decreases the current mana of the character of the specified value.
        /// </summary>
        /// <param name="manaCost">Value of how much the current mana should be decreased</param>
        public void DecreaseCurrentMana(int manaCost) => Mana -= manaCost;

        /// <summary>
        /// Increases the current mana of the character of the specified value.
        /// </summary>
        /// <param name="manaCost">Value of how much the current mana should be increased</param>
        public void IncreaseCurrentMana(int manaCost) => Mana += manaCost;

        /// <summary>
        /// Increment the level of the character by one
        /// </summary>
        public void IncrementLevel() => Level++;

        /// <summary>
        /// Method that move the character based on a specified direction
        /// </summary>
        /// <param name="direction">Direction the character will follow</param>
        public void MoveTo(Direction direction)
        {
            IsMoving = true;

            ChangeY(direction);

            ChangeX(direction);

            if (direction == Direction.STILL)
            {
                IsMoving = false;
            }
        }

        private void ChangeX(Direction direction)
        {
            if (direction == Direction.RIGHT)
            {
                XPos++;
            }

            if (direction == Direction.LEFT)
            {
                XPos--;
            }
        }

        private void ChangeY(Direction direction)
        {
            if (direction == Direction.UP)
            {
                YPos++;
            }
            
            if (direction == Direction .DOWN)
            {
                YPos--;
            }
        }
    }
}
