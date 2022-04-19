using System.Threading.Tasks;
using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Models;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampConnectionViewModel : ViewModelBase
    {
        public ICommand ConnectLampCommand { get; }
        private readonly NavigationService _landingPageViewNavigationService;
        private readonly JsonDatabaseService _jsonDatabaseService;

        public LampConnectionViewModel(
            ConnectToLampService connectToLampService,
            NavigationService landingPageViewNavigationService
        )
        {
            ConnectLampCommand = new ConnectToLampCommand(connectToLampService);

            _landingPageViewNavigationService = landingPageViewNavigationService;
            _jsonDatabaseService = new JsonDatabaseService();
            
            connectToLampService.AddListenerToConnectedLampChanged(OnConnectedLampChanged);
        }

        private async void OnConnectedLampChanged(Lamp lamp)
        {
            if (lamp == null) return;
            
            await SynchronizeDailyData(lamp);
            _landingPageViewNavigationService.Navigate();
        }

        private async Task SynchronizeDailyData(Lamp lamp)
        {
            await _jsonDatabaseService.PullAllDailyData(lamp);
            await _jsonDatabaseService.UpdateDailyDataDatabase(lamp);
            lamp.SortAllDailyData();
        }
    }
}
