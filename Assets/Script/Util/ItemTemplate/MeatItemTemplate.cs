﻿using System.Collections.Generic;

public class MeatItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new []
        {
            ItemType.DestroyMeat
        };
    }

    public Dictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper()
    {
        return null;
    }

    public ItemType[] GetItemTypeListForInventory()
    {
        return new ItemType[] { };
    }

    public InventoryModel CreateInventory()
    {
        return null;
    }

    public Dictionary<ItemType, ItemType> GetItemTypeToNextItemType()
    {
        return new Dictionary<ItemType, ItemType>()
        {
            { ItemType.DestroyMeat, ItemType.Meat },
            { ItemType.Meat, ItemType.DestroyMeat },
        };
    }
}