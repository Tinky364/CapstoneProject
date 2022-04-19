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
        public async Task PullAllDailyData(Lamp lamp)
        {
            try
            {
                string fileName = $"{lamp.Id}.json";
                string dir = @$"{AppDomain.CurrentDomain.BaseDirectory}DailyData\{fileName}";
                if (!File.Exists(dir)) return;

                using FileStream stream = File.OpenRead(dir);
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
                string fileName = $"{lamp.Id}.json";
                string dir = @$"{AppDomain.CurrentDomain.BaseDirectory}DailyData\{fileName}";

                using FileStream stream = File.Create(dir);
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
