using Features.Perks.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Actions.Scripts
{
  public class UIGamePerk : MonoBehaviour
  {
    [SerializeField] private GameObject lockedView;
    [SerializeField] private GameObject unlockedView;
    [SerializeField] private Button useButton;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI priceText;
    public void SetLockedView() => 
      ChangeViewState(true);

    public void SetUnlockedView() => 
      ChangeViewState(false);

    private void ChangeViewState(bool isLocked)
    {
      lockedView.SetActive(isLocked);
      unlockedView.SetActive(!isLocked);
      useButton.enabled = !isLocked;
    }

    public void Display(PerkSettings perk)
    {
      icon.sprite = perk.Icon;
      priceText.text = perk.UseCost.ToString();
    }
  }
}