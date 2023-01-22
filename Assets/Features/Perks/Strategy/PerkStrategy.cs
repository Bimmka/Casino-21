using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public abstract class PerkStrategy
  {
    private readonly PerkSettings settings;

    public PerkType Type => settings.Type;
    public int Cost => settings.UseCost;

    protected PerkStrategy(PerkSettings settings)
    {
      this.settings = settings;
    }

    public abstract bool IsCanBeUsed();
    public abstract void Use();
  }
}