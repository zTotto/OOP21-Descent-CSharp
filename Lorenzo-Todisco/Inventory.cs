namespace LorenzoTodisco
{
    class Inventory
    {
        public List<Pair<AbstractItem, int>> Inv { get; } = new List<Pair<AbstractItem, int>>();

        public Inventory() { }

        public Inventory(List<Pair<AbstractItem, int>> inv)
        {
            Inv = inv;
        }

        public bool Contains(AbstractItem i)
        {
            foreach (Pair<AbstractItem, int> t in Inv)
            {
                if (t.First.Name.Equals(i.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private void EditQuantity(AbstractItem i, int val)
        {
            foreach (Pair<AbstractItem, int> t in Inv)
            {
                if (t.First.Name.Equals(i.Name))
                {
                    t.Second += val;
                    if (t.Second < 1)
                    {
                        Inv.Remove(t);
                    }
                    return;
                }
            }
        }

        public void AddItem(AbstractItem i)
        {
            if (this.Contains(i))
            {
                this.EditQuantity(i, 1);
            }
            else
            {
                Inv.Add(new Pair<AbstractItem, int>(i, 1));
            }
        }

        public void RemoveItem(AbstractItem i)
        {
            if (this.Contains(i))
            {
                this.EditQuantity(i, -1);
            }
        }

        public override string ToString()
        {
            string msg = "\nInventory: \n[";
            foreach (Pair<AbstractItem, int> t in Inv)
            {
                msg += t.ToString() + "\n";
            }
            return msg + "]";
        }
    }
}
