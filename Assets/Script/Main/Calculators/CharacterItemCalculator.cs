using System;
using System.Linq;
using Zenject;

public class CharacterItemCalculator : ITickableUpdate
{
    [Inject] private GameModel _gameModel;

    public void UpdateByDeltaTimer(float delta)
    {
        foreach (var pair in _gameModel.Models)
        {
            UpdateModel(delta, pair.Value);
        }
    }

    private void UpdateModel(float delta, Model model)
    {
        // if (model.ItemModel is ITickableUpdate itemModel)
        // {
        var previousType = model.ItemModel.Type;
        UpdateCharacterItem(delta, model.ItemModel);
        var currentType = model.ItemModel.Type;
        if (previousType != currentType && currentType == ItemType.Playing)
        {
            if (model.ItemModel.ToItemType == ItemType.DestroyedCharacter)
            {
                _gameModel.GameState = GameState.GameOver;
            }

            if (GameUtil.DestroyedItemType.Contains(model.ItemModel.ToItemType))
            {
                model.GameObjectModel.Item.Disappear(() => { });
            }
        }
        // }
    }

    private void UpdateCharacterItem(float delta, ItemModel model)
    {
        if (model.Type == ItemType.Character)
        {
            UpdateTimer(delta, model);
            if (Math.Abs(model.ItemTimer.Current - model.ItemTimer.Duration) < float.Epsilon)
            {
                model.ToItemType = GameUtil.ItemTypeToNextItemType[model.ToItemType];
            }
        }
    }

    private void UpdateTimer(float delta, ItemModel model)
    {
        var itemTimer = model.ItemTimer;

        itemTimer.Current += delta;
        if (itemTimer.Current > itemTimer.Duration)
        {
            itemTimer.Current = itemTimer.Duration;
        }
    }
}