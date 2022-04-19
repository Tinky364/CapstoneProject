using System;
using CapstoneProject.Models;
using CapstoneProject.Stores;

namespace CapstoneProject.Services
{
    public class ConnectToLampService
    {
        private readonly ConnectedLampStore _connectedLampStore;
        
        public ConnectToLampService(ConnectedLampStore connectedLampStore)
        {
            _connectedLampStore = connectedLampStore;
        }

        public void ConnectToLamp()
        {
            _connectedLampStore.Lamp = new Lamp(
                123, "MyLamp", true, new TimeSpan(18, 0, 0), new TimeSpan(4, 0, 0), 95, true
            );
            PullAllNewDailyDataFromLamp(_connectedLampStore.Lamp);
        }
        
        public Lamp GetConnectedLamp()
        {
            return _connectedLampStore.Lamp;
        }

        private void PullAllNewDailyDataFromLamp(Lamp lamp)
        {
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 4), 180, 160));
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 5), 220, 240));
            lamp.AddDailyData(new LampDailyData(new DateTime(2022, 1, 6), 300, 110));
        }

        public void AddListenerToConnectedLampChanged(Action<bool, Lamp> onAction)
        {
            _connectedLampStore.ConnectedLampChanged += onAction;
        }
    }
}
