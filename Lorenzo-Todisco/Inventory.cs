namespace OOP_Project
{
    class Inventory
    {
        private List<Pair<AbstractItem, int>> _inv = new List<Pair<AbstractItem, int>>();

        public List<Pair<AbstractItem, int>> Inv { get => _inv; }

        public Inventory() { }

        public Inventory(List<Pair<AbstractItem, int>> inv)
        {
            _inv = inv;
        }

        public bool Contains(AbstractItem i)
        {
            foreach (Pair<AbstractItem, int> t in _inv)
            {
                if (t.First.Name.Equals(i.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private void editQuantity(AbstractItem i, int val)
        {
            foreach (Pair<AbstractItem, int> t in _inv)
            {
                if (t.First.Name.Equals(i.Name))
                {
                    t.Second += val;
                    if (t.Second < 1)
                    {
                        _inv.Remove(t);
                    }
                    return;
                }
            }
        }

        public void addItem(AbstractItem i)
        {
            if (this.Contains(i))
            {
                this.editQuantity(i, 1);
            }
            else
            {
                _inv.Add(new Pair<AbstractItem, int>(i, 1));
            }
        }

        public void removeItem(AbstractItem i)
        {
            if (this.Contains(i))
            {
                this.editQuantity(i, -1);
            }
        }

        public override string ToString()
        {
            string msg = "\nInventory: \n[";
            foreach (Pair<AbstractItem, int> t in _inv)
            {
                msg += t.ToString() + "\n";
            }
            return msg + "]";
        }
    }
}
