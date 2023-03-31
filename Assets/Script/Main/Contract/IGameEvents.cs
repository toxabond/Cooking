using System;

public interface IGameEvents
{
    event Action ReadyGameEvent;
    event Action GameOverEvent;
    event Action WinGameEvent;
}