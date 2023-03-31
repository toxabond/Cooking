public class LevelModel : ITickableUpdate
{
    public readonly Timer Timer;

    public LevelModel(Timer timer)
    {
        Timer = timer;
    }

    public void UpdateByDeltaTimer(float delta)
    {
            
        if (Timer.Current < Timer.Duration)
        {
            Timer.Current += delta;
            if (Timer.Current > Timer.Duration)
            {
                Timer.Current = Timer.Duration;
            }
        }
    }
}