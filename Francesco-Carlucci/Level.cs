using LorenzoTodisco;

namespace Francesco_Carlucci
{
    /// <summary>
    /// CLass that models a level.
    /// </summary>
    public class Level
    {
        public List<AbstractItem> Items { get; }
        public Position DoorPosition { get; set; }

        /// <summary>
        /// constructor for a level.
        /// </summary>
        public Level()
        {
            Items = new List<AbstractItem>();
            DoorPosition = new Position(0, 0);
        }

        /// <summary>
        /// Add items to the level
        /// </summary>
        /// <param name="itemsToAdd">items to add</param>
        /// <returns></returns>
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
