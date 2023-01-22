using System.Collections.Generic;
using Features.Services.GameSettings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Cards.Scripts.Element
{
  public class CardView : MonoBehaviour
  {
    [SerializeField] private Renderer cardRenderer;

    private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");
    private static readonly int Cutoff = Shader.PropertyToID("_Cutoff");
    
    private Dictionary<GameDifficultType, Texture2D[]> hideCostMasks;
    private float alphaCutoff;
    private MaterialPropertyBlock propBlock;

    private void Awake()
    {
      propBlock = new MaterialPropertyBlock();
    }

    public void Initialize(Dictionary<GameDifficultType, Texture2D[]> hideCostMasks, float alphaCutoff,
      GameDifficultType difficultType)
    {
      this.alphaCutoff = alphaCutoff;
      this.hideCostMasks = hideCostMasks;
      cardRenderer.GetPropertyBlock(propBlock);
      propBlock.SetTexture(MaskTex, RandomMasks(difficultType));
      propBlock.SetFloat(Cutoff, 0f);
      cardRenderer.SetPropertyBlock(propBlock);
    }

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);

    public void DisplayCost()
    {
      cardRenderer.GetPropertyBlock(propBlock);
      propBlock.SetFloat(Cutoff, 0f);
      cardRenderer.SetPropertyBlock(propBlock);
    }

    public void HideCost()
    {
      cardRenderer.GetPropertyBlock(propBlock);
      propBlock.SetFloat(Cutoff, alphaCutoff);
      cardRenderer.SetPropertyBlock(propBlock);
    }

    private Texture2D RandomMasks(GameDifficultType difficultType) => 
      hideCostMasks[difficultType][Random.Range(0, hideCostMasks[difficultType].Length)];
  }
}