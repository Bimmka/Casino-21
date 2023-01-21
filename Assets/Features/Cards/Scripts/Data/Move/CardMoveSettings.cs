using UnityEngine;

namespace Features.Cards.Scripts.Data.Move
{
  [CreateAssetMenu(fileName = "CardMoveSettings", menuName = "StaticData/Cards/Create Card Move Settings", order = 52)]
  public class CardMoveSettings : ScriptableObject
  {
    public float MoveDuration = 1f;
    public float DelayBeforeRotate = 0.5f;
    public float RotateDuration = 0.5f;
    public float YMoveOffset = 0.1f;
    public float XMoveOffset = 0.1f;
  }
}