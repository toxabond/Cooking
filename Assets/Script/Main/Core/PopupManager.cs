using System;
using UnityEngine;

public class PopupManager : MonoBehaviour, IPopupManager
{
    [SerializeField] private WelcomePopup welcomePopup;
    [SerializeField] private BasePopup winPopup;
    [SerializeField] private BasePopup gameOverPopup;
    private IGameEvents _gameEvents;

    public event Action StartGameEvent = delegate { };
    public event Action RestartGameEvent = delegate { };
    public void Init(IGameEvents gameEvents)
    {
        _gameEvents = gameEvents;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        VisibleAll(false);
        welcomePopup.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _gameEvents.ReadyGameEvent += OnReadyGameEvent;
        _gameEvents.GameOverEvent += OnGameOverEvent;
        _gameEvents.WinGameEvent += OnWinGameEvent;

        welcomePopup.ClickEvent += OnStartGame;
        winPopup.ClickEvent += OnStartGame;
        gameOverPopup.ClickEvent += OnStartGame;
    }


    private void OnDisable()
    {
        _gameEvents.ReadyGameEvent -= OnReadyGameEvent;
        _gameEvents.GameOverEvent -= OnGameOverEvent;
        _gameEvents.WinGameEvent -= OnWinGameEvent;

        welcomePopup.ClickEvent -= OnStartGame;
        winPopup.ClickEvent -= OnStartGame;
        gameOverPopup.ClickEvent -= OnStartGame;
    }

    private void OnReadyGameEvent()
    {
        welcomePopup.button.gameObject.SetActive(true);
    }

    private void OnStartGame()
    {
        StartGameEvent();
    }

    private void VisibleAll(bool visible)
    {
        welcomePopup.gameObject.SetActive(visible);
        winPopup.gameObject.SetActive(visible);
        gameOverPopup.gameObject.SetActive(visible);
    }

    private void OnWinGameEvent()
    {
        VisibleAll(false);
        winPopup.gameObject.SetActive(true);
    }

    private void OnGameOverEvent()
    {
        VisibleAll(false);
        gameOverPopup.gameObject.SetActive(true);
    }
}