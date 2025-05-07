namespace GildedRose
{
    public class EventItem : Item
    {
        public EventItem(string name, int sellIn, int quality)
            : base(name, sellIn, quality)
        { }

        public override void Update()
        {
            this.SellIn--;
            this.Quality++;
            
            if (this.SellIn <= 10)
                this.Quality++;
            if (this.SellIn <= 5)
                this.Quality++;
            if (this.SellIn < 0)
                this.Quality = 0;

            this.LimitQuality(0, 50);
        }
    }
}