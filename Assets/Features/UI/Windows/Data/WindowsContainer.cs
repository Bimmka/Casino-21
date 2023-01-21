using System.Collections.Generic;
using Features.Services.UI.Factory;
using Features.UI.Windows.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.UI.Windows.Data
{
  [CreateAssetMenu(fileName = "WindowsContainer", menuName = "StaticData/UI/Create Windows Instantiate Data", order = 52)]
  public class WindowsContainer : SerializedScriptableObject
  {
    public Dictionary<WindowId, BaseWindow> InstantiateData;
  }
}