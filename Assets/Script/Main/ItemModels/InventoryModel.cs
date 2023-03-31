using System.Collections.Generic;
using System.Linq;

public class InventoryModel
{
    private readonly ItemType[] ItemList;
    public List<ItemType> CurrentItemList;
    public List<ItemType> AppliedItemList;

    public InventoryModel(List<ItemType> itemTypes)
    {
        ItemList = itemTypes.ToArray();
        CurrentItemList = itemTypes.ToList();
        AppliedItemList = new List<ItemType>();
    }

    public bool CanApply(ItemType itemType)
    {
        return CurrentItemList.Contains(itemType);
    }

    public void Apply(ItemType itemType)
    {
        CurrentItemList.Remove(itemType);
        AppliedItemList.Add(itemType);
    }

    public void Reset()
    {
        CurrentItemList = ItemList.ToList();
        AppliedItemList = new List<ItemType>();
    }

    public bool IsReady()
    {
        return CurrentItemList.Count == 0;
    }
}