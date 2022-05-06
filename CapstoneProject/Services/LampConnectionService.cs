﻿using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Exceptions;
using CapstoneProject.Models;
using CapstoneProject.Stores;

namespace CapstoneProject.Services
{
    public class LampConnectionService
    {
        private readonly ConnectedLampStore _connectedLampStore;
        private readonly JsonDatabaseService _jsonDatabaseService;
        private readonly Random _random;
        public static string[] Ports;

        public LampConnectionService(
            ConnectedLampStore connectedLampStore, JsonDatabaseService jsonDatabaseService
        )
        {
            _connectedLampStore = connectedLampStore;
            _jsonDatabaseService = jsonDatabaseService;
            _random = new Random();
            Ports = new[] {"COM1", "COM2", "COM3"}; // _ports = SerialPort.GetPortNames();
        }

        // TODO Replace placeholder method logic.
        public async Task ConnectLamp(string selectedPort)
        {
            /*try
            {
                _connectedLampStore.SerialPort = new SerialPort(
                    selectedPort, 9600, Parity.None, 8, StopBits.One
                );
                _connectedLampStore.SerialPort.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
            
            // TODO Create the lamp instance via connection. 
            _connectedLampStore.Lamp = new Lamp(
                121, "LampName", true, 
                new TimeSpan(_random.Next(0,24), _random.Next(0,59), 0), 
                new TimeSpan(_random.Next(0,24), _random.Next(0,59), 0), 
                _random.Next(1,101), _random.NextDouble() >= 0.5
            );
            
            await PullDailyDataOfLamp(_connectedLampStore.Lamp);
            await _jsonDatabaseService.PullDataOfLamp(_connectedLampStore.Lamp);
            await _jsonDatabaseService.PushDataOfLamp(_connectedLampStore.Lamp);
            _connectedLampStore.Lamp.SortAllDailyData();
            _connectedLampStore.OnLampConnected(_connectedLampStore.Lamp);
        }

        // TODO Replace placeholder method logic.
        public void DisconnectLamp()
        {
            /*try
            {
                _connectedLampStore.SerialPort.Close();
                _connectedLampStore.SerialPort = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
            
            _connectedLampStore.Lamp = null;
            _connectedLampStore.OnLampDisconnected();
        }
        
        public Lamp GetConnectedLamp()
        {
            return _connectedLampStore.Lamp;
        }

        public bool IsLampConnected()
        {
            return _connectedLampStore.Lamp != null && _connectedLampStore.Lamp.ConnectionStatus;
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
        private async Task PullDailyDataOfLamp(Lamp lamp)
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

        public static string[] GetPorts()
        {
            return Ports;
        }
    }
}
