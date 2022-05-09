using System.Windows;
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
    private readonly DatabaseService _databaseService;
    private readonly LampConnectionService _lampConnectionService;
    private readonly ViewFactoryService _viewFactoryService;

    public App()
    {
        _navigationStore = new NavigationStore();
        _connectedLampStore = new ConnectedLampStore();
        _databaseService = new DatabaseService();
        _lampConnectionService = new LampConnectionService(_connectedLampStore, _databaseService);
        _viewFactoryService = new ViewFactoryService(
            _navigationStore, _lampConnectionService, _databaseService
        );
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = _viewFactoryService.LandingPage();
        MainWindow = new MainWindowView {DataContext = new MainWindowViewModel(_navigationStore)};
        MainWindow.Show();
            
        base.OnStartup(e);
    }
}
