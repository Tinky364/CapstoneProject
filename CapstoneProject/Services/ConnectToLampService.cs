using System;
using System.Windows;
using CapstoneProject.Exceptions;
using CapstoneProject.Models;
using CapstoneProject.Stores;

namespace CapstoneProject.Services
{
    public class ConnectToLampService
    {
        private readonly ConnectedLampStore _connectedLampStore;
        private readonly Random _random;
        
        public ConnectToLampService(ConnectedLampStore connectedLampStore)
        {
            _connectedLampStore = connectedLampStore;
            _random = new Random();
        }

        // TODO replace placeholder method logic
        public void ConnectToLamp()
        {
            _connectedLampStore.Lamp = new Lamp(
                124, GenerateName(_random.Next(3, 14)), true, 
                new TimeSpan(_random.Next(0,24), _random.Next(0,59), 0), 
                new TimeSpan(_random.Next(0,24), _random.Next(0,59), 0), 
                _random.Next(1,101), _random.NextDouble() >= 0.5
            );
            PullAllNewDailyDataFromLamp(_connectedLampStore.Lamp);
            _connectedLampStore.OnLampConnected(_connectedLampStore.Lamp);
        }
        
        public Lamp GetConnectedLamp()
        {
            return _connectedLampStore.Lamp;
        }
        
        public void AddListenerToLampConnected(Action<Lamp> onAction)
        {
            _connectedLampStore.LampConnected += onAction;
        }

        public void AddListenerToLampDisconnected(Action onAction)
        {
            _connectedLampStore.LampDisconnected += onAction;
        }

        // TODO replace placeholder method logic
        private void PullAllNewDailyDataFromLamp(Lamp lamp)
        {
            try
            {
                lamp.AddDailyData(
                    new LampDailyData(
                        new DateTime(
                            _random.Next(2020, 2023), _random.Next(1, 13), _random.Next(1, 31)
                        ), _random.Next(200, 500), _random.Next(200, 500)
                    )
                );
            }
            catch (LampDailyDataConflictException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        // TODO remove this method later
        private static string GenerateName(int len)
        { 
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
    }
}
