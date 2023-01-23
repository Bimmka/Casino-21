using System.Collections.Generic;
using Features.Services.Assets;
using Features.Services.Leaderboard;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Leaderboard.Scripts
{
  [RequireComponent(typeof(UILeaderboardView))]
  public class UILeaderboardWindow : BaseWindow
  {
    [SerializeField] private UILeaderboardView view;
    [SerializeField] private RectTransform elementsParent;
    [SerializeField] private LeaderboardElement elementPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button refreshButton;
    
    private ILeaderboard leaderboard;
    private IUserProvider userProvider;
    private IWindowsService windowsService;
    private LeaderboardElementSpawner spawner;

    [Inject]
    public void Construct(IAssetProvider assetProvider, ILeaderboard leaderboard, IUserProvider userProvider,
      IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      this.userProvider = userProvider;
      this.leaderboard = leaderboard;
      spawner = new LeaderboardElementSpawner(assetProvider, elementsParent.transform, elementPrefab);
      leaderboard.FetchTopHighscores(OnFoundLeaderboard);
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      closeButton.onClick.AddListener(Close);
      refreshButton.onClick.AddListener(Refresh);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      closeButton.onClick.RemoveListener(Close);
      refreshButton.onClick.RemoveListener(Refresh);
    }

    private void Close() => 
      windowsService.Close(ID);

    private void Refresh()
    {
      view.HideError();
      leaderboard.FetchTopHighscores(OnFoundLeaderboard);
    }

    private void OnFoundLeaderboard(bool success, List<LeaderboardUser> users)
    {
      if (IsDestroying)
        return;
      
      if (success == false)
        view.DisplayError();
      else
      {
        spawner.Spawn(users);
        view.DisplayUser(new LeaderboardUser(userProvider.User.CommonData.Nickname, userProvider.User.PointsData.CurrentPoints, -1));
      }
    }
  }
}