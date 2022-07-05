using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    public class Character
    {
        public int XPos { get; private set; }

        public int YPos { get; private set; }

        public bool IsMoving { get; private set; }


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
