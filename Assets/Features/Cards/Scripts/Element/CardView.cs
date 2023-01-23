using System.Collections.Generic;
using Features.Services.GameSettings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Cards.Scripts.Element
{
  public class CardView : MonoBehaviour
  {
    [SerializeField] private Renderer cardPlane;

    private static readonly int MaskTex = Shader.PropertyToID("_Mask");
    private static readonly int Cutoff = Shader.PropertyToID("_Alpha_Cutoff");
    
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
      if (IsHaveMasks(difficultType))
      {
        cardPlane.GetPropertyBlock(propBlock);
        propBlock.SetTexture(MaskTex, RandomMasks(difficultType));
        propBlock.SetFloat(Cutoff, 1.1f);
        cardPlane.SetPropertyBlock(propBlock);
      }
      else 
        cardPlane.gameObject.SetActive(false);
      
      
      if (difficultType == GameDifficultType.Easy)
        cardPlane.gameObject.SetActive(false);
    }

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);

    public void DisplayCost()
    {
      cardPlane.GetPropertyBlock(propBlock);
      propBlock.SetFloat(Cutoff, 1.1f);
      cardPlane.SetPropertyBlock(propBlock);
    }

    public void HideCost()
    {
      cardPlane.GetPropertyBlock(propBlock);
      propBlock.SetFloat(Cutoff, alphaCutoff);
      cardPlane.SetPropertyBlock(propBlock);
    }
    
    private bool IsHaveMasks(GameDifficultType difficultType) => 
      hideCostMasks.ContainsKey(difficultType) && hideCostMasks[difficultType].Length > 0;

    private Texture2D RandomMasks(GameDifficultType difficultType) => 
      hideCostMasks[difficultType][Random.Range(0, hideCostMasks[difficultType].Length)];
  }
}