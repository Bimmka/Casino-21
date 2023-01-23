using Features.Services.Leaderboard;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Leaderboard.Scripts
{
  public class LeaderboardElement : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI positionText;
    
    public void Initialize(LeaderboardUser user)
    {
      nicknameText.text = user.Name;
      pointsText.text = user.Points.ToString();
      if (user.Position > 0)
        positionText.text = user.Position.ToString();
      else
        positionText.text = "";
    }
  }
}