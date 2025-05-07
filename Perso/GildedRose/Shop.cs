using System;

namespace GildedRose
{
public class Shop
{
    private ItemsRepository itemsRepository;
    public Item[] Inventory { get; private set; }

    public Shop(ItemsRepository itemsRepository)
    {
        this.itemsRepository = itemsRepository;
        this.Inventory = this.itemsRepository.GetInventory();
    }

    public void UpdateInventory()
    {
        this.Inventory = this.itemsRepository.GetInventory();
        foreach (Item i in this.Inventory)
            i.Update();

        this.itemsRepository.SaveInventory(this.Inventory);
    }
}

}