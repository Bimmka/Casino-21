using Features.Level.Scripts.Data;
using Features.Services.Assets;
using TMPro;
using UnityEngine;
using Zenject;

namespace Features.Level.Scripts.Info
{
  public class BetDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI betText;
    [TextArea]
    [SerializeField] private string betTextFormat = "Bet {0}";
    [SerializeField] private BetColumn[] columns;
    [SerializeField] private BetDisplaySettings settings;
    
    private IAssetProvider assetProvider;

    [Inject]
    public void Construct(IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
    }

    private void Awake()
    {
      for (int i = 0; i < columns.Length; i++)
      {
        columns[i].Initialize(settings.ElementsInColumn);
      }
    }

    public void Display(int bet)
    {
      betText.text = string.Format(betTextFormat, bet.ToString());
    }

    public void Hide()
    {
      betText.text =  string.Format(betTextFormat, "");
    }
  }
}