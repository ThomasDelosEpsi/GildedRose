namespace GildedRose
{
    public class ConjuredItem : Item
    {
        public ConjuredItem(string name, int sellIn, int quality)
            : base(name, sellIn, quality)
        { }

        public override void Update()
        {
            this.SellIn--;
            this.Quality -= 2;
            
            if (this.SellIn < 0)
                this.Quality -= 2;

            this.LimitQuality(0, 50);
        }
    }
}