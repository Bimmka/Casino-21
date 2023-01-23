using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Bet.Scripts
{
  public class UIBetWindowView : MonoBehaviour
  {
    [SerializeField] private TMP_InputField betText;
    public void DisplayBet(float bet)
    {
      betText.text = bet.ToString();
    }
  }
}