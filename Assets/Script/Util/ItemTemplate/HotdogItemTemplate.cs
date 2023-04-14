using System.Collections.Generic;

public class HotdogItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new[]
        {
            ItemType.DestroyHotdog
        };
    }

    public IReadOnlyDictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper()
    {
        return new Dictionary<RewardFoodType, ItemType>()
        {
            { RewardFoodType.Hotdog, ItemType.Hotdog },
        };
    }

    public ItemType[] GetItemTypeListForInventory()
    {
        return new[]
        {
            ItemType.Hotdog,
            ItemType.DestroyHotdog
        };
    }

    public InventoryModel CreateInventory()
    {
        var list = new List<ItemType>()
        {
            ItemType.Cabbage,
            ItemType.Sausage,
            ItemType.Mustard
        };
        return new InventoryModel(list);
    }

    public IReadOnlyDictionary<ItemType, ItemType> GetItemTypeToNextItemType()
    {
        return new Dictionary<ItemType, ItemType>()
        {
            { ItemType.DestroyHotdog, ItemType.Hotdog },
            { ItemType.Hotdog, ItemType.DestroyHotdog },
        };
    }
}