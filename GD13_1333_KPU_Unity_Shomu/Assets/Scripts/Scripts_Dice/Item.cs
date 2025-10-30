namespace GD13_1333_Shomu.Scripts
{
    internal abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Use(Player player);
        public virtual bool IsConsumable() => true;
    }
}
