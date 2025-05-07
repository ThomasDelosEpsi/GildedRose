namespace GildedRose
{
    public class GenericItem : Item
    {
        public GenericItem(string name, int sellIn, int quality)
            : base(name, sellIn, quality)
        { }

        public override void Update()
        {
            this.SellIn--;
            this.Quality--;
            
            if (this.SellIn < 0)
                this.Quality--;

            this.LimitQuality(0, 50);
        }
    }
}