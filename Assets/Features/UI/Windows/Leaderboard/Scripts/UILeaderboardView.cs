using Features.Services.Leaderboard;
using UnityEngine;

namespace Features.UI.Windows.Leaderboard.Scripts
{
  public class UILeaderboardView : MonoBehaviour
  {
    [SerializeField] private GameObject errorParent;
    [SerializeField] private LeaderboardElement userElement;

    public void DisplayError() => 
      ChangeErrorView(true);

    public void HideError() => 
      ChangeErrorView(false);

    public void DisplayUser(LeaderboardUser user)
    {
      userElement.Initialize(user);
      userElement.gameObject.SetActive(true);
    }

    private void ChangeErrorView(bool isEnable) => 
      errorParent.SetActive(isEnable);
  }
}