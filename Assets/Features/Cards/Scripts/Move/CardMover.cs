using System;
using DG.Tweening;
using Features.Cards.Scripts.Data.Move;
using UnityEngine;

namespace Features.Cards.Scripts.Move
{
  public class CardMover
  {
    private readonly CardMoveSettings moveSettings;
    private readonly Transform card;

    public CardMover(CardMoveSettings moveSettings, Transform card)
    {
      this.moveSettings = moveSettings;
      this.card = card;
    }

    public void Move(Vector3 at, Quaternion rotation, Action callback = null)
    {
      card.DOPath(MovePath(at), moveSettings.MoveDuration, PathType.CatmullRom)
        .SetEase(Ease.InOutSine)
        .OnComplete(() => OnEndMove(at, rotation, callback));
      
      Sequence sequence = DOTween.Sequence();
      sequence.AppendInterval(moveSettings.DelayBeforeRotate)
        .Append(card.DORotateQuaternion(rotation, moveSettings.RotateDuration));
    }

    public void SetPosition(Vector3 at) => 
      card.position = at;

    public void SetRotation(Quaternion rotation) => 
      card.rotation = rotation;

    private void OnEndMove(Vector3 at, Quaternion rotation, Action callback)
    {
      DOTween.Kill(card);
      SetPosition(at);
      SetRotation(rotation);
      callback?.Invoke();
    }

    private Vector3[] MovePath(Vector3 at)
    {
      Vector3[] path = new Vector3[2];
      path[0] = new Vector3(at.x - card.position.x, card.position.y + moveSettings.YMoveOffset, card.position.z - moveSettings.XMoveOffset);
      path[1] = at;
      return path;
    }
  }
}