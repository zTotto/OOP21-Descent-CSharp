using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LorenzoTodisco;

namespace Francesco_Carlucci
{
    public interface Level
    {
        public Level AddItems();

        public Level AddEnemies();

        public void SetDoorPosition();

        public void RemoveItem(AbstractItem item);

        public void RemoveMob();
    }
}
