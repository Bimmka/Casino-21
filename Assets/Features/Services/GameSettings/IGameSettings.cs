namespace Features.Services.GameSettings
{
  public interface IGameSettings
  {
    GameDifficultType DifficultType { get; }
    int CurrentBet { get; }
    void SetType(GameDifficultType type);
    void InitializeBet(int bet);
    float CurrentCoefficient();
  }
}