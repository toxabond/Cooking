using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameBuilder : IGameBuilder
{
    [Inject]private GamedBind _gamedBind;
    [Inject]private IGameFactory _factory;

    
    public GameBuilder BindPlaceWithGameObjectModel(int idGroup, IReadOnlyList<Transform> placeList, int fromIndex = 0,int toIndex=-1,
        ItemType itemType = ItemType.None)
    {
        if (toIndex == -1)
        {
            toIndex = placeList.Count-1;
        }
        for (var i = fromIndex; i <= toIndex; i++)
        {
            var food = itemType == ItemType.None ? null : _factory.CreateItem(itemType, placeList[i]);
            _gamedBind.BindPlaceWithGameObject(new Place(idGroup, i), new GameObjectModel(placeList[i], food));
        }

        return this;
    }

    public GameBuilder BindPlaceWithItemModel(int idGroup, int fromIndex, int toIndex, ItemType itemType)
    {
        for (var i = fromIndex; i <= toIndex; i++)
        {
            var itemModel = new ItemModel(itemType, GameUtil.CreateInventory(itemType));

            _gamedBind.BindPlaceWithItemModel(new Place(idGroup, i), itemModel);
        }

        return this;
    }
    
    public GameBuilder Subscription(ModifyItemType modifyItemType, int idGroup, int fromIndex, int toIndex,
        Button button, IChoiceStrategy strategy = null)
    {
        var handler = _factory.CreateHandler<Handler>(modifyItemType, idGroup, fromIndex, toIndex, -1, 0, 0, strategy);
        button.onClick.AddListener(() => { handler.Execute(); });
        return this;
    }

    public GameBuilder SubscriptionExternal(int idGroup, IReadOnlyList<Button> button, int fromIndex, int externalIdGroup,
        int externalFromIndex, int externalToIndex,
        IChoiceStrategy strategy = null)
    {
        for (var i = fromIndex; i < button.Count; i++)
        {
            var handler = _factory.CreateHandler<Handler>(ModifyItemType.External, idGroup, i - fromIndex, i - fromIndex,
                externalIdGroup, externalFromIndex, externalToIndex, strategy);

            button[i].onClick.AddListener(() => { handler.Execute(); });
        }

        return this;
    }

}

public interface IGameBuilder
{
    GameBuilder BindPlaceWithGameObjectModel(int idGroup, IReadOnlyList<Transform> placeList, int fromIndex = 0,
        int toIndex = -1,
        ItemType itemType = ItemType.None);

    GameBuilder SubscriptionExternal(int idGroup, IReadOnlyList<Button> button, int fromIndex, int externalIdGroup,
        int externalFromIndex, int externalToIndex,
        IChoiceStrategy strategy = null);
}