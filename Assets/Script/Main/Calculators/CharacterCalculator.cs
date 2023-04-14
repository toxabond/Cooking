using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterCalculator : ICharacterCalculator, ITickableUpdate, ITickableCheck
{
    [Inject] private ICharacterGenerator _characterGenerator;
    [Inject] private GameModel _gameModel;
    private List<CharacterBlockConfig> _waitBlockList;
    private LevelConfig _config;

    public void Init(LevelConfig levelConfig)
    {
        _config = levelConfig;
        _waitBlockList = new List<CharacterBlockConfig>();
    }

    public void UpdateByDeltaTimer(float delta)
    {
        TryCreateWaitCharacter();
        TryCreateReadyCharacter();
    }

    public void Check()
    {
        CheckWinGame(_gameModel.Models);
    }

    private void CheckWinGame(Dictionary<int, Model> models)
    {
        var gameModelCharacterModel = _gameModel.CharacterModel;
        if (_gameModel.GameState == GameState.Playing &&
            gameModelCharacterModel.Amount == gameModelCharacterModel.CurrentAmount)
        {
            var isFindActiveCharacter = false;
            foreach (var pair in models)
            {
                if (pair.Value.ItemModel.FromItemType == ItemType.Character ||
                    pair.Value.ItemModel.ToItemType == ItemType.Character)
                {
                    isFindActiveCharacter = true;
                }
            }

            if (!isFindActiveCharacter)
            {
                _gameModel.GameState = GameState.Win;
            }
        }
    }

    private void TryCreateReadyCharacter()
    {
        while (_characterGenerator.HasReadyCharacter)
        {
            var block = _characterGenerator.Dequeue();
            var place = new Place(0, block.position);

            if (_characterGenerator.TryCreateCharacter(place, block))
            {
                _gameModel.CharacterModel.CurrentAmount++;
            }
            else
            {
                _waitBlockList.Add(block);
            }
        }
    }

    private void TryCreateWaitCharacter()
    {
        for (var i = 0; i < _waitBlockList.Count; i++)
        {
            var block = _waitBlockList[i];
            var place = new Place(0, block.position);
            if (_characterGenerator.TryCreateCharacter(place, block))
            {
                _gameModel.CharacterModel.CurrentAmount++;
                _waitBlockList[i] = null;
            }
            else
            {
                var index = Random.Range(0, _config.characterPlaceAmount);
                place = new Place(0, index);
                block.position = index;
                if (_characterGenerator.TryCreateCharacter(place, block))
                {
                    _gameModel.CharacterModel.CurrentAmount++;
                    _waitBlockList[i] = null;
                }
            }
        }

        _waitBlockList.RemoveAll(b => b == null);
    }
}