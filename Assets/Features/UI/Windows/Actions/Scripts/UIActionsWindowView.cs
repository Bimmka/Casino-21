using UnityEngine;

namespace Features.UI.Windows.Actions.Scripts
{
  public class UIActionsWindowView : MonoBehaviour
  {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject takeButtonParent;
    [SerializeField] private GameObject checkButtonParent;

    public void Hide() => 
      ChangeVisibleState(0, false);

    public void Show() => 
      ChangeVisibleState(1, true);


    public void ShowButtons() => 
      ChangeButtonsState(true, true);

    public void HideButtons() => 
      ChangeButtonsState(false, false);

    private void ChangeButtonsState(bool isTakeActive, bool isCheckActive)
    {
      takeButtonParent.SetActive(isTakeActive);
      checkButtonParent.SetActive(isCheckActive);
    }

    private void ChangeVisibleState(float alpha, bool isVisible)
    {
      canvasGroup.alpha = alpha;
      canvasGroup.interactable = isVisible;
      canvasGroup.blocksRaycasts = isVisible;
    }
  }
}