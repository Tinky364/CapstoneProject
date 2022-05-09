using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Models;

namespace CapstoneProject.Services
{
    public class JsonDatabaseService
    {
        private const string FolderPath = @"%LocalAppData%\LampDailyData";

        public readonly Dictionary<int, Lamp> AllDatabaseLamps;

        public JsonDatabaseService()
        {
            AllDatabaseLamps = new Dictionary<int, Lamp>();
            PullAllDailyData();
        }

        private void PullAllDailyData()
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
                    AllDatabaseLamps.Add(id, new Lamp(id));

                    using FileStream stream = File.OpenRead(filePath);
                    var dailyDataList = JsonSerializer.Deserialize<IList<LampDailyData>>(stream);
                    if (dailyDataList != null)
                    {
                        foreach (var dailyData in dailyDataList)
                        {
                            AllDatabaseLamps[id].AddDailyData(dailyData);
                        }
                    }
                    stream.Dispose();
                    
                    AllDatabaseLamps[id].SortAllDailyData();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
        
        public async Task PullDataOfLamp(Lamp lamp)
        {
            try
            {
                string folderPath = Environment.ExpandEnvironmentVariables(FolderPath);
                
                string filePath = @$"{folderPath}\{lamp.Id}.json";
                if (!File.Exists(filePath)) return;

                await using FileStream stream = File.OpenRead(filePath);
                var dailyDataList = await JsonSerializer.DeserializeAsync<IList<LampDailyData>>(stream);
                if (dailyDataList != null)
                {
                    foreach (var dailyData in dailyDataList)
                    {
                        lamp.AddDailyData(dailyData);
                    }
                }
                await stream.DisposeAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
