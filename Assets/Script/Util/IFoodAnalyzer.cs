using System.Collections.Generic;

public interface IFoodAnalyzer
{
    List<ItemType> GetDestroyItemType();
    Dictionary<RewardFoodType, ItemType> RewardFoodTypeToItemTypeMapper();
    InventoryModel CreateInventory(ItemType itemType);
    Dictionary<ItemType, ItemType> ItemTypeToNextItemType();
}