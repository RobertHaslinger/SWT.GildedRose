using System.Collections.Generic;

namespace SWT.GildedRose
{
	public class GildedRose
	{
		IList<Item> Items;
		public GildedRose(IList<Item> Items) 
		{
			this.Items = Items;
		}

        private void UpdateNormal(Item normalItem)
        {
            normalItem.Quality -= normalItem.SellIn <= 0 ? 2 : 1;

            if (normalItem.Quality < 0)
            {
                normalItem.Quality = 0;
            }
        }

        private void UpdateAgedBrie(Item agedBrie)
        {
            if (agedBrie.Quality < 50)
            {
                agedBrie.Quality++;
            }
        }

        private void UpdateConjured(Item conjured)
        {
			UpdateNormal(conjured);
			UpdateNormal(conjured);
        }

        private void UpdateBackstagePasses(Item pass)
        {
            if (pass.SellIn < 0)
            {
                pass.Quality = 0;
				return;
            }

            pass.Quality += 1;
            if (pass.SellIn < 11)
            {
                pass.Quality += 1;
            }

            if (pass.SellIn < 6)
            {
                pass.Quality += 1;
            }

            if (pass.Quality > 50)
            {
                pass.Quality = 50;
            }
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                switch (item.Name)
                {
                    case { } name when name.ToLower().Contains("sulfuras"):
                    {
                        continue;
                    }
                    case { } name when name.ToLower().Contains("aged brie"):
                    {
						UpdateAgedBrie(item);
                        break;
                    }
                    case { } name when name.ToLower().Contains("conjured"):
                    {
						UpdateConjured(item);
                        break;
                    }
                    case { } name when name.ToLower().Contains("backstage pass"):
                    {
						UpdateBackstagePasses(item);
                        break;
                    }
                    default:
                    {
						UpdateNormal(item);
						break;
                    }
				}

                item.SellIn--;
            }
        }
		
		/// <summary>
		/// DONT USE THIS
		/// </summary>
		public void UpdateQualityOld()
		{
			for (var i = 0; i < Items.Count; i++)
			{
				if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
				{
					if (Items[i].Quality > 0)
					{
						if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
						{
							Items[i].Quality = Items[i].Quality - 1;
						}
					}
				}
				else
				{
					if (Items[i].Quality < 50)
					{
						Items[i].Quality = Items[i].Quality + 1;
						
						if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
						{
							if (Items[i].SellIn < 11)
							{
								if (Items[i].Quality < 50)
								{
									Items[i].Quality = Items[i].Quality + 1;
								}
							}
							
							if (Items[i].SellIn < 6)
							{
								if (Items[i].Quality < 50)
								{
									Items[i].Quality = Items[i].Quality + 1;
								}
							}
						}
					}
				}
				
				if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
				{
					Items[i].SellIn = Items[i].SellIn - 1;
				}
				
				if (Items[i].SellIn < 0)
				{
					if (Items[i].Name != "Aged Brie")
					{
						if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
						{
							if (Items[i].Quality > 0)
							{
								if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
								{
									Items[i].Quality = Items[i].Quality - 1;
								}
							}
						}
						else
						{
							Items[i].Quality = Items[i].Quality - Items[i].Quality;
						}
					}
					else
					{
						if (Items[i].Quality < 50)
						{
							Items[i].Quality = Items[i].Quality + 1;
						}
					}
				}
			}
		}
		
	}
	
	public class Item
	{
		public string Name { get; set; }
		
		public int SellIn { get; set; }
		
		public int Quality { get; set; }
	}
	
}
