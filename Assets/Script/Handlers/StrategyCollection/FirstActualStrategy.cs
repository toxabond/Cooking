using System.Collections.Generic;
using System.Linq;
using ModestTree;

public class FirstActualStrategy : IChoiceStrategy
{
    private readonly GameModel _gameModel;

    public FirstActualStrategy(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public virtual Place GetActualTarget(ModifyItemType modifyItemType, IReadOnlyList<Place> targetList)
    {
        if (targetList == null || targetList.IsEmpty())
        {
            return null;
        }

        Place result = null;
        ItemModel itemModel;
        switch (modifyItemType)
        {
            case ModifyItemType.Create:
                foreach (var place in targetList)
                {
                    itemModel = _gameModel.GetItemModelByPlace(place);
                    if (itemModel != null && GameUtil.DestroyedItemType.Contains(itemModel.Type))
                    {
                        result = place;
                        break;
                    }
                }

                break;
            case ModifyItemType.External:
                itemModel = _gameModel.GetItemModelByPlace(targetList.First());
                if (itemModel.Type != ItemType.Playing && itemModel.IsReady())
                {
                    result = targetList.First();
                }

                break;
            case ModifyItemType.Next:
                itemModel = _gameModel.GetItemModelByPlace(targetList.First());
                if (itemModel.Type != ItemType.Playing && !GameUtil.DestroyedItemType.Contains(itemModel.Type))
                {
                    result = targetList.First();
                }

                break;
            default:
                result = targetList.First();
                break;
        }

        return result;
    }

    public virtual Place GetActualExternalTarget(ModifyItemType modifyItemType, Place targetPlace, ItemType itemType,
        IReadOnlyList<Place> externalTargetList)
    {
        if (externalTargetList == null || externalTargetList.IsEmpty())
        {
            return null;
        }

        Place result = null;
        switch (modifyItemType)
        {
            case ModifyItemType.Create:
                foreach (var place in externalTargetList)
                {
                    if (GameUtil.DestroyedItemType.Contains(_gameModel.GetItemModelByPlace(place).Type))
                    {
                        result = place;
                        break;
                    }
                }

                break;
            case ModifyItemType.External:
                var itemModel = _gameModel.GetItemModelByPlace(targetPlace);
                foreach (var place in externalTargetList)
                {
                    if (!_gameModel.ContainsKey(place))
                    {
                        continue;
                    }

                    if (_gameModel.GetItemModelByPlace(place).CanApply(itemModel.Type))
                    {
                        result = place;
                        break;
                    }
                }

                break;
            case ModifyItemType.Apply:
                foreach (var place in externalTargetList)
                {
                    if (_gameModel.GetItemModelByPlace(place).CanApply(itemType))
                    {
                        result = place;
                        break;
                    }
                }

                break;
            default:
                result = externalTargetList[0];
                break;
        }

        return result;
    }
}