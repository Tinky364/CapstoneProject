using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels;

public class LampConnectionViewModel : ViewModelBase
{
    public ICommand ConnectLampCommand { get; }

    private string _selectedPort;
    public string SelectedPort 
    {
        get => _selectedPort;
        set
        {
            _selectedPort = value;
            OnPropertyChanged(nameof(SelectedPort));
        }
    }

    // TODO Remove
    private int _dummyLampId = 123;
    public int DummyLampId
    {
        get => _dummyLampId;
        set
        {
            _dummyLampId = value;
            OnPropertyChanged(nameof(DummyLampId));
        }
    }
    
    // TODO Remove
    private string _dummyLampName = "LampName";
    public string DummyLampName
    {
        get => _dummyLampName;
        set
        {
            _dummyLampName = value;
            OnPropertyChanged(nameof(DummyLampName));
        }
    }
        
    public LampConnectionViewModel(LampConnectionService lampConnectionService)
    {
        if (LampConnectionService.Ports.Count > 0)
            _selectedPort = LampConnectionService.Ports[0];
        ConnectLampCommand = new ConnectLampCommand(lampConnectionService);
    }
}
