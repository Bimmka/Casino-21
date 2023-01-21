namespace Features.Services.GameSettings
{
  public class GameSettingsService : IGameSettings
  {
    public GameDifficultType DifficultType { get; private set; }

    public void SetType(GameDifficultType type)
    {
      DifficultType = type;
    }
  }
}