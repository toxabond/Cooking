using System.Collections.Generic;
using System.Linq;

public class InventoryModel
{
    private readonly ItemType[] _itemList;
    public List<ItemType> CurrentItemList { get; private set; }
    public List<ItemType> AppliedItemList { get; private set; }

    public InventoryModel(IReadOnlyList<ItemType> itemTypes)
    {
        _itemList = itemTypes.ToArray();
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
        CurrentItemList = _itemList.ToList();
        AppliedItemList = new List<ItemType>();
    }

    public bool IsReady()
    {
        return CurrentItemList.Count == 0;
    }
}