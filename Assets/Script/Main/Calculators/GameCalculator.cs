using Zenject;

public class GameCalculator : ITickableUpdate
{
    [Inject] private GameModel _gameModel;
    
    public void UpdateByDeltaTimer(float delta)
    {
        _gameModel.GameTime += delta;
        // Level.UpdateByDeltaTimer(delta);
        
    }
    
    

    

}