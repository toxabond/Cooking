using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModel : ITickableUpdate
{
    public GameState GameState = GameState.Pause;
    public bool IsActiveInput;
    public Dictionary<int, Model> Models;
    public LevelModel Level;
    public CharacterModel CharacterModel;
    private float _gameTime;
    public float GameTime => _gameTime;

    public void Init(LevelModel level, CharacterModel characterModel)
    {
        GameState = GameState.Playing;
        Models = new Dictionary<int, Model>();
        IsActiveInput = true;
        Level = level;
        CharacterModel = characterModel;
        _gameTime = 0;
    }


    public bool ContainsKey(Place place)
    {
        return Models.ContainsKey(place.GetHashCode());
    }

    public ItemModel GetItemModelByPlace(Place place)
    {
        // return _itemModels[place.GetHashCode()];
        if (!Models.ContainsKey(place.GetHashCode()))
        {
            Debug.LogError("Don't found place:" + place);
        }

        return Models[place.GetHashCode()].ItemModel;
    }


    public GameObjectModel GetGameObjectModelByPlace(Place place)
    {
        // return _gameObjectModels[place.GetHashCode()];
        if (place == null || !Models.ContainsKey(place.GetHashCode()))
        {
            Debug.LogError("Don't found place:" + place);
        }

        return Models[place.GetHashCode()].GameObjectModel;
    }

    public void UpdateByDeltaTimer(float delta)
    {
        _gameTime += delta;
        Level.UpdateByDeltaTimer(delta);
        foreach (var pair in Models)
        {
            UpdateModel(delta, pair.Value);
        }

        CheckWinGame(Models);
        CheckGameOverByTime();
    }

    private void CheckWinGame(Dictionary<int, Model> models)
    {
        var gameModelCharacterModel = CharacterModel;
        if (GameState == GameState.Playing &&
            gameModelCharacterModel.Amount == gameModelCharacterModel.CurrentAmount)
        {
            var isFindActiveCharacter = false;
            foreach (var pair in models)
            {
                if (pair.Value.ItemModel.FromItemType == ItemType.Character || pair.Value.ItemModel.ToItemType == ItemType.Character)
                {
                    isFindActiveCharacter = true;
                }
            }

            if (!isFindActiveCharacter)
            {
                GameState = GameState.Win;
            }
        }
    }

    private void UpdateModel(float delta, Model model)
    {
        if (model.ItemModel is ITickableUpdate itemModel)
        {
            var previousType = model.ItemModel.Type;
            itemModel.UpdateByDeltaTimer(delta);
            var currentType = model.ItemModel.Type;
            if (previousType != currentType && currentType == ItemType.Playing)
            {
                if (model.ItemModel.ToItemType == ItemType.DestroyedCharacter)
                {
                    GameState = GameState.GameOver;
                }

                if (GameUtil.DestroyedItemType.Contains(model.ItemModel.ToItemType))
                {
                    model.GameObjectModel.Item.Disappear(() => { });
                }
            }
        }
    }
    
    private void CheckGameOverByTime()
    {
        if (_gameTime > Level.Timer.Current)
        {
            GameState = GameState.GameOver;
        }
    }
    
}