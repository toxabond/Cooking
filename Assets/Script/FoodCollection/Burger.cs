using System;
using UnityEngine;

public class Burger : FoodItem
{
    public GameObject cheese;
    public GameObject meat;
    public GameObject tomato;
    public GameObject bun;

    public override void Appear(Action action)
    {
        bun.SetActive(true);
        cheese.SetActive(false);
        meat.SetActive(false);
        tomato.SetActive(false);
        action();
    }

    public override void Next(ItemType fromItemType, ItemType toItemType, Action action)
    {
        if (toItemType == ItemType.DestroyBurger)
        {
            Destroy(gameObject);
        }

        action();
    }

    public override void ApplyItem(ItemType itemType, Action action)
    {
        switch (itemType)
        {
            case ItemType.Cheese:
                cheese.SetActive(true);
                break;
            case ItemType.Meat:
                meat.SetActive(true);
                break;
            case ItemType.Tomato:
                tomato.SetActive(true);
                break;
            default:
                Debug.LogError( "Burger don't found ItemType:" + itemType);
                break;
        }

        action();
    }
}