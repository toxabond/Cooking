using Zenject;

public class LevelCalculator : ITickableUpdate, ITickableCheck
{
    [Inject] private GameModel _gameModel;

    public void UpdateByDeltaTimer(float delta)
    {
        var timer = _gameModel.Level.Timer;
        if (timer.Current < timer.Duration)
        {
            timer.Current += delta;
            if (timer.Current > timer.Duration)
            {
                timer.Current = timer.Duration;
            }
        }
    }
    
    public void Check()
    {
        CheckGameOverByTime();
    }
    
    private void CheckGameOverByTime()
    {
        if (_gameModel.GameTime-0.5 > _gameModel.Level.Timer.Current)
        {
            _gameModel.GameState = GameState.GameOver;
        }
    }

}