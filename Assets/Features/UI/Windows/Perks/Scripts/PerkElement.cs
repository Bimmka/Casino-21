using System;
using Features.Perks.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Perks.Scripts
{
  [RequireComponent(typeof(PerkElementView))]
  public class PerkElement : MonoBehaviour
  {
    [SerializeField] private PerkElementView view;
    [SerializeField] private Button clickButton;
    
    private PerkSettings settings;
    public PerkType Type => settings.Type;
    
    public event Action<PerkElement> Clicked;

    private void Awake()
    {
      clickButton.onClick.AddListener(NotifyAboutClick);
    }

    public void Initialize(PerkSettings settings, bool isOpen)
    {
      this.settings = settings;
      view.Display(settings, isOpen);
    }

    public void Cleanup()
    {
      clickButton.onClick.RemoveListener(NotifyAboutClick);
    }

    public void Select() => 
      view.SetSelectView();

    public void Deselect() => 
      view.SetDeselectView();

    private void NotifyAboutClick() => 
      Clicked?.Invoke(this);
  }
}