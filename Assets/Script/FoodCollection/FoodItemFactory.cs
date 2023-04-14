using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodItemFactory<T> : IFactory<ItemType, T>
{
    private readonly DiContainer _container;
    private readonly Dictionary<ItemType, GameObject> _itemTypeToGameObjectMapper;

    public FoodItemFactory(PrefabsCollection prefabs, DiContainer container)
    {
        _container = container;
        _itemTypeToGameObjectMapper = new Dictionary<ItemType, GameObject>()
        {
            { ItemType.EmptyGlass, prefabs.food.glass },
            { ItemType.Burger, prefabs.food.burger },
            { ItemType.Meat, prefabs.food.meat },
            { ItemType.Hotdog, prefabs.food.hotdog },
            { ItemType.Sausage, prefabs.food.sausage },
            { ItemType.Phase0FrenchFries, prefabs.food.frenchFries },
        };
    }

    public T Create(ItemType itemType)
    {
        if (!_itemTypeToGameObjectMapper.ContainsKey(itemType))
        {
            Debug.LogError("Don't found itemType:" + itemType);
        }

        var gameObject = _itemTypeToGameObjectMapper[itemType];
        if (gameObject == null)
        {
            Debug.LogError("Don't found itemType:" + itemType + ". Need add into GameSettingsInstaller food section");
        }

        return _container.InstantiatePrefabForComponent<T>(gameObject);
    }
}