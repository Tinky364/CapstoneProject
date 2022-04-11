using System;

namespace CapstoneProject.Models
{
    public class LampDailyData
    {
        public DateTime DateTime { get; }
        public float BatteryConsumptionMin { get; }
        public float BatteryChargeMin { get; }

        public LampDailyData(
            DateTime dateTime, float batteryConsumptionMin, float batteryChargeMin
        )
        {
            DateTime = dateTime;
            BatteryConsumptionMin = batteryConsumptionMin;
            BatteryChargeMin = batteryChargeMin;
        }
        
        public bool Conflicts(LampDailyData dailyData)
        {
            return dailyData.DateTime.Date == DateTime.Date;
        }
    }
}
