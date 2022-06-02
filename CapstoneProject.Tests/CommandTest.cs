using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;
using Xunit;

namespace CapstoneProject.Tests;

public class CommandTest
{
    [Fact]
    public void Execute_ConnectLampCommand()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );
        
        navigationStore.CurrentViewModel = viewModelFactoryService.LandingPage();
        Assert.IsType<LandingPageViewModel>(navigationStore.CurrentViewModel);
        
        LandingPageViewModel? landingPageViewModel =
            navigationStore.CurrentViewModel as LandingPageViewModel;
        Assert.NotNull(landingPageViewModel);
        
        Assert.IsType<LampConnectionViewModel>(landingPageViewModel?.OverviewContent);
        LampConnectionViewModel? lampConnectionViewModel =
            landingPageViewModel?.OverviewContent as LampConnectionViewModel;
        Assert.NotNull(lampConnectionViewModel);

        object[] connectionData = { "COM1", 123, "Lamp123"};
        lampConnectionViewModel?.ConnectLampCommand.Execute(connectionData);
        Assert.NotNull(connectedLampStore.Lamp);
        Assert.True(lampConnectionService.IsLampConnected());
    }
    
    [Fact]
    public void Execute_DisconnectLampCommand()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );

        lampConnectionService.ConnectLamp("", 123, "lamp123");
        navigationStore.CurrentViewModel = viewModelFactoryService.LampOverview();
        Assert.IsType<LampOverviewViewModel>(navigationStore.CurrentViewModel);
        
        LampOverviewViewModel? lampConnectionViewModel =
            navigationStore.CurrentViewModel as LampOverviewViewModel;
        Assert.NotNull(lampConnectionViewModel);

        lampConnectionViewModel?.DisconnectLampCommand.Execute(null);
        Assert.Null(connectedLampStore.Lamp);
        Assert.True(!lampConnectionService.IsLampConnected());
    }

    [Fact]
    public void Execute_GoToLampSettingsViewCommand()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );

        lampConnectionService.ConnectLamp("", 123, "lamp123");
        navigationStore.CurrentViewModel = viewModelFactoryService.LampOverview();
        Assert.IsType<LampOverviewViewModel>(navigationStore.CurrentViewModel);
        
        LampOverviewViewModel? lampConnectionViewModel =
            navigationStore.CurrentViewModel as LampOverviewViewModel;
        Assert.NotNull(lampConnectionViewModel);
        
        lampConnectionViewModel?.GoToLampSettingsViewCommand.Execute(null);
        Assert.IsType<LampSettingsViewModel>(navigationStore.CurrentViewModel);
    }

    [Fact]
    public void Execute_SaveLampSettingsCommand()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );

        string lampName = "lamp123";
        lampConnectionService.ConnectLamp("", 123, lampName);
        Assert.Equal(lampName, connectedLampStore.Lamp.Name);
        navigationStore.CurrentViewModel = viewModelFactoryService.LampSettings();
        Assert.IsType<LampSettingsViewModel>(navigationStore.CurrentViewModel);
        LampSettingsViewModel? lampSettingsViewModel =
            navigationStore.CurrentViewModel as LampSettingsViewModel;
        Assert.NotNull(lampSettingsViewModel);

        string newLampName = "lamp321";
        lampSettingsViewModel!.Name = newLampName;
        lampSettingsViewModel.SaveLampSettingsCommand.Execute(null);
        Assert.Equal(newLampName, connectedLampStore.Lamp.Name);
    }
    
    [Fact]
    public void Execute_GoToLandingPageViewCommand()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );

        lampConnectionService.ConnectLamp("", 123, "lamp123");
        navigationStore.CurrentViewModel = viewModelFactoryService.LampSettings();
        Assert.IsType<LampSettingsViewModel>(navigationStore.CurrentViewModel);
        LampSettingsViewModel? lampSettingsViewModel =
            navigationStore.CurrentViewModel as LampSettingsViewModel;
        Assert.NotNull(lampSettingsViewModel);

        lampSettingsViewModel?.GoToLandingPageViewCommand.Execute(null);
        Assert.IsType<LandingPageViewModel>(navigationStore.CurrentViewModel);
    }
}
