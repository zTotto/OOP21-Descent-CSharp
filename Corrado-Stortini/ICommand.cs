using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    public interface ICommand
    {
        void ExecuteCommand(Character character);
    }
}
