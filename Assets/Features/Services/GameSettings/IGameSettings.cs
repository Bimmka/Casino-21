using Features.Perks.Data;

namespace Features.Services.GameSettings
{
  public interface IGameSettings
  {
    GameDifficultType DifficultType { get; }
    int CurrentBet { get; }
    PerkType PerkType { get; }
    void SetType(GameDifficultType type);
    void InitializeBet(int bet);
    float CurrentCoefficient();
    void AddPerk(PerkType type);
    void Reset();
  }
}