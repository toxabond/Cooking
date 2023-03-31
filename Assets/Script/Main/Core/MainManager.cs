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

    [SerializeField] private List<Zone> zoneCollection;
    [SerializeField] private UICollection uiCollection;
    [SerializeField] private GameObject zoneHolder;
    [SerializeField] private PopupManager popupManager;


    [Inject] private Loader _loader;
    [Inject] private CharacterGenerator _generator;
    [Inject] private LevelSetting _level;
    [Inject] private Zone00Initializer _initializer;
    [Inject] private GameModel _gameModel;
    
    private Zone _zone;
    private LevelConfig _levelConfig;

    private async void Start()
    {
        if (_level.isUseLocalLevel)
        {
            _levelConfig = _level.levelConfig;
            ReadyGameEvent();
        }else{
            _levelConfig = JsonUtility.FromJson<LevelConfig>(await _loader.LoadDataByTask(_level.externalUrl));
            ReadyGameEvent();
        }
    }

    private void OnEnable()
    {
        popupManager.StartGameEvent += OnStartAmeEvent;
        popupManager.RestartGameEvent += OnRestartGameEvent;
    }

    private void OnDisable()
    {
        popupManager.StartGameEvent -= OnStartAmeEvent;
        popupManager.RestartGameEvent -= OnRestartGameEvent;
    }

    private void OnStartAmeEvent()
    {
        if (_zone != null)
        {
            Destroy(_zone.gameObject);
        }

        _zone = Instantiate(zoneCollection[_levelConfig.idZone], zoneHolder.transform);
        var uiElements = _zone.gameObject.GetComponentInChildren<IUIElements>();

        var levelModel = new LevelModel(new Timer(_levelConfig.levelTime));
        var characterModel = new CharacterModel(_levelConfig.CharacterAmount);
        _gameModel.Init(levelModel, characterModel);

        uiCollection.CharacterProgress.Bind(_gameModel);
        uiCollection.GameTimerProgress.Bind(_gameModel);
        
        _initializer.Init(_levelConfig, _gameModel, uiElements);
    }

    private void OnRestartGameEvent()
    {
        OnStartAmeEvent();
    }

    private void Update()
    {
        try
        {
            if (_gameModel.GameState != GameState.Playing)
            {
                return;
            }

            _gameModel.UpdateByDeltaTimer(Time.deltaTime);
            _generator.UpdateByDeltaTimer(Time.deltaTime);

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