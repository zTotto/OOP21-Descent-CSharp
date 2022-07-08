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

        internal int GetCurrentMana()
        {
            throw new NotImplementedException();
        }

        internal void DecreaseCurrentMana(int manaCost)
        {
            throw new NotImplementedException();
        }

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
