using System;
using CapstoneProject.Models;

namespace CapstoneProject.Services
{
    public class LampConnectionService
    {
        public Lamp ConnectToLamp()
        {
            Lamp lamp = new Lamp(
                123, "MyLamp", true, new TimeSpan(18, 0, 0), new TimeSpan(4, 0, 0), 95, true
            );
            CatchAllNewDailyData(lamp);
            return lamp;
        }

        private void CatchAllNewDailyData(Lamp lamp)
        {
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 4), 180, 160));
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 5), 220, 240));
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 6), 300, 110));
        }
    }
}
