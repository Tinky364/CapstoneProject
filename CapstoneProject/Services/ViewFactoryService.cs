using CapstoneProject.Stores;
using CapstoneProject.ViewModels;

namespace CapstoneProject.Services;

public class ViewFactoryService
{
    private readonly NavigationStore _navigationStore;
    private readonly LampConnectionService _lampConnectionService;

    public ViewFactoryService(
        NavigationStore navigationStore, LampConnectionService lampConnectionService
    )
    {
        _navigationStore = navigationStore;
        _lampConnectionService = lampConnectionService;
    }
        
    public LandingPageViewModel LandingPage()
    {
        return new LandingPageViewModel(
            _lampConnectionService, LampConnection, LampOverview, LampDailyAnalysis
        );
    }
        
    public LampConnectionViewModel LampConnection()
    {
        return new LampConnectionViewModel(_lampConnectionService);
    }
        
    public LampOverviewViewModel LampOverview()
    {
        return new LampOverviewViewModel(
            _lampConnectionService, new NavigationService(_navigationStore, LampSettings)
        );
    }
        
    public LampDailyAnalysisViewModel LampDailyAnalysis()
    {
        return new LampDailyAnalysisViewModel(_lampConnectionService);
    }

    public LampSettingsViewModel LampSettings()
    {
        return new LampSettingsViewModel(
            _lampConnectionService, new NavigationService(_navigationStore, LandingPage)
        );
    }
}
