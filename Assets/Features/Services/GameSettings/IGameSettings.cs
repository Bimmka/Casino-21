namespace Features.Services.GameSettings
{
  public interface IGameSettings
  {
    GameDifficultType DifficultType { get; }
    void SetType(GameDifficultType type);
  }
}