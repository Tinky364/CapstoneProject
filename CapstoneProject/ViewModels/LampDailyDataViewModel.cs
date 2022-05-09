using CapstoneProject.Models;

namespace CapstoneProject.ViewModels;

public class LampDailyDataViewModel : ViewModelBase
{
    private readonly LampDailyData _lampDailyData;

    public string DateTime => _lampDailyData.DateTime.ToString("d");
        
    public string BatteryChargeMin
    {
        get
        {
            float totalMin = _lampDailyData.BatteryChargeMin;
            int hour = (int)(totalMin / 60);
            int min = (int)(totalMin % 60);
                
            if (min == 0 && hour == 0) return "0";
            if (hour == 0) return $"{min}m";
            if (min == 0) return $"{hour}h";
            return $"{hour}h {min}m";
        }
    }

    public string BatteryConsumptionMin
    {
        get
        {
            float totalMin = _lampDailyData.BatteryConsumptionMin;
            int hour = (int)(totalMin / 60);
            int min = (int)(totalMin % 60);
                
            if (min == 0 && hour == 0) return "0";
            if (hour == 0) return $"{min}m";
            if (min == 0) return $"{hour}h";
            return $"{hour}h {min}m";
        }
    }

    public LampDailyDataViewModel(LampDailyData lampDailyData)
    {
        _lampDailyData = lampDailyData;
    }
}
