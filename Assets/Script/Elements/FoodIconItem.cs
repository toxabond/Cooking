using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodIconItem : BaseItem
{
    public class Factory : PlaceholderFactory<ItemType,FoodIconItem>
    {
    }
}
