using System;

namespace CapstoneProject.Models
{
    public class LampDailyData
    {
        public DateTime DateTime { get; }
        public float BatteryChargeMin { get; }
        public float BatteryConsumptionMin { get; }

        public LampDailyData(
            DateTime dateTime, float batteryChargeMin, float batteryConsumptionMin
        )
        {
            DateTime = dateTime;
            BatteryChargeMin = batteryChargeMin;
            BatteryConsumptionMin = batteryConsumptionMin;
        }
        
        public bool Conflicts(LampDailyData dailyData)
        {
            return dailyData.DateTime.Date == DateTime.Date;
        }
    }
}
