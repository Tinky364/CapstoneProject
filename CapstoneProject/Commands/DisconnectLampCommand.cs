using System.Threading.Tasks;
using CapstoneProject.Services;

namespace CapstoneProject.Commands
{
    public class DisconnectLampCommand : AsyncCommandBase
    {
        private readonly LampConnectionService _lampConnectionService;

        public DisconnectLampCommand(LampConnectionService lampConnectionService)
        {
            _lampConnectionService = lampConnectionService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _lampConnectionService.DisconnectLamp();
        }
    }
}
