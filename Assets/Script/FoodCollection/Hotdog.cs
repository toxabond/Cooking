using System;
using UnityEngine;

public class Hotdog : FoodItem
{ 
    [SerializeField] private GameObject cabbage;
    [SerializeField] private GameObject sausage;
    [SerializeField] private GameObject mustard;
    
    public override void Appear(Action action)
    {
        cabbage.SetActive(false);
        mustard.SetActive(false);
        sausage.SetActive(false);
        
        action();
    }
    
    public override void Next(ItemType fromItemType, ItemType toItemType, Action action)
    {
        if (toItemType == ItemType.DestroyHotdog)
        {
            Destroy(gameObject);
        }

        action();
    }
    
    public override void ApplyItem(ItemType itemType, Action action)
    {
        switch (itemType)
        {
            case ItemType.Cabbage:
                cabbage.SetActive(true);
                break;
            case ItemType.Mustard:
                mustard.SetActive(true);
                break;
            case ItemType.Sausage:
                sausage.SetActive(true);
                break;
            
            default:
                Debug.LogError( "Hotdog don't found ItemType:" + itemType);
                break;
        }

        action();
    }
}
