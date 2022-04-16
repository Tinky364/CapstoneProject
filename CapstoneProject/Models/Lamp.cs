using System;
using System.Collections.Generic;
using CapstoneProject.Exceptions;

namespace CapstoneProject.Models
{
    public class Lamp
    {
        public int Id { get; }
        public string Name { get; set; }
        public bool ConnectionStatus { get; }
        public TimeSpan OnTime { get; set; }
        public TimeSpan OffTime { get; set; }
        public int BatteryPercentage { get; }
        public bool Automated { get; set; }

        private readonly List<LampDailyData> _dailyDataList;

        public Lamp(
            int id, string name, bool connectionStatus, TimeSpan onTime, TimeSpan offTime,
            int batteryPercentage, bool automated
        )
        {
            Id = id;
            Name = name;
            ConnectionStatus = connectionStatus;
            OnTime = onTime;
            OffTime = offTime;
            BatteryPercentage = batteryPercentage;
            Automated = automated;
            _dailyDataList = new List<LampDailyData>();
        }

        public IEnumerable<LampDailyData> GetAllDailyData()
        {
            return _dailyDataList;
        }

        public void AddDailyData(LampDailyData dailyData)
        {
            foreach (LampDailyData existingDailyData in _dailyDataList)
            {
                if (existingDailyData.Conflicts(dailyData))
                {
                    throw new LampDailyDataConflictException(existingDailyData, dailyData);
                }
            }
            
            _dailyDataList.Add(dailyData);
        }
    }
}
