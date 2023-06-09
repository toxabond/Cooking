﻿public class CharacterItemModel : ItemModel
{
    public CharacterItemModel(ItemType itemType, InventoryModel inventory, CharacterSetting characterSetting) : base(itemType, inventory)
    {
        ItemTimer.Current = 0;
        ItemTimer.Duration = characterSetting.waitTime;
    }
}