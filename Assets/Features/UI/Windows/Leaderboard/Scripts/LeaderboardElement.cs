using Features.Services.Leaderboard;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Leaderboard.Scripts
{
  public class LeaderboardElement : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI pointsText;
    
    public void Initialize(LeaderboardUser user)
    {
      nicknameText.text = user.Name;
      pointsText.text = user.Points.ToString();
    }
  }
}