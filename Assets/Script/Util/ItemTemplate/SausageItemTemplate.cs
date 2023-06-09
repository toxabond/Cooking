﻿using System.Collections.Generic;

public class SausageItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new []
        {
            ItemType.DestroySausage
        };
    }

    public IReadOnlyDictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper()
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

    public IReadOnlyDictionary<ItemType, ItemType> GetItemTypeToNextItemType()
    {
        return new Dictionary<ItemType, ItemType>()
        {
            { ItemType.DestroySausage, ItemType.Sausage },
            { ItemType.Sausage, ItemType.DestroySausage },
        };
    }
}