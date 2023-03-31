using System;
using UnityEngine;
using Zenject;

public class MainController
{
    [Inject] private GameModel _gameModel;
    [Inject] private FoodItem.Factory _foodItemFactory;

    public void ApplyExecute(ModifyItemType modifyItemType, ItemType itemType, Place externalPlace)
    {
        var gameObjectModel = _gameModel.GetGameObjectModelByPlace(externalPlace);
        var itemModel = _gameModel.GetItemModelByPlace(externalPlace);
        switch (modifyItemType)
        {
            case ModifyItemType.Apply:
                itemModel.Apply(itemType);
                gameObjectModel.Item.ApplyItem(itemType, () => { });
                break;
            default:
                Debug.LogError("GameView: don't found ModifyItemType:" + modifyItemType);
                break;
        }
    }

    public void Execute(ModifyItemType modifyItemType, Place place, Place externalPlace)
    {
        var gameObjectModel = _gameModel.GetGameObjectModelByPlace(place);
        var itemModel = _gameModel.GetItemModelByPlace(place);
        if (itemModel.FromItemType != itemModel.ToItemType)
        {
            Debug.Log("Item is playing!!!");
            // Debug.Log("Item is playing!!!");
            return;
        }

        switch (modifyItemType)
        {
            case ModifyItemType.Next:
                NextExecute(itemModel, gameObjectModel);

                break;
            case ModifyItemType.External:
                var externalGameObjectModel = _gameModel.GetGameObjectModelByPlace(externalPlace);
                var externalItemModel = _gameModel.GetItemModelByPlace(externalPlace);

                var itemType = itemModel.Type;
                NextExecute(itemModel, gameObjectModel);

                // itemModel.Apply(itemType);
                externalItemModel.Apply(itemType);
                if (externalItemModel.ToItemType == ItemType.Character)
                {
                    CharacterExecute(externalItemModel as CharacterItemModel, itemType,
                        externalGameObjectModel as CharacterGameObjectModel);
                }
                else
                {
                    externalGameObjectModel.Item.ApplyItem(itemType,
                        () => { externalItemModel.SetItemType(externalItemModel.ToItemType); });
                }

                break;
            case ModifyItemType.Create:
                var nextItemType = NextType(itemModel.ToItemType);
                itemModel.Reset();
                itemModel.ToItemType = nextItemType;
                var newItem = _foodItemFactory.Create(nextItemType);

                var itemTransform = newItem.transform;
                itemTransform.SetParent(gameObjectModel.Transform.parent);
                itemTransform.position = gameObjectModel.Transform.position;
                itemTransform.localScale = Vector3.one;

                gameObjectModel.Item = newItem;
                newItem.Appear(() => { itemModel.SetItemType(nextItemType); });
                break;
            default:
                // Debug.LogError("GameView: don't found ModifyItemType:" + modifyItemType);
                Debug.LogError("GameView: don't found ModifyItemType:" + modifyItemType);
                break;
        }
    }

    private void NextExecute(ItemModel itemModel, GameObjectModel gameObjectModel)
    {
        var nextItemType = NextType(itemModel.ToItemType);

        if (gameObjectModel.Item != null)
        {
            itemModel.ToItemType = nextItemType;
            gameObjectModel.Item.Next(itemModel.FromItemType, itemModel.ToItemType,
                () => itemModel.SetItemType(nextItemType));
        }
        else
        {
            throw new Exception("");
        }
    }

    private void CharacterExecute(CharacterItemModel itemModel, ItemType itemType,
        CharacterGameObjectModel gameObjectModel)
    {
        if (gameObjectModel.Item != null)
        {
            gameObjectModel.Item.ApplyItem(itemType, () => { UpdateCharacter(itemModel, gameObjectModel); });
        }
        else
        {
            throw new Exception("gameObjectModel.Item is null");
        }
    }

    private void UpdateCharacter(CharacterItemModel itemModel, CharacterGameObjectModel gameObjectModel)
    {
        if (itemModel.Inventory.IsReady())
        {
            itemModel.SetItemType(NextType(itemModel.ToItemType));
        }

        if (itemModel.Type == ItemType.DestroyedCharacter)
        {
            gameObjectModel.Character.Disappear(() => { });
        }
    }

    private ItemType NextType(ItemType itemType)
    {
        return GameUtil.ItemTypeToNextItemType[itemType];
    }
}