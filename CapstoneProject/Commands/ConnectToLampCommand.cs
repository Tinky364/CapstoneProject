using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Services;

namespace CapstoneProject.Commands
{
    public class ConnectToLampCommand : AsyncCommandBase
    {
        private readonly ConnectToLampService _connectToLampService;

        public ConnectToLampCommand(ConnectToLampService connectToLampService)
        {
            _connectToLampService = connectToLampService;
        }
        
        public override async Task ExecuteAsync(object parameter)
        {
            _connectToLampService.ConnectToLamp();
        }
    }
}
