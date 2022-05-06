using System;
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
            await _lampConnectionService.ConnectLamp(Convert.ToString(parameter));
        }
    }
}
