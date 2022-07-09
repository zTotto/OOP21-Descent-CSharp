using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Class for the heal skill
    /// </summary>
    public class HealSkill : AbstractSkill
    {
        private readonly int _hp;

        /// <summary>
        /// Constructor for the skill: Heal
        /// </summary>
        /// <param name="manaCost">Cost of mana for this skill</param>
        /// <param name="hp">Hp to be recovered when healing with this skill</param>
        public HealSkill(int manaCost, int hp) : base(manaCost)
        {
            _hp = hp;
        }

        /// <summary>
        /// If the hp of the character are minor of the maxHp of the character, the character will heal itself using mana
        /// </summary>
        /// <param name="character">Character to be healed</param>
        /// <returns>True if the skill has been executed</returns>
        protected override bool ExecuteSkill(Character character) => character.Hp < character.MaxHp && character.Heal(_hp);

        /// <summary>
        /// Don't need to be used with this skill, nothing is going back to its original state
        /// </summary>
        /// <param name="character"></param>
        protected override void ResetInitialState(Character character)
        {
        }
    }
}
