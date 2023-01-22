using System;
using Features.Cards.Scripts.Data.Cards;
using Features.Cards.Scripts.Data.Move;
using Features.Cards.Scripts.Move;
using Features.Services.GameSettings;
using TMPro;
using UnityEngine;

namespace Features.Cards.Scripts.Element
{
  [RequireComponent(typeof(CardView))]
  public class Card : MonoBehaviour
  {
    [SerializeField] private CardView view;
    [SerializeField] private CardMoveSettings moveSettings;

    private CardMover mover;
    private CardSettings settings;
    public string ID { get; private set; }
    public int Cost { get; private set; }

    private void Awake()
    {
      mover = new CardMover(moveSettings, transform);
    }

    public void Initialize(string id, int cost, CardSettings settings, GameDifficultType difficultType)
    {
      this.settings = settings;
      ID = id;
      Cost = cost;
      view.Initialize(settings.DifficultMasks, settings.AlphaCutoff, difficultType);
    }

    public void Show() => 
      view.Show();

    public void Hide() => 
      view.Hide();

    public void HideCost() => 
      view.HideCost();

    public void DisplayCost() => 
      view.DisplayCost();

    public void SetPosition(Vector3 at) =>
      mover.SetPosition(at);

    public void SetRotation(Quaternion rotation) =>
      mover.SetRotation(rotation);

    public void Move(Vector3 at, Quaternion rotation, Action callback) => 
      mover.Move(at, rotation, callback);
  }
}