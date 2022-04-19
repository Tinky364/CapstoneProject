using System;
using CapstoneProject.Models;

namespace CapstoneProject.Stores
{
    public class ConnectedLampStore
    {
        private Lamp _lamp;
        public Lamp Lamp
        {
            get => _lamp;
            set
            {
                _lamp = value;
                if (_lamp == null)
                    OnLampDisconnected();
            }
        }

        public event Action<Lamp> LampConnected;
        public event Action LampDisconnected;

        public void OnLampConnected(Lamp lamp)
        {
            LampConnected?.Invoke(lamp);
        }

        public void OnLampDisconnected()
        {
            LampDisconnected?.Invoke();
        }
    }
}
