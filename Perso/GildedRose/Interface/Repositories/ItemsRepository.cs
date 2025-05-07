using System;
using System.Dynamic;

namespace GildedRose
{
    public interface ItemsRepository
    {
       public Item[] GetInventory();

       public void SaveInventory(Item[] items);

    }
    
    }