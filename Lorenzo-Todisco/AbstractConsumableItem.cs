namespace LorenzoTodisco
{
    abstract class AbstractConsumableItem : AbstractItem
    {
        public float Modifier { get; }
        public AbstractConsumableItem(string name, Position position, float modifier) : base(name, position)
        {
            Modifier = modifier;
        }

        public abstract void Use(Character pg);
        public abstract bool CanUse(Character pg);

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                AbstractConsumableItem i = (AbstractConsumableItem)obj;
                return (Name == i.Name) && (Modifier == i.Modifier);
            }
        }
    }
}
