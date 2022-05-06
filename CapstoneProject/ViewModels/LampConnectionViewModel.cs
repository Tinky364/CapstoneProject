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
        
        private readonly JsonDatabaseService _jsonDatabaseService;

        public LampConnectionViewModel(ConnectToLampService connectToLampService)
        {
            ConnectLampCommand = new ConnectToLampCommand(connectToLampService);

            _jsonDatabaseService = new JsonDatabaseService();
            
            connectToLampService.AddListenerToLampConnected(OnLampConnected);
        }

        private async void OnLampConnected(Lamp lamp)
        {
            await SynchronizeDailyData(lamp);
        }

        private async Task SynchronizeDailyData(Lamp lamp)
        {
            await _jsonDatabaseService.PullAllDailyData(lamp);
            await _jsonDatabaseService.UpdateDailyDataDatabase(lamp);
            lamp.SortAllDailyData();
        }
    }
}
