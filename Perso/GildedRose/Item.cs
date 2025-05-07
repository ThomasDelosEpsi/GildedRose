namespace GildedRose
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public int SellIn { get; protected set; }
        public int Quality { get; protected set; }

        public Item(string name, int sellIn, int quality)
        {
            this.Name = name;
            this.SellIn = sellIn;
            this.Quality = quality;
        }

        public abstract void Update();

        protected void LimitQuality(int min, int max)
        {
            if (this.Quality < min)
                this.Quality = min;
            if (this.Quality > max)
                this.Quality = max;
        }
    }
}