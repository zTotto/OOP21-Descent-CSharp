using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    public class Movement : ICommand
    {
        private readonly Direction _direction;

        public Movement(Direction direction)
        {
            _direction = direction;
        }

        public void ExecuteCommand(Character character)
        {
            character.MoveTo(_direction);
        }
    }
}
