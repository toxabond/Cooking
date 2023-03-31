using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodIconItemFactory<T> : IFactory<ItemType, T> where T:BaseItem{
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
            { ItemType.FullGlass, _prefabs.foodIcon.glassIcon },
            { ItemType.Burger, _prefabs.foodIcon.burgerIcon },
            { ItemType.Hotdog, _prefabs.foodIcon.hotdogIcon },
            { ItemType.FullFrenchFries, _prefabs.foodIcon.frenchFriesIcon },
        };
        var instantiatePrefabForComponent = _container.InstantiatePrefabForComponent<T>(mapper[itemType]);
        instantiatePrefabForComponent.itemType = itemType;
        return instantiatePrefabForComponent;
    }
}