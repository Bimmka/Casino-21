using TMPro;
using UnityEngine;

namespace Features.Level.Info
{
  public class BetDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI betText;
    public void Display(int bet)
    {
      betText.text = bet.ToString();
    }

    public void Hide()
    {
      betText.text = "0";
    }
  }
}