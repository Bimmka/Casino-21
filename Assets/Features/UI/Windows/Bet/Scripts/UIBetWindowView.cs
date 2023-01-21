using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Bet.Scripts
{
  public class UIBetWindowView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI betText;
    public void DisplayBet(in float bet)
    {
      betText.text = bet.ToString();
    }
  }
}