using Features.Constants;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Services.UI.Factory.BaseUI
{
  public class UIFactory : PlaceholderFactory<BaseWindow>, IUIFactory
  {
    private readonly DiContainer container;
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;

    private Transform uiRoot;

    private Camera mainCamera;

    [Inject]
    public UIFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticData, IWindowsService windowsService)
    {
      this.container = container;
      this.assets = assets;
      this.staticData = staticData;
      windowsService.Register(this);
    }

    public BaseWindow Create(WindowId id)
    {
      BaseWindow window = LoadWindowInstantiateData(id);
      
      if (uiRoot == null)
        CreateUIRoot();

      return CreateWindow(window, id);
    }

    private void CreateUIRoot()
    {
        if (uiRoot != null)
            return;

        UIRoot prefab = assets.Instantiate(GameConstants.UIRootPath).GetComponent<UIRoot>();

        prefab.SetCamera(GetCamera());
        uiRoot = prefab.transform;
    }

    private BaseWindow CreateWindow(BaseWindow prefab, WindowId id)
    {
      BaseWindow window = InstantiateWindow(prefab, id);
      return window;
    }

    private BaseWindow InstantiateWindow(BaseWindow prefab, WindowId id)
    {
      BaseWindow window = container.InstantiatePrefab(prefab, uiRoot).GetComponent<BaseWindow>();
      window.SetID(id);
      return window;
    }


    private BaseWindow LoadWindowInstantiateData(WindowId id) => 
      staticData.ForWindow(id);

    private Camera GetCamera()
    {
      if (mainCamera == null)
        mainCamera = Camera.main;
      return mainCamera;
    }
  }
}