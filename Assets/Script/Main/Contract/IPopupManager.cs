using System;

public interface IPopupManager
{
    event Action StartGameEvent;
    event Action RestartGameEvent;

    void Init(IGameEvents gameEvents);
}