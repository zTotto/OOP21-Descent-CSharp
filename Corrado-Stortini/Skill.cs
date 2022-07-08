using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Abstract class to reppresent a generic skill
    /// </summary>
    public abstract class AbstractSkill : ICommand
    {
        public int ManaCost { get; }

        /// <summary>
        /// Constructor for a generic skill.
        /// </summary>
        /// <param name="manaCost">value of how much mana will cost the skill every time it will be executed</param>
        public AbstractSkill(int manaCost)
        {
            ManaCost = manaCost;
        }

        /// <inheritdoc/>
        public void ExecuteCommand(Character character)
        {
            if(character.GetCurrentMana() >= ManaCost && ExecuteSkill(character))
            {
                character.DecreaseCurrentMana(ManaCost);
            }
            else
            {
                ResetInitialState(character);
            }
        }

        /// <summary>
        /// Uses the skill on the specified character.
        /// </summary>
        /// <param name="character">The character the skill will be used on.</param>
        /// <returns>True if the skill has been executed</returns>
        protected abstract bool ExecuteSkill(Character character);

        /// <summary>
        /// Undoes what the skill/spell did to a character.
        /// Is suggested to implement just if the skill is an Hold skill
        /// </summary>
        /// <param name="character">Target of the reset</param>
        protected abstract void ResetInitialState(Character character);
    }
}
