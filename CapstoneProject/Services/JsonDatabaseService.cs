using System;
using CapstoneProject.Models;

namespace CapstoneProject.Services
{
    public class JsonDatabaseService
    {
        public void CatchAllDailyData(Lamp lamp)
        {
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 1), 200, 180));
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 2), 260, 230));
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 3), 220, 150));
        }
        
        public void UpdateDailyDataDatabase(Lamp lamp)
        {
            
        }
    }
}
