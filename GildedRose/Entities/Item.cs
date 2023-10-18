namespace GildedRose.Entities
{
    public class Item
    {
        private const int DefaultMaxQuality = 50;
        public Item()
        {
            MaxQuality = DefaultMaxQuality;
        }

        public Item(int maxQuality)
        {
            MaxQuality = maxQuality;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int SellIn { get; set; }

        private int quality;
        public int Quality
        {
            get => quality;

            set
            {
                if (value < 0)
                {
                    throw new QualityBelowZeroException();
                }

                if (value > MaxQuality)
                {
                    throw new OverQualityException();
                }

                this.quality = value;
            }
        }
        public int MaxQuality { get; set; }

        // Item properties
        public bool IsNormal { get; set; }
        public bool IncreasesInQuality { get; set; }
        public bool HasConstantQuality { get; set; }

        public override string ToString()
        {
            return $"{this.Code}, {this.Name}, {this.SellIn}, {this.Quality}";
        }  
    }
}
