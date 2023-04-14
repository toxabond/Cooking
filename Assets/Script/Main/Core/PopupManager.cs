using System;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private WelcomePopup welcomePopup;
    [SerializeField] private BasePopup winPopup;
    [SerializeField] private BasePopup gameOverPopup;
    [SerializeField] private MainManager mainManager;
    private IGameEvents _mainManager;

    public event Action StartGameEvent = delegate { };
    public event Action RestartGameEvent = delegate { };

    private void Awake()
    {
        _mainManager = mainManager;
        mainManager = null;
    }

    private void Start()
    {
        VisibleAll(false);
        welcomePopup.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _mainManager.ReadyGameEvent += OnReadyGameEvent;
        _mainManager.GameOverEvent += OnGameOverEvent;
        _mainManager.WinGameEvent += OnWinGameEvent;

        welcomePopup.ClickEvent += OnStartGame;
        winPopup.ClickEvent += OnStartGame;
        gameOverPopup.ClickEvent += OnStartGame;
    }


    private void OnDisable()
    {
        _mainManager.ReadyGameEvent -= OnReadyGameEvent;
        _mainManager.GameOverEvent -= OnGameOverEvent;
        _mainManager.WinGameEvent -= OnWinGameEvent;

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