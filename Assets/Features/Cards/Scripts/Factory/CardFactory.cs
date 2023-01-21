using Features.Cards.Scripts.Data.Cards;
using Features.Cards.Scripts.Element;
using Features.Services.Assets;
using UnityEngine;

namespace Features.Cards.Scripts.Factory
{
  public class CardFactory
  {
    private readonly IAssetProvider assetProvider;

    public CardFactory(IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
    }
    
    public Card Create(CardSettings settings, int spawnIndex, Transform transform, Vector3 at, Quaternion rotation)
    {
      string id = $"{settings.Cost}:{spawnIndex}";
      Card card = assetProvider.Instantiate(settings.Prefab, at, rotation, transform);
      card.Initialize(id, settings.Cost);
      return card;
    }
  }
}