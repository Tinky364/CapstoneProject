using System.Threading.Tasks;
using CapstoneProject.Services;

namespace CapstoneProject.Commands
{
    public class ConnectLampCommand : AsyncCommandBase
    {
        private readonly LampConnectionService _lampConnectionService;

        public ConnectLampCommand(LampConnectionService lampConnectionService)
        {
            _lampConnectionService = lampConnectionService;
        }
        
        public override async Task ExecuteAsync(object parameter)
        {
            _lampConnectionService.ConnectLamp();
        }
    }
}
