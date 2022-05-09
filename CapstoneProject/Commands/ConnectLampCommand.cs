using System.Threading.Tasks;
using CapstoneProject.Services;

namespace CapstoneProject.Commands;

public class ConnectLampCommand : AsyncCommandBase
{
    private readonly LampConnectionService _lampConnectionService;

    public ConnectLampCommand(LampConnectionService lampConnectionService)
    {
        _lampConnectionService = lampConnectionService;
    }
        
    public override async Task ExecuteAsync(object parameter)
    {
        object[] values = (object[])parameter;
        string selectedPort = (string)values[0];
        int dummyLampId = (int)values[1];
        string dummyLampName = (string)values[2];
        await _lampConnectionService.ConnectLamp(selectedPort, dummyLampId, dummyLampName);
    }
}
