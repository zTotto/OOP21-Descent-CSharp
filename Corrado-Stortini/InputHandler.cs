using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Class that handles the input, in order to give a ICommand object, referred to
    /// the action the player want to execute(based on what he pressed).
    /// </summary>
    public class InputHandler
    {
        private readonly IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        /// <summary>
        /// Add a new command
        /// </summary>
        /// <param name="key">The KeyBinding for the new command</param>
        /// <param name="command">The new command to add</param>
        /// <returns>The input handler, allowing the method to be reused</returns>
        public InputHandler AddCommand(KeyBinding key, ICommand command)
        {
            _commands.Add(key.GetName(), command);
            return this;
        }

        /// <summary>
        /// Return what command to use based on the pressed key.
        /// </summary>
        /// <param name="key">The action the user wants to perform</param>
        /// <returns>The command related to the action the user wants to perform</returns>
        public ICommand HandleInput(KeyBinding action) => _commands[action.GetName()];
    }
}
