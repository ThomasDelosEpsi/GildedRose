using System;
using System.Dynamic;

namespace GildedRose
{
    public class InMemoryItemRepository : ItemsRepository{
        private Item[] Inventory;
        public Item[] GetInventory()
        {
             return this.Inventory;
        }

        public void SaveInventory(Item[] items)
        {
            this.Inventory = items;
        }

  
    }
}