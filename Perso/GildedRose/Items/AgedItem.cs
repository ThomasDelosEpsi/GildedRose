namespace GildedRose
{
    public class AgedItem : Item
    {
        public AgedItem(string name, int sellIn, int quality)
            : base(name, sellIn, quality)
        { }

        public override void Update()
        {
            this.SellIn--;
            this.Quality++;
            this.LimitQuality(0, 50);
        }
    }
}