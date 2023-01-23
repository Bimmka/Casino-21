using System;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public abstract class PerkStrategy
  {
    private readonly PerkSettings settings;

    protected PerkStrategy(PerkSettings settings)
    {
      this.settings = settings;
    }

    public abstract bool IsCanBeUsed();
    public abstract void Use(Action callback);
  }
}