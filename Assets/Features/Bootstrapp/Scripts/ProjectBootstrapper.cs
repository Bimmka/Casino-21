﻿using Features.CustomCoroutine;
using Features.GameStates;
using Features.GameStates.Factory;
using Features.GameStates.Observer.Scripts;
using Features.GameStates.States;
using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.GameSettings;
using Features.Services.Save;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.UI.Windows.Data;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class ProjectBootstrapper : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private WindowsContainer windowsContainer;
    [SerializeField] private LoadingCurtain loadingCurtain;
    [SerializeField] private GameStatesObserver gameStatesObserver;

    public override void Start()
    {
      base.Start();
      ResolveGameStatesObserver();
    }

    public override void InstallBindings()
    {
      BindAssetProvider();
      BindStaticData();
      BindCoroutineRunner();
      BindSceneLoading();
      BindLoadingCurtain();
      BindWindowsService();
      BindGameStateMachine();
      BindGameStatesFactory();
      BindGameStatesObserver();
      BindUserProviderService();
      BindSaveService();
      BindGameSettingsService();
    }

    private void ResolveGameStatesObserver() => 
      Container.Resolve<GameStatesObserver>().StartGame();

    private void BindAssetProvider() => 
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();

    private void BindStaticData() =>
      Container.Bind<IStaticDataService>().To<StaticDataService>().FromNew().AsSingle()
        .WithArguments(windowsContainer);

    private void BindCoroutineRunner() => 
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindSceneLoading() => 
      Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();

    private void BindLoadingCurtain() => 
      Container.Bind<LoadingCurtain>().ToSelf().FromComponentInNewPrefab(loadingCurtain).AsSingle();

    private void BindWindowsService() => 
      Container.Bind<IWindowsService>().To<WindowsService>().FromNew().AsSingle();

    private void BindGameStateMachine() => 
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();

    private void BindGameStatesFactory() => 
      Container.Bind<GameStatesFactory>().ToSelf().FromNew().AsSingle();

    private void BindGameStatesObserver() => 
      Container.Bind<GameStatesObserver>().ToSelf().FromComponentInNewPrefab(gameStatesObserver).AsSingle();

    private void BindUserProviderService() => 
      Container.Bind<IUserProvider>().To<UserProviderService>().FromNew().AsSingle();

    private void BindSaveService() => 
      Container.Bind<ISaveService>().To<PrefsSaveService>().FromNew().AsSingle();

    private void BindGameSettingsService() => 
      Container.Bind<IGameSettings>().To<GameSettingsService>().FromNew().AsSingle();
  }
}