using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public GameState GameState = GameState.Pause;
    public bool IsActiveInput;
    public float GameTime;
    public Dictionary<int, Model> Models { get; private set; }
    public LevelModel Level { get; private set; }
    public CharacterModel CharacterModel { get; private set; }

    public void Init(LevelModel level, CharacterModel characterModel)
    {
        GameState = GameState.Playing;
        Models = new Dictionary<int, Model>();
        IsActiveInput = true;
        Level = level;
        CharacterModel = characterModel;
        GameTime = 0;
    }

    public bool ContainsKey(Place place)
    {
        return Models.ContainsKey(place.GetHashCode());
    }

    public ItemModel GetItemModelByPlace(Place place)
    {
        if (!Models.ContainsKey(place.GetHashCode()))
        {
            Debug.LogError("Don't found place:" + place);
        }

        return Models[place.GetHashCode()].ItemModel;
    }

    public GameObjectModel GetGameObjectModelByPlace(Place place)
    {
        if (place == null || !Models.ContainsKey(place.GetHashCode()))
        {
            Debug.LogError("Don't found place:" + place);
        }

        return Models[place.GetHashCode()].GameObjectModel;
    }
}