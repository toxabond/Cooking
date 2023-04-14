using Script.Core.Interface;

public interface IZoneInitializer
{
    void Init(LevelConfig levelConfig, GameModel gameModel, IUIElements uiElements);
}