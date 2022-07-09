using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Interface of a Command.
    /// </summary>
    public interface ICommand
    {
        void ExecuteCommand(Character character);
    }
}
