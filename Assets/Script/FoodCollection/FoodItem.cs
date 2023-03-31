using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class FoodItem : MonoBehaviour, IItem
{
    public abstract void Next(ItemType fromItemType, ItemType toItemType, Action action);
    public abstract void ApplyItem(ItemType itemType, Action action);

    public virtual void Bind(ItemModel modelItemModel)
    {
    }
    
    public abstract void Appear(Action action);
    public void Disappear(Action action)
    {
        throw new NotImplementedException();
    }

    public class Factory : PlaceholderFactory<ItemType, FoodItem>
    {
    }
}