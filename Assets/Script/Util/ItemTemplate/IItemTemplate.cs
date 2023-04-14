using System.Collections.Generic;

public interface IItemTemplate
{
    ItemType[] GetDestroyItemType();
    IReadOnlyDictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper();

    ItemType[] GetItemTypeListForInventory();
    InventoryModel CreateInventory();
    IReadOnlyDictionary<ItemType, ItemType> GetItemTypeToNextItemType();
}