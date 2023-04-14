
    using System;
    using System.Collections.Generic;
    using Zenject;

    public class FoodAnalyzer : IFoodAnalyzer
    {
        private List<ItemType> _destroyItemType;
        private Dictionary<RewardFoodType, ItemType> _rewardFoodTypeToItemTypeMapper;
        private Dictionary<ItemType,Func<InventoryModel>> _createInventoryMapper;
        private Dictionary<ItemType, ItemType> _itemTypeToNextItemType;
        
        [Inject]
        public void Construct(List<IItemTemplate> itemTemplates)
        {
            _destroyItemType = new List<ItemType>();
            _rewardFoodTypeToItemTypeMapper = new Dictionary<RewardFoodType, ItemType>();
            _createInventoryMapper = new Dictionary<ItemType, Func<InventoryModel>>();
            _itemTypeToNextItemType = new Dictionary<ItemType, ItemType>();
            itemTemplates.ForEach(template =>
            {
                _destroyItemType.AddRange(template.GetDestroyItemType());
                var mapper = template.GetRewardFoodTypeToItemTypeMapper();
                foreach (var itemType in mapper)
                {
                    _rewardFoodTypeToItemTypeMapper.Add(itemType.Key,itemType.Value);
                }

                var itemTypeListForInventory = template.GetItemTypeListForInventory();
                foreach (var itemType in itemTypeListForInventory)
                {
                    _createInventoryMapper.Add(itemType, template.CreateInventory);
                }
                
                var itemTypeToNextItemTypeTemp = template.GetItemTypeToNextItemType();
                foreach (var itemType in itemTypeToNextItemTypeTemp)
                {
                    _itemTypeToNextItemType.Add(itemType.Key, itemType.Value);
                }

            });
            
        }

        public List<ItemType> GetDestroyItemType()
        {
            return _destroyItemType;
        }

        public Dictionary<RewardFoodType, ItemType> RewardFoodTypeToItemTypeMapper()
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

        public Dictionary<ItemType, ItemType> ItemTypeToNextItemType()
        {
            return _itemTypeToNextItemType;
        }
    }