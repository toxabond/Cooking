using System;
using System.Collections.Generic;
using Script.Core.Interface;
using UnityEngine;
using Zenject;

public class MainManager : MonoBehaviour, IGameEvents
{
    public event Action ReadyGameEvent = delegate { };
    public event Action GameOverEvent = delegate { };
    public event Action WinGameEvent = delegate { };

    [SerializeField] private UICollection uiCollection;
    [SerializeField] private GameObject zoneHolder;
    [SerializeField] private PopupManager popupManager;

    [Inject] private ILoader _loader;
    [Inject] private ZoneCollectionSetting _zoneCollection;
    [Inject] private LevelSetting _level;
    [Inject] private IZoneInitializer _initializer;
    [Inject] private GameModel _gameModel;

    [Inject] private List<ITickableUpdate> _tickableUpdateList;
    [Inject] private List<ITickableCheck> _tickableCheckList;

    private Zone _zone;
    private LevelConfig _levelConfig;

    private async void Start()
    {
        if (_level.isUseLocalLevel)
        {
            _levelConfig = _level.levelConfig;
        }
        else
        {
            _levelConfig = JsonUtility.FromJson<LevelConfig>(await _loader.LoadDataByTask(_level.externalUrl));
        }

        ReadyGameEvent();
    }

    private void OnEnable()
    {
        popupManager.StartGameEvent += OnStartGameEvent;
        popupManager.RestartGameEvent += OnRestartGameEvent;
    }

    private void OnDisable()
    {
        popupManager.StartGameEvent -= OnStartGameEvent;
        popupManager.RestartGameEvent -= OnRestartGameEvent;
    }

    private void OnStartGameEvent()
    {
        if (_zone != null)
        {
            Destroy(_zone.gameObject);
        }

        _zone = Instantiate(_zoneCollection.zoneCollection[_levelConfig.idZone], zoneHolder.transform);
        var uiElements = _zone.gameObject.GetComponentInChildren<IUIElements>();

        var levelModel = new LevelModel(new Timer(_levelConfig.levelTime));
        var characterModel = new CharacterModel(_levelConfig.CharacterAmount);
        _gameModel.Init(levelModel, characterModel);

        uiCollection.characterProgress.Bind(_gameModel);
        uiCollection.gameTimerProgress.Bind(_gameModel);

        _initializer.Init(_levelConfig, _gameModel, uiElements);
    }

    private void OnRestartGameEvent()
    {
        OnStartGameEvent();
    }

    private void Update()
    {
        try
        {
            if (_gameModel.GameState != GameState.Playing)
            {
                return;
            }

            _tickableUpdateList.ForEach(t => t.UpdateByDeltaTimer(Time.deltaTime));
            _tickableCheckList.ForEach(t => t.Check());

            if (_gameModel.GameState == GameState.GameOver)
            {
                GameOverEvent();
            }
            else if (_gameModel.GameState == GameState.Win)
            {
                WinGameEvent();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            _gameModel.GameState = GameState.Pause;
        }
    }
}