using TMPro;
using UnityEngine;

namespace Features.Level.Scripts.Info
{
  public class LevelInfoDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI userPointsText;
    [SerializeField] private TextMeshProUGUI dealerPointsText;
    [SerializeField] private BetDisplayer betDisplayer;
    [TextArea]
    [SerializeField] private string userPointsTextFormat = "Player: {0}";
    [TextArea]
    [SerializeField] private string dealerPointsTextFormat = "Dealer: {0}";

    public void DisplayUserPoints(int points) => 
      userPointsText.text = string.Format(userPointsTextFormat, points);

    public void DisplayDealerPoints(int points) => 
      dealerPointsText.text = string.Format(dealerPointsTextFormat, points);

    public void DisplayBet(int bet)
    {
      betDisplayer.Display(bet);
    }

    public void Reset()
    {
      userPointsText.text = string.Format(userPointsTextFormat, "");
      dealerPointsText.text = string.Format(dealerPointsTextFormat, "");
      betDisplayer.Hide();
    }
  }
}