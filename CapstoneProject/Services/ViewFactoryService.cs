using CapstoneProject.Stores;
using CapstoneProject.ViewModels;

namespace CapstoneProject.Services;

public class ViewFactoryService
{
    private readonly NavigationStore _navigationStore;
    private readonly LampConnectionService _lampConnectionService;
    private readonly DatabaseService _databaseService;

    public ViewFactoryService(
        NavigationStore navigationStore, LampConnectionService lampConnectionService, 
        DatabaseService databaseService
    )
    {
        _navigationStore = navigationStore;
        _lampConnectionService = lampConnectionService;
        _databaseService = databaseService;
    }
        
    public LandingPageViewModel LandingPage()
    {
        return new LandingPageViewModel(
            _lampConnectionService, _databaseService, LampConnection, LampOverview, 
            DailyAnalysisList, DailyAnalysisText
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
        
    public DailyAnalysisListViewModel DailyAnalysisList(int lampId)
    {
        return new DailyAnalysisListViewModel(_databaseService, lampId);
    }

    public DailyAnalysisTextViewModel DailyAnalysisText()
    {
        return new DailyAnalysisTextViewModel();
    }

    public LampSettingsViewModel LampSettings()
    {
        return new LampSettingsViewModel(
            _lampConnectionService, _databaseService,
            new NavigationService(_navigationStore, LandingPage)
        );
    }
}
