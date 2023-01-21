using System;
using Features.Cards.Scripts.Data.Move;
using Features.Cards.Scripts.Move;
using TMPro;
using UnityEngine;

namespace Features.Cards.Scripts.Element
{
  public class Card : MonoBehaviour
  {
    [SerializeField] private CardMoveSettings moveSettings;
    [SerializeField] private TextMeshProUGUI costText;

    private CardMover mover;
    public string ID { get; private set; }
    public int Cost { get; private set; }

    private void Awake()
    {
      mover = new CardMover(moveSettings, transform);
    }

    public void Initialize(string id, int cost)
    {
      ID = id;
      Cost = cost;
      costText.text = cost.ToString();
    }

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);


    public void SetPosition(Vector3 at) =>
      mover.SetPosition(at);

    public void SetRotation(Quaternion rotation) =>
      mover.SetRotation(rotation);

    public void Move(Vector3 at, Quaternion rotation, Action callback) => 
      mover.Move(at, rotation, callback);
  }
}