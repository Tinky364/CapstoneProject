using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Models;

namespace CapstoneProject.Services
{
    public class JsonDatabaseService
    {
        private const string FolderPath = @"%LocalAppData%\LampDailyData";
        
        public async Task PullAllDailyData(Lamp lamp)
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
        
        public async Task UpdateDailyDataDatabase(Lamp lamp)
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
