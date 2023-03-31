using System;
using System.Collections.Generic;
using ModestTree;
using Script.Core.Character;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterGenerator : ITickableUpdate
{
    [Inject] public MainController Main;
    [Inject] public GamedBind GamedBind;
    [Inject] public GameFactory Factory;

    private List<CharacterBlockConfig> _blockList;
    private List<Transform> _uiCharacterPlace;
    private List<CharacterBlockConfig> _waitBlockList;
    private int _currentIndex;
    private LevelConfig _config;
    private GameModel _gameModel;

    public void Init(LevelConfig levelConfig , List<Transform> uiCharacterPlace,
        GameModel gameModel)
    {
        _currentIndex = 0;
        _config = levelConfig;
        _gameModel = gameModel;
        _waitBlockList = new List<CharacterBlockConfig>();
        _uiCharacterPlace = uiCharacterPlace;
        _blockList = CreateBlocks(levelConfig);
        _blockList.Sort((a, b) => a.createdTime - b.createdTime);
    }

    private List<CharacterBlockConfig> CreateBlocks(LevelConfig levelConfig)
    {
        if (!levelConfig.isGenerateLevel)
        {
            return levelConfig.characterConfigs;
        }
        Random.InitState(DateTime.UtcNow.GetHashCode());
        var config = levelConfig.generationConfiguration;
        var foodIcons = new List<RewardFoodType>();
        for (var i = 0; i < config.glassAmount;i++)
        {
            foodIcons.Add(RewardFoodType.Glass);
        }
        for (var i = 0; i < config.burgerAmount;i++)
        {
            foodIcons.Add(RewardFoodType.Burger);
        }
        for (var i = 0; i < config.hotdogAmount;i++)
        {
            foodIcons.Add(RewardFoodType.Hotdog);
        }
        for (var i = 0; i < config.frenchFriesAmount;i++)
        {
            foodIcons.Add(RewardFoodType.FrenchFries);
        }
        
        var list = new List<CharacterBlockConfig>();
        
        for (var i = 0; i < config.characterAmount; i++)
        {
            var block = new CharacterBlockConfig();
            block.createdTime = 2 + 10 * i;

            block.type = (CharacterType)(Random.Range(0, 4)); 
            block.position = Random.Range(0, levelConfig.characterPlaceAmount);
            block.inventory = new List<RewardFoodType>();
            var index = Random.Range(0, foodIcons.Count);
            block.inventory.Add(foodIcons[index]);
            foodIcons.RemoveAt(index);
            
            list.Add(block);
        }

        var minAmount = foodIcons.Count-2*config.characterAmount;
        while (!foodIcons.IsEmpty() && foodIcons.Count>minAmount)
        {
            var block = list[Random.Range(0, list.Count)];
            if (block.inventory.Count < 3)
            {
                var index = Random.Range(0, foodIcons.Count);
                block.inventory.Add(foodIcons[index]);
                foodIcons.RemoveAt(index);
            }

        }


        return list;
    }

    public void UpdateByDeltaTimer(float delta)
    {
        for (var i = 0; i < _waitBlockList.Count; i++)
        {
            var block = _waitBlockList[i];
            var place = new Place(0, block.position);
            if (TryCreateCharacter(place, block))
            {
                _gameModel.CharacterModel.CurrentAmount++;
                _waitBlockList[i] = null;
            }
            else
            {
                var index = Random.Range(0, _config.characterPlaceAmount);
                place = new Place(0, index);
                block.position = index;
                if (TryCreateCharacter(place, block))
                {
                    _gameModel.CharacterModel.CurrentAmount++;
                    _waitBlockList[i] = null;
                }
            }
        }

        _waitBlockList.RemoveAll(b => b == null);

        while (_currentIndex < _blockList.Count && _blockList[_currentIndex].createdTime <= _gameModel.GameTime)
        {
            var block = _blockList[_currentIndex];
            var place = new Place(0, block.position);

            if (TryCreateCharacter(place, block))
            {
                _gameModel.CharacterModel.CurrentAmount++;
            }
            else
            {
                _waitBlockList.Add(block);
            }

            _currentIndex++;
        }
    }

    private bool TryCreateCharacter(Place place, CharacterBlockConfig block)
    {
        if (!_gameModel.ContainsKey(place) || _gameModel.GetItemModelByPlace(place).Type != ItemType.Character)
        {
            var characterBlock = Factory.CreateCharacterBlock(block, _uiCharacterPlace[block.position]);
            GamedBind.BindPlaceWithGameObject(place, characterBlock.Model.GameObjectModel);
            GamedBind.BindPlaceWithItemModel(place, characterBlock.Model.ItemModel);

            Main.Execute(ModifyItemType.Next, place, null);
            return true;
        }

        return false;
    }
}