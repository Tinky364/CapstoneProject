using System;
using System.Collections.Generic;
using CapstoneProject.Exceptions;

namespace CapstoneProject.Models;

public class Lamp
{
    public int Id { get; }
    public string Name { get; set; }
    public bool ConnectionStatus { get; set; }
    public TimeSpan OnTime { get; set; }
    public TimeSpan OffTime { get; set; }
    public int BatteryPercentage { get; set; }
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

    public Lamp(int id, string name)
    {
        Id = id;
        Name = name;
        ConnectionStatus = false;
        OnTime = default;
        OffTime = default;
        BatteryPercentage = default;
        Automated = default;
        _dailyDataList = new List<LampDailyData>();
    }

    public void InitializeConnection(
        string name, TimeSpan onTime, TimeSpan offTime, int batteryPercentage, bool automated
    )
    {
        Name = name;
        ConnectionStatus = true;
        OnTime = onTime;
        OffTime = offTime;
        BatteryPercentage = batteryPercentage;
        Automated = automated;
    }

    public IEnumerable<LampDailyData> GetAllDailyData() => _dailyDataList;

    public void AddDailyData(LampDailyData dailyData)
    {
        foreach (LampDailyData existingDailyData in _dailyDataList)
        {
            if (existingDailyData.Conflicts(dailyData))
            {
                throw new LampDailyDataConflictException(
                    "Existing daily data conflict.", existingDailyData, dailyData
                );
            }
        }
            
        _dailyDataList.Add(dailyData);
    }

    public void SortAllDailyData()
    {
        _dailyDataList.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
    }
}
