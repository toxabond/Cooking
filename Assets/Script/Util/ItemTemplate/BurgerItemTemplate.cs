using System.Collections.Generic;

public class BurgerItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new[]
        {
            ItemType.DestroyBurger
        };
    }

    public IReadOnlyDictionary<RewardFoodType, ItemType> GetRewardFoodTypeToItemTypeMapper()
    {
        return new Dictionary<RewardFoodType, ItemType>()
        {
            { RewardFoodType.Burger, ItemType.Burger },
        };
    }

    public ItemType[] GetItemTypeListForInventory()
    {
        return new[]{ItemType.Burger, ItemType.DestroyBurger};
    }

    public InventoryModel CreateInventory()
    {
            var list = new List<ItemType>()
            {
                ItemType.Meat,
                ItemType.Tomato,
                ItemType.Cheese
            };
            return new InventoryModel(list);
    }

    public IReadOnlyDictionary<ItemType, ItemType> GetItemTypeToNextItemType()
    {
        return new Dictionary<ItemType, ItemType>()
        {
            { ItemType.DestroyBurger, ItemType.Burger },
            { ItemType.Burger, ItemType.DestroyBurger },
        };
    }
}