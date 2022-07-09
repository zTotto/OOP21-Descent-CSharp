
using LorenzoTodisco;

namespace Francesco_Carlucci
{
    public class Level
    {
        public List<AbstractItem> Items { get; }
        public Position DoorPosition { get; set; }

        public Level()
        {
            Items = new List<AbstractItem>();
            DoorPosition = new Position(0, 0);
        }

        public Level AddItems(params AbstractItem[] itemsToAdd)
        {
            foreach (AbstractItem i in itemsToAdd)
            {
                if(i.Position != null)
                {
                    Items.Add(i);
                }
            }
            return this;
        }

        public void RemoveItem(AbstractItem item) => Items.Remove(item);
    }
}
