using System.Collections.Generic;

public static class GameUtil
{
    public static ItemType[] DestroyedItemType = new ItemType[]
    {
        ItemType.None,
        ItemType.DestroyedCharacter,
        ItemType.DestroyMeat,
        ItemType.DestroyBurger,
        ItemType.DestroyHotdog,
        ItemType.DestroySausage,
        ItemType.DestroyFrenchFries
    };

    public static Dictionary<RewardFoodType, ItemType> MapperRewardFoodTypeToItemType =
        new Dictionary<RewardFoodType, ItemType>()
        {
            { RewardFoodType.Glass, ItemType.FullGlass },
            { RewardFoodType.Burger, ItemType.Burger },
            { RewardFoodType.Hotdog, ItemType.Hotdog },
            { RewardFoodType.FrenchFries, ItemType.FullFrenchFries },
        };

    public static Dictionary<ItemType, ItemType> ItemTypeToNextItemType = new Dictionary<ItemType, ItemType>()
    {
        { ItemType.EmptyGlass, ItemType.FullGlass },
        { ItemType.FullGlass, ItemType.EmptyGlass },

        { ItemType.CreatedCharacter, ItemType.Character },
        { ItemType.Character, ItemType.DestroyedCharacter },
        { ItemType.DestroyedCharacter, ItemType.CreatedCharacter },

        { ItemType.DestroyBurger, ItemType.Burger },
        { ItemType.Burger, ItemType.DestroyBurger },

        { ItemType.DestroyMeat, ItemType.Meat },
        { ItemType.Meat, ItemType.DestroyMeat },

        { ItemType.DestroyHotdog, ItemType.Hotdog },
        { ItemType.Hotdog, ItemType.DestroyHotdog },

        { ItemType.DestroySausage, ItemType.Sausage },
        { ItemType.Sausage, ItemType.DestroySausage },

        { ItemType.DestroyFrenchFries, ItemType.Phase0FrenchFries },
        { ItemType.Phase0FrenchFries, ItemType.Phase1FrenchFries },
        { ItemType.Phase1FrenchFries, ItemType.FullFrenchFries },
        { ItemType.FullFrenchFries, ItemType.DestroyFrenchFries },
    };
    
    public static InventoryModel CreateInventory(ItemType itemType)
    {
        InventoryModel result;
        List<ItemType> list;
        switch (itemType)
        {
            case ItemType.Burger:
            case ItemType.DestroyBurger:
                list = new List<ItemType>()
                {
                    ItemType.Meat,
                    ItemType.Tomato,
                    ItemType.Cheese
                };
                result = new InventoryModel(list);
                break;
            case ItemType.Hotdog:
            case ItemType.DestroyHotdog:
                list = new List<ItemType>()
                {
                    ItemType.Cabbage,
                    ItemType.Sausage,
                    ItemType.Mustard
                };

                result = new InventoryModel(list);
                break;
            default:
                result = new InventoryModel(new List<ItemType>());
                break;
        }

        return result;
    }
}