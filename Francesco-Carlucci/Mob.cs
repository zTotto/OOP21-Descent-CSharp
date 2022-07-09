using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LorenzoTodisco;

namespace Francesco_Carlucci
{
    public class Mob : Character
    {
        public Mob(Weapon startingWeapon, int maxHp, int speed) : base(startingWeapon, maxHp, speed)
        {
        }
    }
}
