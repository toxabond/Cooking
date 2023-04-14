using System.Collections.Generic;

public class CharacterItemTemplate : IItemTemplate
{
    public ItemType[] GetDestroyItemType()
    {
        return new[]
        {
            ItemType.DestroyedCharacter
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
            { ItemType.CreatedCharacter, ItemType.Character },
            { ItemType.Character, ItemType.DestroyedCharacter },
            { ItemType.DestroyedCharacter, ItemType.CreatedCharacter },
        };
    }
}