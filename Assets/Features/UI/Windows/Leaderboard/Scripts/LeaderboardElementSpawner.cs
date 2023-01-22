using System.Collections.Generic;
using Features.Services.Assets;
using Features.Services.Leaderboard;
using UnityEngine;

namespace Features.UI.Windows.Leaderboard.Scripts
{
  public class LeaderboardElementSpawner
  {
    private readonly IAssetProvider assetProvider;
    private readonly Transform spawnParent;
    private readonly LeaderboardElement prefab;

    public LeaderboardElementSpawner(IAssetProvider assetProvider, Transform spawnParent, LeaderboardElement prefab)
    {
      this.assetProvider = assetProvider;
      this.spawnParent = spawnParent;
      this.prefab = prefab;
    }

    public void Spawn(List<LeaderboardUser> users)
    {
      LeaderboardElement element;
      for (int i = 0; i < users.Count; i++)
      {
        element = assetProvider.Instantiate(prefab, spawnParent);
        element.Initialize(users[i]);
      }
    }
  }
}