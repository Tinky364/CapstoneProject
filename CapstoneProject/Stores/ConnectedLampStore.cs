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
                OnConnectedLampChanged(_lamp != null, _lamp);
            }
        }

        public event Action<bool, Lamp> ConnectedLampChanged;

        private void OnConnectedLampChanged(bool hasConnection, Lamp lamp)
        {
            ConnectedLampChanged?.Invoke(hasConnection, lamp);
        }
    }
}
