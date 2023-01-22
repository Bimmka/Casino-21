using System.Collections.Generic;
using Features.Services.UI.Factory;
using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;

namespace Features.Services.UI.Windows
{
  public class WindowsService : IWindowsService
  {
    private IUIFactory uiFactory;

    private readonly Dictionary<WindowId, BaseWindow> windows;
    
    public WindowsService()
    {
      windows = new Dictionary<WindowId, BaseWindow>(10);
    }

    public void Register(IUIFactory factory) => 
      uiFactory = factory;

    public BaseWindow Open(WindowId windowId)
    {
      if (windows.ContainsKey(windowId) == false)
        AddSpawnedWindow(windowId,CreateWindow(windowId));
      
      windows[windowId].Open();
      return windows[windowId];
    }

    public void Close(WindowId windowId)
    {
      if (windows.ContainsKey(windowId) == false)
        return;

      windows[windowId].Destroy();
    }

    public BaseWindow Window(WindowId windowId)
    {
      if (windows.ContainsKey(windowId) == false)
        return null;

      return windows[windowId];
    }

    private BaseWindow CreateWindow(WindowId windowId) => 
      uiFactory.Create(windowId);


    private void AddSpawnedWindow(WindowId windowId, BaseWindow window)
    {
      windows.Add(windowId, window);
      window.Destroyed += OnWindowDestroyed;
    }

    private void OnWindowDestroyed(BaseWindow window)
    {
      window.Destroyed -= OnWindowDestroyed;
      Close(window.ID);
      windows.Remove(window.ID);
    }
  }
}