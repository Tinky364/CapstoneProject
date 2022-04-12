using CapstoneProject.Models;

namespace CapstoneProject.ViewModels
{
    public class LampViewModel : ViewModelBase
    {
        private readonly Lamp _lamp;

        public string Id => _lamp.Id.ToString();
        public string Name => _lamp.Name;
        public string ConnectionStatus => _lamp.ConnectionStatus ? "Connected" : "Disconnected";
        public string OnTime => _lamp.OnTime.ToString();
        public string OffTime => _lamp.OffTime.ToString();
        public string BatteryPercentage => _lamp.BatteryPercentage.ToString();
        public string Automated => _lamp.Automated ? "On" : "Off";

        public LampViewModel(Lamp lamp)
        {
            _lamp = lamp;
        }
    }
}
