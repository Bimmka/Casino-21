using System;
using System.Collections.Generic;
using Features.Perks.Data;

namespace Features.User.Data
{
  public class UserOpenPerks
  {
    private readonly UserStatistics userStatistics;
    private readonly Dictionary<PerkType, PerkContainer> perks;

    public UserOpenPerks(PerksSettingsContainer perksContainer, UserStatistics userStatistics)
    {
      this.userStatistics = userStatistics;
      perks = new Dictionary<PerkType, PerkContainer>(perksContainer.Perks.Length);
      for (int i = 0; i < perksContainer.Perks.Length; i++)
      {
        perks.Add(perksContainer.Perks[i].Type, new PerkContainer(false, perksContainer.Perks[i]));
      }
    }

    public void Restore(List<PerkType> openPerks)
    {
      for (int i = 0; i < openPerks.Count; i++)
      {
        perks[openPerks[i]].SetIsOpen();
      }
    }

    public bool IsOpen(PerkType perkType) => 
      perks[perkType].IsOpen;

    public List<PerkType> OpenPerks()
    {
      List<PerkType> openPerks = new List<PerkType>();
      foreach (KeyValuePair<PerkType,PerkContainer> perk in perks)
      {
        if (perk.Value.IsOpen)
          openPerks.Add(perk.Key);
      }

      return openPerks;
    }

    public void RefreshPerksOpen()
    {
      foreach (KeyValuePair<PerkType,PerkContainer> perk in perks)
      {
        if (perk.Value.IsOpen == false && IsOpen(perk.Value.Settings.UnlockCondition))
          perk.Value.SetIsOpen();
      }
    }

    private bool IsOpen(UnlockCondition unlockCondition)
    {
      switch (unlockCondition.Type)
      {
        case UnlockConditionType.FromStart:
          return true;
        case UnlockConditionType.WinGames:
          return userStatistics.Statistic(StatisticsType.TotalWins) >= unlockCondition.Count;
        case UnlockConditionType.TotalGames:
          return userStatistics.Statistic(StatisticsType.TotalGames) >= unlockCondition.Count;
        case UnlockConditionType.WinStreak:
          return userStatistics.Statistic(StatisticsType.WinStreak) >= unlockCondition.Count;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private class PerkContainer
    {
      public bool IsOpen;
      public readonly PerkSettings Settings;

      public PerkContainer(bool isOpen, PerkSettings settings)
      {
        IsOpen = isOpen;
        Settings = settings;
      }

      public void SetIsOpen() => 
        IsOpen = true;
    }
  }
}