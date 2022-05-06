using System.Windows.Input;
using CapstoneProject.Commands;
using CapstoneProject.Services;

namespace CapstoneProject.ViewModels
{
    public class LampConnectionViewModel : ViewModelBase
    {
        public ICommand ConnectLampCommand { get; }
        
        public LampConnectionViewModel(LampConnectionService lampConnectionService)
        {
            ConnectLampCommand = new ConnectLampCommand(lampConnectionService);
        }
    }
}
