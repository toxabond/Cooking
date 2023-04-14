using System.Collections.Generic;

public class SausageItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new []
        {
            ItemType.DestroySausage
        };
    }

    public Dictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper()
    {
        return null;
    }

    public ItemType[] GetItemTypeListForInventory()
    {
        return new ItemType[]{};
    }

    public InventoryModel CreateInventory()
    {
        return null;
    }

    public Dictionary<ItemType, ItemType> GetItemTypeToNextItemType()
    {
        return new Dictionary<ItemType, ItemType>()
        {
            { ItemType.DestroySausage, ItemType.Sausage },
            { ItemType.Sausage, ItemType.DestroySausage },
        };
    }
}