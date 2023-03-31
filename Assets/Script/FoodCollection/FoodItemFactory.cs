using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodItemFactory<T> : IFactory<ItemType, T>{
    [Inject] private PrefabsCollection _prefabs;
    [Inject]
    readonly DiContainer _container = null;

    public DiContainer Container
    {
        get { return _container; }
    }
        
        
    public T Create(ItemType itemType)
    {
        var mapper = new Dictionary<ItemType, GameObject>()
        {
            { ItemType.EmptyGlass, _prefabs.food.glass },
            { ItemType.Burger, _prefabs.food.burger },
            { ItemType.Meat, _prefabs.food.meat },
            { ItemType.Hotdog, _prefabs.food.hotdog },
            { ItemType.Sausage, _prefabs.food.sausage },
            { ItemType.Phase0FrenchFries, _prefabs.food.frenchFries },
            
        };
        if (!mapper.ContainsKey(itemType))
        {
            Debug.LogError("Don't found itemType:"+itemType);
        }

        var gameObject = mapper[itemType];
        if (gameObject == null)
        {
            Debug.LogError("Don't found itemType:"+itemType+". Need add into GameSettingsInstaller food section");
        }
        return _container.InstantiatePrefabForComponent<T>(gameObject);
    }
}