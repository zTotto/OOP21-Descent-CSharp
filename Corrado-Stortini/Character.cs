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

        /// <summary>
        /// Method that move the character based on a specified direction
        /// </summary>
        /// <param name="direction">Direction the character will follow</param>
        public void MoveTo(Direction direction)
        {
            IsMoving = true;

            if (direction == Direction.UP)
            {
                YPos++;
            }

            if (direction == Direction.DOWN)
            {
                YPos--;
            }

            if (direction == Direction.RIGHT)
            {
                XPos++;
            }

            if (direction == Direction.LEFT)
            {
                XPos--;
            }

            if (direction == Direction.STILL)
            {
                IsMoving = false;
            }
        }
    }
}
