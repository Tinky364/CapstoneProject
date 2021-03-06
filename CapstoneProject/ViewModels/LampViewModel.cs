using CapstoneProject.Models;

namespace CapstoneProject.ViewModels;

public class LampViewModel : ViewModelBase
{
    private readonly Lamp _lamp;

    public string Id => _lamp.Id.ToString();
    public string Name => _lamp.Name;
    public bool ConnectionStatus => _lamp.ConnectionStatus;
    public string OnTime => _lamp.OnTime.ToString(@"hh\:mm");
    public string OffTime => _lamp.OffTime.ToString(@"hh\:mm");
    public string BatteryPercentage => $"{_lamp.BatteryPercentage}%";
    public bool Automated => _lamp.Automated;

    public LampViewModel(Lamp lamp)
    {
        _lamp = lamp;
    }
}
