using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerProgress : MonoBehaviour
{
    [SerializeField] private Image progress;
    [SerializeField] private TextMeshProUGUI text;
    private GameModel _gameModel;

    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    private void Update()
    {
        if (_gameModel == null || _gameModel.GameState != GameState.Playing)
        {
            return;
        }

        progress.fillAmount = 1 - _gameModel.Level.Timer.Current / _gameModel.Level.Timer.Duration;
        var seconds = _gameModel.Level.Timer.Duration - _gameModel.Level.Timer.Current;
        var timeSpan = new TimeSpan(0, 0, (int)seconds);
        text.text = $"{timeSpan.Minutes.ToString()}:{timeSpan.Seconds.ToString()}";
    }
}