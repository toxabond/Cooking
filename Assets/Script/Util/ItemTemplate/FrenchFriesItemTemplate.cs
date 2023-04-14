using System.Collections.Generic;

public class FrenchFriesItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new []
        {
            ItemType.DestroyFrenchFries
        };
    }

    public Dictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper()
    {
        return new Dictionary<RewardFoodType, ItemType>()
        {
            { RewardFoodType.FrenchFries, ItemType.FullFrenchFries },
        };
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
            { ItemType.DestroyFrenchFries, ItemType.Phase0FrenchFries },
            { ItemType.Phase0FrenchFries, ItemType.Phase1FrenchFries },
            { ItemType.Phase1FrenchFries, ItemType.FullFrenchFries },
            { ItemType.FullFrenchFries, ItemType.DestroyFrenchFries },
        };
    }
}