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

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);
    

    public void SetPosition(Vector3 at)
    {
      transform.position = at;
    }

    public void Move(Vector3 at, Quaternion rotation)
    {
      transform.position = at;
      transform.rotation = rotation;
    }

    public void SetRotation(Quaternion rotation)
    {
      transform.rotation = rotation;
    }
  }
}