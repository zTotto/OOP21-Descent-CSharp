using System;
using System.Collections.Generic;
using System.Text;

namespace CorradoStortini
{
    /// <summary>
    /// Class of the skill SpeedUp
    /// </summary>
    public class SpeedUpSkill : AbstractSkill
    {
        private readonly int _initialSpeed;
        private readonly int _addedSpeed;

        /// <summary>
        /// Constructor of the skill SpeedUpSkill. The initialSpeed and the addedSpeed 
        /// will be used in the ExecuteSkill and ResetInitialState methods
        /// </summary>
        /// <param name="manaCost">Mana cost of the skill</param>
        /// <param name="initialSpeed">Initial speed of the character</param>
        /// <param name="addedSpeed">Speed to add to the character</param>
        public SpeedUpSkill(int manaCost, int initialSpeed, int addedSpeed) : base(manaCost)
        {
            _initialSpeed = initialSpeed;
            _addedSpeed = addedSpeed;
        }

        /// <summary>
        /// Add the addedSpeed setted in the constructor to the speed of the passed character
        /// </summary>
        /// <param name="character">Character to which add the speed</param>
        /// <returns>True if the skill has been executed</returns>
        protected override bool ExecuteSkill(Character character)
        {
            return character.Speed < _addedSpeed + _initialSpeed && character.IncreaseSpeed(_addedSpeed);
        }

        /// <summary>
        /// Reset the initial speed of the character when the skill finishes
        /// </summary>
        /// <param name="character">Target of the speed reset</param>
        protected override void ResetInitialState(Character character) => character.Speed = _initialSpeed;
    }
}
