public class Timer : ITimer
{
    public float Duration { get; set; }
    public float Current { get; set; }

    public Timer(float duration, float current = 0)
    {
        Duration = duration;
        Current = current;
    }
}

public interface ITimer
{
    //sec
    float Duration { get;}
    //sec
    float Current { get;}
}