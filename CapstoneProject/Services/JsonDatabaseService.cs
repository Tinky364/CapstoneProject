using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CapstoneProject.Models;

namespace CapstoneProject.Services
{
    public class JsonDatabaseService
    {
        // TODO fix it
        public async Task CatchAllDailyData(Lamp lamp)
        {
            string fileName = $"{lamp.Id}.json";
            string dir = @$"{AppDomain.CurrentDomain.BaseDirectory}DailyData\{fileName}";

            using FileStream stream = File.OpenRead(dir);
            var dailyDataList = await JsonSerializer.DeserializeAsync<IList<LampDailyData>>(stream);
            if (dailyDataList != null)
            {
                foreach (var dailyData in dailyDataList)
                {
                    lamp.AddDailyData(dailyData);
                }
            }
        }
        
        public async Task UpdateDailyDataDatabase(Lamp lamp)
        {
            string fileName = $"{lamp.Id}.json";
            string dir = @$"{AppDomain.CurrentDomain.BaseDirectory}DailyData\{fileName}";

            using FileStream stream = File.Create(dir);
            await JsonSerializer.SerializeAsync(stream, lamp.GetAllDailyData());
            await stream.DisposeAsync();
        }
    }
}
