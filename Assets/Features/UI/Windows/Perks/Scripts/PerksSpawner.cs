using Features.Perks.Data;
using Features.Services.Assets;
using UnityEngine;

namespace Features.UI.Windows.Perks.Scripts
{
  public class PerksSpawner
  {
    private readonly IAssetProvider assetProvider;
    private readonly Transform spawnParent;
    private readonly PerkElement prefab;

    public PerksSpawner(IAssetProvider assetProvider, Transform spawnParent, PerkElement prefab)
    {
      this.assetProvider = assetProvider;
      this.spawnParent = spawnParent;
      this.prefab = prefab;
    }

    public PerkElement Create(PerkSettings perkSettings, bool isOpen)
    {
      PerkElement element = assetProvider.Instantiate(prefab, spawnParent);
      element.Initialize(perkSettings, isOpen);
      return element;
    }
  }
}