using System;
using NUnit.Framework;
using System.Collections.Generic;
using SWT.GildedRose;

namespace SWT.GildedRose.Test
{
	[TestFixture()]
	public class GildedRoseTest
	{
        [Test]
        public void Test_QualityDegradesWhenUpdateQuality()
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 5, Quality = 20}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            Assert.AreEqual(19, items[0].Quality);
        }

        [Test]
        public void Test_QualityDegradesTwiceAsFastWhenDatePassed()
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = -1, Quality = 20}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            Assert.AreEqual(18, items[0].Quality);
        }

        [Test]
        public void Test_QualityNeverNegative()
        {
            IList<Item> items = new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 1, Quality = 4},
                new Item {Name = "Elixir of the Mongoose", SellIn = 1, Quality = 5},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 0,
                    Quality = 20
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 1, Quality = 6}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.GreaterOrEqual(item.Quality, 0);
            }
        }

        [Test]
        public void Test_AgedBrieIncreasesQuality()
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 8, Quality = 10}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            Assert.AreEqual(11, items[0].Quality);
        }

        [Test]
        public void Test_QualityNeverOver50()
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 8, Quality = 50}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            Assert.AreEqual(50, items[0].Quality);
        }

        [Test]
        public void Test_QualityNeverOver50ExceptSulfras()
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 8, Quality = 49},
                new Item
                {
                    Name = "Backstage passes to a Metallica concert",
                    SellIn = 3,
                    Quality = 45
                },
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 49},
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.LessOrEqual(item.Quality, 50);
            }
        }

        [Test]
        public void Test_SulfrasNeverAlters()
        {
            IList<Item> items = new List<Item>
            {
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();

            foreach (Item item in items)
            {
                Assert.AreEqual(80, item.Quality);
            }
            Assert.AreEqual(10, items[0].SellIn);
            Assert.AreEqual(-1, items[1].SellIn);
        }

        [Test]
        public void Test_BackstagePassesSellInMoreThan10()
        {
            IList<Item> items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a Metallica concert",
                    SellIn = 13,
                    Quality = 10
                }
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();

            Assert.AreEqual(11, items[0].Quality);
        }

        [Test]
        public void Test_BackstagePassesSellInMoreThan5()
        {
            IList<Item> items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a Metallica concert",
                    SellIn = 8,
                    Quality = 20
                }
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();

            Assert.AreEqual(22, items[0].Quality);
        }

        [Test]
        public void Test_BackstagePassesSellInMoreThan0()
        {
            IList<Item> items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a Metallica concert",
                    SellIn = 2,
                    Quality = 30
                }
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();

            Assert.AreEqual(33, items[0].Quality);
        }

        [Test]
        public void Test_BackstagePassesSellInLessThan0()
        {
            IList<Item> items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a Metallica concert",
                    SellIn = -1,
                    Quality = 40
                }
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();

            Assert.AreEqual(0, items[0].Quality);
        }

        [Test]
        public void Test_ConjuredItemsDegradeTwiceAsFastInQuality()
        {
            IList<Item> items = new List<Item>{
                new Item {Name = "Conjured Dexterity Vest", SellIn = 5, Quality = 10}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();

            Assert.AreEqual(6, items[0].Quality);
        }

        [Test]
        public void Test_ConjuredItemsWhenDatePassed()
        {
            IList<Item> items = new List<Item>{
                new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 9}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();
            gildedRose.UpdateQuality();

            Assert.AreEqual(1, items[0].Quality);
        }

        [Test]
        public void Test_AllItemsQualityFromTextTestFeature()
        {
            IList<Item> items = new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
            GildedRose gildedRose = new GildedRose(items);
            gildedRose.UpdateQuality();

            Assert.AreEqual(19, items[0].Quality);
            Assert.AreEqual(1, items[1].Quality);
            Assert.AreEqual(6, items[2].Quality);
            Assert.AreEqual(80, items[3].Quality);
            Assert.AreEqual(80, items[4].Quality);
            Assert.AreEqual(21, items[5].Quality);
            Assert.AreEqual(50, items[6].Quality);
            Assert.AreEqual(50, items[7].Quality);
            Assert.AreEqual(4, items[8].Quality);
        }
    }
}

