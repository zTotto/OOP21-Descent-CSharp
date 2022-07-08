using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Functional interface of a Command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute a command on a specific character.
        /// </summary>
        /// <param name="character">the character on which the command will be executed</param>
        void ExecuteCommand(Character character);
    }
}
