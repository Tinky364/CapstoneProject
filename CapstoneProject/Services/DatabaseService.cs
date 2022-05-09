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

public class DatabaseService
{
    private const string FolderPath = @"%LocalAppData%\LampDailyData";
    private readonly string _curFolderPath;

    private readonly Dictionary<int, Lamp> _allDatabaseLamps;

    private readonly List<int> _databaseLampIds;

    public DatabaseService()
    {
        _curFolderPath = Environment.ExpandEnvironmentVariables(FolderPath);
        
        _allDatabaseLamps = new Dictionary<int, Lamp>();
        ReadAllDatabaseLampsData();
        
        _databaseLampIds = new List<int>(_allDatabaseLamps.Keys);
    }
    
    public IEnumerable<int> GetAllDatabaseLampIds() => _databaseLampIds;

    private void ReadAllDatabaseLampsData()
    {
        try
        {
            foreach(string fileName in  Directory.GetFiles(_curFolderPath).Select(Path.GetFileName))
            {
                string filePath = @$"{_curFolderPath}\{fileName}";
                if (!File.Exists(filePath)) throw new FileNotFoundException();

                int id = GetLampIdFromFileName(fileName);
                string name = GetLampNameFromFileName(fileName);
                _allDatabaseLamps.Add(id, new Lamp(id, name));

                using FileStream stream = File.OpenRead(filePath);
                var dailyDataList = JsonSerializer.Deserialize<IList<LampDailyData>>(stream);
                if (dailyDataList != null)
                {
                    foreach (var dailyData in dailyDataList)
                        _allDatabaseLamps[id].AddDailyData(dailyData);
                }
                stream.Dispose();
                    
                _allDatabaseLamps[id].SortAllDailyData();
            }
        }
        catch (Exception)
        {
            Trace.WriteLine("Could not read database lamps data files!");
        }
    }

    private int GetLampIdFromFileName(string fileName)
    {
        return int.Parse(fileName.Substring(0, fileName.IndexOf("_", StringComparison.Ordinal)));
    }

    private string GetLampNameFromFileName(string fileName)
    {
        int nameStartIndex = fileName.IndexOf("_", StringComparison.Ordinal) + 1;
        int nameLength = fileName.IndexOf(".", StringComparison.Ordinal) - nameStartIndex;
        return fileName.Substring(nameStartIndex, nameLength);
    }
        
    public async Task WriteDatabaseLampData(Lamp lamp)
    {
        try 
        {
            if (!File.Exists(_curFolderPath)) Directory.CreateDirectory(_curFolderPath);
                
            string filePath = @$"{_curFolderPath}\{lamp.Id}_{lamp.Name}.json";
            await using FileStream stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, lamp.GetAllDailyData());
            await stream.DisposeAsync();
        }
        catch (Exception)
        {
            MessageBox.Show(
                "Could not write the database lamp data file!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }

    public bool IsDatabaseLampExist(int lampId)
    {
        return _allDatabaseLamps.ContainsKey(lampId);
    }

    public bool GetDatabaseLamp(int lampId, out Lamp lamp)
    {
        if (!_allDatabaseLamps.TryGetValue(lampId, out Lamp tryLamp))
        {
            lamp = null;
            return false;
        }

        lamp = tryLamp;
        return true;
    }

    public bool AddDatabaseLamp(Lamp lamp)
    {
        if (!_allDatabaseLamps.TryAdd(lamp.Id, lamp)) return false;
        
        _databaseLampIds.Add(lamp.Id);
        return true;
    }

    public void DeleteDatabaseLampOnLampNameChange(int lampId, string previousLampName)
    {
        try 
        {
            string filePath = @$"{_curFolderPath}\{lampId}_{previousLampName}.json";
            File.Delete(filePath);
        }
        catch (Exception)
        {
            MessageBox.Show(
                "Could not delete the database lamp file!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }
}
