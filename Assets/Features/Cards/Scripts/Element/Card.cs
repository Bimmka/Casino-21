using TMPro;
using UnityEngine;

namespace Features.Cards.Scripts.Element
{
  public class Card : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI costText;
    public string ID { get; private set; }
    public int Cost { get; private set; }

    public void Initialize(string id, int cost)
    {
      ID = id;
      Cost = cost;
      costText.text = cost.ToString();
    }
  }
}