using System.Globalization;
using CapstoneProject.Models;

namespace CapstoneProject.ViewModels
{
    public class LampDailyDataViewModel : ViewModelBase
    {
        private readonly LampDailyData _lampDailyData;

        public string DateTime => _lampDailyData.DateTime.ToString("d");
        public string BatteryChargeMin =>
            _lampDailyData.BatteryChargeMin.ToString(CultureInfo.InvariantCulture);
        public string BatteryConsumptionMin =>
            _lampDailyData.BatteryConsumptionMin.ToString(CultureInfo.InvariantCulture);

        public LampDailyDataViewModel(LampDailyData lampDailyData)
        {
            _lampDailyData = lampDailyData;
        }
    }
}
