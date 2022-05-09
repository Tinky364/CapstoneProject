using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Models;

namespace CapstoneProject.Services;

public class JsonDatabaseService
{
    private const string FolderPath = @"%LocalAppData%\LampDailyData";

    private readonly Dictionary<int, Lamp> _allDatabaseLamps;

    public JsonDatabaseService()
    {
        _allDatabaseLamps = new Dictionary<int, Lamp>();
        PullAllDatabaseLampsData();
    }

    private void PullAllDatabaseLampsData()
    {
        try
        {
            string folderPath = Environment.ExpandEnvironmentVariables(FolderPath);
                
            foreach(string fileName in  Directory.GetFiles(folderPath).Select(Path.GetFileName))
            {
                string filePath = @$"{folderPath}\{fileName}";
                if (!File.Exists(filePath)) throw new FileNotFoundException();

                int.TryParse(
                    fileName.Substring(0, fileName.IndexOf(".", StringComparison.Ordinal)),
                    out int id
                );
                _allDatabaseLamps.Add(id, new Lamp(id));

                using FileStream stream = File.OpenRead(filePath);
                var dailyDataList = JsonSerializer.Deserialize<IList<LampDailyData>>(stream);
                if (dailyDataList != null)
                {
                    foreach (var dailyData in dailyDataList)
                    {
                        _allDatabaseLamps[id].AddDailyData(dailyData);
                    }
                }
                stream.Dispose();
                    
                _allDatabaseLamps[id].SortAllDailyData();
            }
        }
        catch (Exception e)
        {
            Trace.WriteLine(e.Message);
        }
    }
        
    public async Task PushDataOfLamp(Lamp lamp)
    {
        try 
        {  
            string folderPath = Environment.ExpandEnvironmentVariables(FolderPath);
            if (!File.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                
            string filePath = @$"{folderPath}\{lamp.Id}.json";
            await using FileStream stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, lamp.GetAllDailyData());
            await stream.DisposeAsync();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public bool IsLampExistInDatabase(int lampId)
    {
        return _allDatabaseLamps.ContainsKey(lampId);
    }

    public bool GetDatabaseLamp(int lampId, out Lamp lamp)
    {
        if (_allDatabaseLamps.TryGetValue(lampId, out Lamp tryLamp))
        {
            lamp = tryLamp;
            return true;
        }
            
        lamp = null;
        return false;
    }

    public bool AddDatabaseLamp(Lamp lamp)
    {
        return _allDatabaseLamps.TryAdd(lamp.Id, lamp);
    }
}
