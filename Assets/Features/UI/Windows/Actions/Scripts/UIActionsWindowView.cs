using UnityEngine;

namespace Features.UI.Windows.Actions.Scripts
{
  public class UIActionsWindowView : MonoBehaviour
  {
    [SerializeField] private GameObject takeButtonParent;
    [SerializeField] private GameObject checkButtonParent;

    public void ShowButtons() => 
      ChangeButtonsState(true, true);

    public void HideButtons() => 
      ChangeButtonsState(false, false);

    private void ChangeButtonsState(bool isTakeActive, bool isCheckActive)
    {
      takeButtonParent.SetActive(isTakeActive);
      checkButtonParent.SetActive(isCheckActive);
    }
  }
}