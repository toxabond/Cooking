using System.Collections.Generic;
using System.Linq;
using Script.Core.Character;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    [Inject] private GameModel _gameModel;
    [Inject] private MainController _main;
    [Inject] private FoodItem.Factory _foodFactory;
    [Inject] private BaseCharacter.Factory _characterFactory;
    [Inject] private CharacterSetting _characterSetting;
    private Transform _startCharacterPlace;

    public void Init(Transform startCharacterPlace)
    {
        _startCharacterPlace = startCharacterPlace;
    }

    private List<CharacterBlock> CreateCharacterBlocks(List<CharacterBlockConfig> characterBlockConfigs,
        List<Transform> characterPlace)
    {
        var list = new List<CharacterBlock>();
        foreach (var config in characterBlockConfigs)
        {
            var foodItems = config.inventory.Select(i => GameUtil.MapperRewardFoodTypeToItemType[i]).ToList();
            list.Add(CreateCharacterBlock(config, characterPlace[config.position]));
        }

        return list;
    }

    public CharacterBlock CreateCharacterBlock(CharacterBlockConfig characterBlockConfig, Transform place)
    {
        var foodItems = characterBlockConfig.inventory.Select(i => GameUtil.MapperRewardFoodTypeToItemType[i]).ToList();
        return new CharacterBlock(characterBlockConfig.createdTime, characterBlockConfig.position,
            CreateCharacter(characterBlockConfig.type, foodItems, place));
    }

    private Model CreateCharacter(CharacterType characterType, List<ItemType> inventoryItemList,
        Transform characterPlace)
    {
        var inventory = new InventoryModel(inventoryItemList);
        var itemModel = new CharacterItemModel(ItemType.CreatedCharacter, inventory, _characterSetting);

        var character = CreateGameObjectCharacter(characterType, _startCharacterPlace);
        character.startPosition = _startCharacterPlace;
        character.targetPosition = characterPlace;
        character.inventory.InitItems(inventory.CurrentItemList);
        var gameObjectModel = new CharacterGameObjectModel(characterPlace, character);
        var model = new Model
        {
            ItemModel = itemModel,
            GameObjectModel = gameObjectModel
        };
        return model;
    }

    public IHandler CreateFrenchFriesHandler(int idGroup, int index, int externalIdGroup, int externalFromIndex,
        int externalToIndex)
    {
        var handlers = new List<IHandler>();


        var handler0 = CreateHandler<Handler>(ModifyItemType.Next, idGroup, index, index, -1, 0, 0);
        handlers.Add(handler0);
        var handler1 = CreateHandler<Handler>(ModifyItemType.Next, idGroup, index, index, -1, 0, 0);
        handlers.Add(handler1);
        var handler2 = CreateHandler<Handler>(ModifyItemType.External, idGroup, index, index, externalIdGroup,
            externalFromIndex, externalToIndex);
        handlers.Add(handler2);
        var sequenceBehavior = new SequenceBehavior(handlers);
        return sequenceBehavior;
    }

    public IHandler CreateGlassHandler(int idGroup, int index, int externalIdGroup, int externalFromIndex,
        int externalToIndex)
    {
        var handlers = new List<IHandler>();
        var handler0 = CreateHandler<Handler>(ModifyItemType.Next, idGroup, index, index, -1, 0, 0);
        handlers.Add(handler0);
        var handler1 = CreateHandler<Handler>(ModifyItemType.External, idGroup, index, index, externalIdGroup,
            externalFromIndex, externalToIndex);
        handlers.Add(handler1);
        var sequenceBehavior = new SequenceBehavior(handlers);
        return sequenceBehavior;
    }

    public T CreateHandlerOnlyExternal<T>(ModifyItemType modifyItemType,
        int externalIdGroup, int externalFromIndex, int externalToIndex,
        IChoiceStrategy strategy = null) where T : BaseHandler, new()
    {
        return CreateHandler<T>(modifyItemType, -1, 0, 0, externalIdGroup, externalFromIndex, externalToIndex);
    }

    public T CreateHandler<T>(ModifyItemType modifyItemType, int idGroup, int fromIndex, int toIndex,
        int externalIdGroup,
        int externalFromIndex, int externalToIndex, IChoiceStrategy strategy = null) where T : BaseHandler, new()
    {
        var handler = new T
        {
            GameModel = _gameModel,
            MainController = _main,
            ModifyItemType = modifyItemType,
            Strategy = strategy ?? new LowerTimeStrategy(_gameModel),
            ItemList = new List<Place>(),
            ExternalItemList = new List<Place>()
        };
        if (idGroup != -1)
        {
            for (var i = fromIndex; i <= toIndex; i++)
            {
                handler.ItemList.Add(new Place(idGroup, i));
            }
        }

        if (externalIdGroup != -1)
        {
            for (var i = externalFromIndex; i <= externalToIndex; i++)
            {
                handler.ExternalItemList.Add(new Place(externalIdGroup, i));
            }
        }

        return handler;
    }

    public FoodItem CreateItem(ItemType itemType, Transform transform)
    {
        var foodItem = _foodFactory.Create(itemType);
        var itemTransform = foodItem.transform;
        itemTransform.SetParent(transform.parent);
        itemTransform.localScale = Vector3.one;
        ApplyPosition(itemTransform, transform);
        return foodItem;
    }

    public BaseCharacter CreateGameObjectCharacter(CharacterType characterType, Transform position)
    {
        var character = _characterFactory.Create(characterType);

        var characterTransform = character.transform;
        characterTransform.SetParent(position.parent);
        characterTransform.localScale = Vector3.one;
        ApplyPosition(characterTransform, position);
        return character;
    }

    private void ApplyPosition(Transform item, Transform target)
    {
        var current = Camera.main;
        var screenToWorldPosition = current.ScreenToWorldPoint(target.position);
        var pos = RectTransformUtility.WorldToScreenPoint(current, screenToWorldPosition);
        item.position = pos;
    }
}

public interface IGameFactory
{
    void Init(Transform startCharacterPlace);

    IHandler CreateFrenchFriesHandler(int idGroup, int index, int externalIdGroup, int externalFromIndex,
        int externalToIndex);

    T CreateHandlerOnlyExternal<T>(ModifyItemType modifyItemType,
        int externalIdGroup, int externalFromIndex, int externalToIndex,
        IChoiceStrategy strategy = null) where T : BaseHandler, new();

    IHandler CreateGlassHandler(int idGroup, int index, int externalIdGroup, int externalFromIndex,
        int externalToIndex);

    T CreateHandler<T>(ModifyItemType modifyItemType, int idGroup, int fromIndex, int toIndex,
        int externalIdGroup,
        int externalFromIndex, int externalToIndex, IChoiceStrategy strategy = null) where T : BaseHandler, new();

    CharacterBlock CreateCharacterBlock(CharacterBlockConfig characterBlockConfig, Transform place);

    FoodItem CreateItem(ItemType itemType, Transform transform);
}