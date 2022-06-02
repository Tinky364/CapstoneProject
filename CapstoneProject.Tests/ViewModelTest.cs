using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;
using Xunit;

namespace CapstoneProject.Tests;

public class ViewModelFactoryServiceTest
{
    [Fact]
    public void Create_LandingPageViewModel()
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
    }
    
    [Fact]
    public void Create_LampConnectionViewModel()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );
        
        navigationStore.CurrentViewModel = viewModelFactoryService.LampConnection();
        Assert.IsType<LampConnectionViewModel>(navigationStore.CurrentViewModel);
    }
    
    [Fact]
    public void Create_LampOverviewViewModel()
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
    }
    
    [Fact]
    public void Create_DailyAnalysisListViewModel()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );
        
        navigationStore.CurrentViewModel = viewModelFactoryService.DailyAnalysisList(123);
        Assert.IsType<DailyAnalysisListViewModel>(navigationStore.CurrentViewModel);
    }
    
    [Fact]
    public void Create_DailyAnalysisTextViewModel()
    {
        NavigationStore navigationStore = new();
        ConnectedLampStore connectedLampStore = new();
        DatabaseService databaseService = new();
        LampConnectionService lampConnectionService = new(connectedLampStore, databaseService);
        ViewModelFactoryService viewModelFactoryService = new(
            navigationStore, lampConnectionService, databaseService
        );
        
        navigationStore.CurrentViewModel = viewModelFactoryService.DailyAnalysisText();
        Assert.IsType<DailyAnalysisTextViewModel>(navigationStore.CurrentViewModel);
    }
    
    [Fact]
    public void Create_LampSettingsViewModel()
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
    }
}
