using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodIconItemFactory<T> : IFactory<ItemType, T> where T : BaseItem
{
    private readonly DiContainer _container;
    private readonly Dictionary<ItemType, GameObject> _itemTypeToGameObjectMapper;

    public FoodIconItemFactory(PrefabsCollection prefabs, DiContainer container)
    {
        _container = container;
        _itemTypeToGameObjectMapper = new Dictionary<ItemType, GameObject>()
        {
            { ItemType.FullGlass, prefabs.foodIcon.glassIcon },
            { ItemType.Burger, prefabs.foodIcon.burgerIcon },
            { ItemType.Hotdog, prefabs.foodIcon.hotdogIcon },
            { ItemType.FullFrenchFries, prefabs.foodIcon.frenchFriesIcon },
        };
    }

    public T Create(ItemType itemType)
    {
        var instantiatePrefabForComponent =
            _container.InstantiatePrefabForComponent<T>(_itemTypeToGameObjectMapper[itemType]);
        instantiatePrefabForComponent.itemType = itemType;
        return instantiatePrefabForComponent;
    }
}