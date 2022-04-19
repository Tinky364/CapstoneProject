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
        private readonly NavigationService _lampConnectedViewNavigationService;
        private readonly JsonDatabaseService _jsonDatabaseService;

        public LampConnectionViewModel(
            ConnectToLampService connectToLampService,
            NavigationService lampConnectedViewNavigationService
        )
        {
            ConnectLampCommand = new ConnectToLampCommand(connectToLampService);

            _lampConnectedViewNavigationService = lampConnectedViewNavigationService;
            _jsonDatabaseService = new JsonDatabaseService();
            
            connectToLampService.AddListenerToConnectedLampChanged(OnConnectedLampChanged);
        }

        private async void OnConnectedLampChanged(bool hasConnection, Lamp lamp)
        {
            if (!hasConnection) return;
            
            await SynchronizeDailyData(lamp);
            _lampConnectedViewNavigationService.Navigate();
        }

        private async Task SynchronizeDailyData(Lamp lamp)
        {
            //await _jsonDatabaseService.PullAllDailyData(_lamp);
            await _jsonDatabaseService.UpdateDailyDataDatabase(lamp);
            lamp.SortAllDailyData();
        }
    }
}
