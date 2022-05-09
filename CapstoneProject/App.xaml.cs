﻿using System.Windows;
using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;

namespace CapstoneProject;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore _navigationStore;
    private readonly ConnectedLampStore _connectedLampStore;
    private readonly JsonDatabaseService _jsonDatabaseService;
    private readonly LampConnectionService _lampConnectionService;
    private readonly ViewFactoryService _viewFactoryService;

    public App()
    {
        _navigationStore = new NavigationStore();
        _connectedLampStore = new ConnectedLampStore();
        _jsonDatabaseService = new JsonDatabaseService();
        _lampConnectionService = new LampConnectionService(
            _connectedLampStore, _jsonDatabaseService
        );
        _viewFactoryService = new ViewFactoryService(_navigationStore, _lampConnectionService);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = _viewFactoryService.LandingPage();
        MainWindow = new MainWindow { DataContext = new MainViewModel(_navigationStore) };
        MainWindow.Show();
            
        base.OnStartup(e);
    }
}
