using System;

public interface IItem
{
    void Appear(Action action);
    void Next(ItemType fromItemType, ItemType toItemType, Action action);
    void ApplyItem(ItemType itemType, Action action);
    void Bind(ItemModel modelItemModel);
    void Disappear(Action action);
}