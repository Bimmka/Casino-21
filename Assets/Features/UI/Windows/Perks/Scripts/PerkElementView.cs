using Features.Perks.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Perks.Scripts
{
  public class PerkElementView : MonoBehaviour
  {
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI usePriceText;
    [SerializeField] private TextMeshProUGUI unlockConditionText;
    [SerializeField] private GameObject openParent;
    [SerializeField] private GameObject closeParent;

    public void Display(PerkSettings settings, bool isOpen)
    {
      icon.sprite = settings.Icon;
      nameText.text = settings.Name;
      descriptionText.text = settings.Description;
      usePriceText.text = settings.UseCost.ToString();
      unlockConditionText.text = string.Format(settings.UnlockCondition.TextFormat, settings.UnlockCondition.Count);
      openParent.SetActive(isOpen);
      closeParent.SetActive(!isOpen);
    }
  }
}