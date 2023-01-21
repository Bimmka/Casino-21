namespace Features.Services.GameSettings
{
  public class GameSettingsService : IGameSettings
  {
    public GameDifficultType DifficultType { get; private set; }
    public int CurrentBet { get; private set; }

    public void SetType(GameDifficultType type)
    {
      DifficultType = type;
    }

    public void InitializeBet(int bet)
    {
      CurrentBet = bet;
    }
  }
}