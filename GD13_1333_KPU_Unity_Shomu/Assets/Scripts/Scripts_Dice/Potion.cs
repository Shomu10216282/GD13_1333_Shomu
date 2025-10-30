namespace GD13_1333_Shomu.Scripts
{
    internal class Potion : Item
    {
        private int v2;
        private int v3;
        private string v1;

        // Fix: Call base constructor with default values for name and description
        public Potion(string v1, int v2, int v3) : base(v1, string.Empty)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public Potion(string name, string description, int v2, int v3) : base(name, description)
        {
            this.v2 = v2;
            this.v3 = v3;
        }

        public override void Use(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}