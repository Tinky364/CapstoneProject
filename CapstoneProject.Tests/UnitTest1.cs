using CapstoneProject.Models;
using CapstoneProject.Services;
using CapstoneProject.Stores;
using CapstoneProject.ViewModels;
using Xunit;

namespace CapstoneProject.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        NavigationStore navigationStore = new NavigationStore();
        ConnectedLampStore connectedLampStore = new ConnectedLampStore();
        DatabaseService databaseService = new DatabaseService();
        LampConnectionService lampConnectionService = new LampConnectionService(
            connectedLampStore, databaseService
        );
        ViewFactoryService viewFactoryService = new ViewFactoryService(
            navigationStore, lampConnectionService, databaseService
        );
        
        navigationStore.CurrentViewModel = viewFactoryService.LandingPage();
        Assert.IsType<LandingPageViewModel>(navigationStore.CurrentViewModel);
        if (navigationStore.CurrentViewModel is LandingPageViewModel landingPageViewModel)
        {
            Assert.IsType<LampConnectionViewModel>(landingPageViewModel.OverviewContent);
            if (landingPageViewModel.OverviewContent is LampConnectionViewModel lampConnectionViewModel)
            {
                object[] connectionData = { "COM1", 123, "Lamp123"};
                lampConnectionViewModel.ConnectLampCommand.Execute(connectionData);
            }
        }

    }
}
