using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampConnectionViewModel : ViewModelBase
    {
        public ICommand ConnectLampCommand { get; }

        private string _selectedPort = LampConnectionService.Ports[0];
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
        private int _dummyId = 123;
        public int DummyId
        {
            get => _dummyId;
            set
            {
                _dummyId = value;
                OnPropertyChanged(nameof(DummyId));
            }
        }
        
        public LampConnectionViewModel(LampConnectionService lampConnectionService)
        {
            ConnectLampCommand = new ConnectLampCommand(lampConnectionService);
        }
    }
}
