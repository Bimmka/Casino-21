using Features.Perks.Data;
using UnityEngine;

namespace Features.UI.Windows.Actions.Scripts
{
  public class UIActionsWindowView : MonoBehaviour
  {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject takeButtonParent;
    [SerializeField] private GameObject checkButtonParent;
    [SerializeField] private UIGamePerk gamePerk;

    public void Hide() => 
      ChangeVisibleState(0, false);

    public void Show() => 
      ChangeVisibleState(1, true);

    public void HidePerk() => 
      gamePerk.gameObject.SetActive(false);

    public void ShowPerk() => 
      gamePerk.gameObject.SetActive(true);

    public void SetLockPerk() => 
      gamePerk.SetLockedView();
    public void SetUnlockPerk() => 
      gamePerk.SetUnlockedView();

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

    public void SetPerkView(PerkSettings perk)
    {
      gamePerk.Display(perk);
    }
  }
}