using System;
using System.Collections.Generic;
using Zenject;

public class FoodAnalyzer : IFoodAnalyzer
{
    private List<ItemType> _destroyItemType;
    private Dictionary<RewardFoodType, ItemType> _rewardFoodTypeToItemTypeMapper;
    private Dictionary<ItemType, Func<InventoryModel>> _createInventoryMapper;
    private Dictionary<ItemType, ItemType> _itemTypeToNextItemType;

    [Inject]
    public void Construct(List<IItemTemplate> itemTemplates)
    {
        _destroyItemType = new List<ItemType>();
        _rewardFoodTypeToItemTypeMapper = new Dictionary<RewardFoodType, ItemType>();
        _createInventoryMapper = new Dictionary<ItemType, Func<InventoryModel>>();
        _itemTypeToNextItemType = new Dictionary<ItemType, ItemType>();
        foreach (var template in itemTemplates)
        {
            AddDestroyItemType(template);
            AddRewardFoodTypeToItemTypeMapper(template);
            AddItemTypeListForInventory(template);
            AddItemTypeToNextItemType(template);
        }
    }

    private void AddDestroyItemType(IItemTemplate template)
    {
        var destroyItemType = template.GetDestroyItemType();
        if (destroyItemType != null)
        {
            _destroyItemType.AddRange(destroyItemType);
        }
    }

    private void AddRewardFoodTypeToItemTypeMapper(IItemTemplate template)
    {
        var mapper = template.GetRewardFoodTypeToItemTypeMapper();
        if (mapper != null)
        {
            foreach (var itemType in mapper)
            {
                _rewardFoodTypeToItemTypeMapper.Add(itemType.Key, itemType.Value);
            }
        }
    }

    private void AddItemTypeListForInventory(IItemTemplate template)
    {
        var itemTypeListForInventory = template.GetItemTypeListForInventory();
        if (itemTypeListForInventory != null)
        {
            foreach (var itemType in itemTypeListForInventory)
            {
                _createInventoryMapper.Add(itemType, template.CreateInventory);
            }
        }
    }

    private void AddItemTypeToNextItemType(IItemTemplate template)
    {
        var itemTypeToNextItemTypeTemp = template.GetItemTypeToNextItemType();
        if (itemTypeToNextItemTypeTemp != null)
        {
            foreach (var itemType in itemTypeToNextItemTypeTemp)
            {
                _itemTypeToNextItemType.Add(itemType.Key, itemType.Value);
            }
        }
    }

    public IReadOnlyList<ItemType> GetDestroyItemType()
    {
        return _destroyItemType;
    }

    public IReadOnlyDictionary<RewardFoodType, ItemType> RewardFoodTypeToItemTypeMapper()
    {
        return _rewardFoodTypeToItemTypeMapper;
    }

    public InventoryModel CreateInventory(ItemType itemType)
    {
        if (_createInventoryMapper.ContainsKey(itemType))
        {
            return _createInventoryMapper[itemType]();
        }

        return new InventoryModel(new List<ItemType>());
    }

    public IReadOnlyDictionary<ItemType, ItemType> ItemTypeToNextItemType()
    {
        return _itemTypeToNextItemType;
    }
}