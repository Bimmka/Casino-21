using DG.Tweening;
using Features.User.Data;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.HUD.Scripts
{
  public class PointsDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private float animationDuration;

    private int currentPoints;
    private Tweener displayTweener;
    
    private UserPointsData userPoints;

    public void Construct(UserPointsData userPoints)
    {
      this.userPoints = userPoints;
      currentPoints = userPoints.CurrentPoints;
      Display(userPoints.CurrentPoints);  
      userPoints.Changed += OnPointsChange;
    }

    public void Cleanup()
    {
      userPoints.Changed -= OnPointsChange;
    }

    private void OnPointsChange(int newPoints)
    {
      if (displayTweener.IsActive())
        displayTweener.Kill();

      displayTweener = DOTween.To(SetPoints, currentPoints, newPoints, animationDuration)
        .OnComplete(OnAnimationEnd);
    }

    private void SetPoints(float count)
    {
      currentPoints = (int) count;
      Display(currentPoints);
    }

    private void Display(int points) => 
      pointsText.text = points.ToString();

    private void OnAnimationEnd() => 
      displayTweener = null;
  }
}