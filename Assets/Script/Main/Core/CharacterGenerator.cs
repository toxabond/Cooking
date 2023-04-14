using System;
using System.Collections.Generic;
using ModestTree;
using Script.Core.Character;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterGenerator : ICharacterGenerator
{
    [Inject] private IMainController _main;
    [Inject] private GamedBind _gamedBind;
    [Inject] private IGameFactory _factory;

    private List<CharacterBlockConfig> _blockList;
    private IReadOnlyList<Transform> _uiCharacterPlace;

    private int _currentIndex;

    private GameModel _gameModel;

    public void Init(LevelConfig levelConfig, IReadOnlyList<Transform> uiCharacterPlace, GameModel gameModel)
    {
        _currentIndex = 0;

        _gameModel = gameModel;

        _uiCharacterPlace = uiCharacterPlace;
        _blockList = CreateBlocks(levelConfig);
        _blockList.Sort((a, b) => a.createdTime - b.createdTime);
    }

    private void AddRewardFood(List<RewardFoodType> foodList, RewardFoodType type, int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            foodList.Add(type);
        }
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
        AddRewardFood(foodIcons, RewardFoodType.Glass, config.glassAmount);
        AddRewardFood(foodIcons, RewardFoodType.Burger, config.burgerAmount);
        AddRewardFood(foodIcons, RewardFoodType.Hotdog, config.hotdogAmount);
        AddRewardFood(foodIcons, RewardFoodType.FrenchFries, config.frenchFriesAmount);

        var list = new List<CharacterBlockConfig>();

        for (var i = 0; i < config.characterAmount; i++)
        {
            var block = new CharacterBlockConfig
            {
                createdTime = 2 + 10 * i,
                type = (CharacterType)(Random.Range(0, 4)),
                position = Random.Range(0, levelConfig.characterPlaceAmount),
                inventory = new List<RewardFoodType>()
            };

            var index = Random.Range(0, foodIcons.Count);
            block.inventory.Add(foodIcons[index]);
            foodIcons.RemoveAt(index);

            list.Add(block);
        }

        var minAmount = foodIcons.Count - 2 * config.characterAmount;
        while (!foodIcons.IsEmpty() && foodIcons.Count > minAmount)
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


    public bool TryCreateCharacter(Place place, CharacterBlockConfig block)
    {
        if (!_gameModel.ContainsKey(place) || _gameModel.GetItemModelByPlace(place).Type != ItemType.Character)
        {
            var model = _factory.CreateCharacterBlock(block, _uiCharacterPlace[block.position]);
            _gamedBind.BindPlaceWithGameObject(place, model.GameObjectModel);
            _gamedBind.BindPlaceWithItemModel(place, model.ItemModel);

            _main.Execute(ModifyItemType.Next, place, null);
            return true;
        }

        return false;
    }

    public bool HasReadyCharacter => _currentIndex < _blockList.Count &&
                                     _blockList[_currentIndex].createdTime <= _gameModel.GameTime;

    public CharacterBlockConfig Dequeue()
    {
        var block = _blockList[_currentIndex];
        _currentIndex++;
        return block;
    }
}