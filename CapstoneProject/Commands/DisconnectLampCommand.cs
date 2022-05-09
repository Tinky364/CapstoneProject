using CapstoneProject.Services;

namespace CapstoneProject.Commands;

public class DisconnectLampCommand : CommandBase
{
    private readonly LampConnectionService _lampConnectionService;

    public DisconnectLampCommand(LampConnectionService lampConnectionService)
    {
        _lampConnectionService = lampConnectionService;
    }

    public override void Execute(object parameter)
    {
        _lampConnectionService.DisconnectLamp();
    }
}
