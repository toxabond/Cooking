using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    public GameObject holder;
    private List<FoodIconItem> _activeItemCollection;
    [Inject] private FoodIconItem.Factory _foodIconItemFactory;

    public void Init(IEnumerable<ItemType> itemList)
    {
        _activeItemCollection = new List<FoodIconItem>();
        foreach (var itemType in itemList)
        {
            var icon = _foodIconItemFactory.Create(itemType);
            _activeItemCollection.Add(icon);
            var iconTransform = icon.transform;
            iconTransform.SetParent(holder.transform);
            iconTransform.localScale = Vector3.one;
        }
    }

    public void ApplyItem(ItemType itemType, Action action)
    {
        var index = _activeItemCollection.FindIndex(item => item.itemType == itemType);
        if (index != -1)
        {
            Destroy(_activeItemCollection[index].gameObject);
            _activeItemCollection.RemoveAt(index);
        }

        action();
    }
}