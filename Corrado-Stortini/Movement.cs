using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Class used to move a character based on the specified direction
    /// </summary>
    public class Movement : ICommand
    {
        private readonly Direction _direction;

        /// <summary>
        /// Constructor for the Movement class
        /// </summary>
        /// <param name="direction">Direction that a character must follow when moving through this class</param>
        public Movement(Direction direction)
        {
            _direction = direction;
        }

        /// <inheritdoc />
        public void ExecuteCommand(Character character) => character.MoveTo(_direction);
    }
}
