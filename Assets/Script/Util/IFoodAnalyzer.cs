using System.Collections.Generic;

public interface IFoodAnalyzer
{
    IReadOnlyList<ItemType> GetDestroyItemType();
    IReadOnlyDictionary<RewardFoodType, ItemType> RewardFoodTypeToItemTypeMapper();
    InventoryModel CreateInventory(ItemType itemType);
    IReadOnlyDictionary<ItemType, ItemType> ItemTypeToNextItemType();
}