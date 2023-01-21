using TMPro;
using UnityEngine;

namespace Features.Level.Scripts.Info
{
  public class LevelInfoDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI userPointsText;
    [SerializeField] private TextMeshProUGUI dealerPointsText;
    [SerializeField] private BetDisplayer betDisplayer;

    public void DisplayUserPoints(int points) => 
      userPointsText.text = points.ToString();

    public void DisplayDealerPoints(int points) => 
      dealerPointsText.text = points.ToString();

    public void DisplayBet(int bet)
    {
      betDisplayer.Display(bet);
    }

    public void Reset()
    {
      userPointsText.text = "0";
      dealerPointsText.text = "0";
      betDisplayer.Hide();
    }
  }
}