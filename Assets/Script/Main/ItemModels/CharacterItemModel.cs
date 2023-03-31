using System;
using DG.Tweening;

public class CharacterItemModel : ItemModel
{
    public CharacterItemModel(ItemType itemType, InventoryModel inventory, CharacterSetting characterSetting) : base(itemType, inventory)
    {
        Inventory = inventory;
        ItemTimer.Current = 0;
        ItemTimer.Duration = characterSetting.waitTime;
    }

    public override void UpdateByDeltaTimer(float delta)
    {
        if (Type == ItemType.Character)
        {
            UpdateTimer(delta);
            if (Math.Abs(ItemTimer.Current - ItemTimer.Duration) < float.Epsilon)
            {
                UpdateCharacter();
            }
        }
    }

    private void UpdateCharacter()
    {
        ToItemType = GameUtil.ItemTypeToNextItemType[ToItemType];
    }

    private void UpdateTimer(float delta)
    {
        ItemTimer.Current += delta;
        if (ItemTimer.Current > ItemTimer.Duration)
        {
            ItemTimer.Current = ItemTimer.Duration;
        }
    }
}