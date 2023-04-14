using System.Collections.Generic;

public interface IItemTemplate
{
    ItemType[] GetDestroyItemType();
    Dictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper();

    ItemType[] GetItemTypeListForInventory();
    InventoryModel CreateInventory();
    Dictionary<ItemType, ItemType> GetItemTypeToNextItemType();
}