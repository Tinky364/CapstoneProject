using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CapstoneProject.Exceptions;
using CapstoneProject.Models;
using CapstoneProject.Stores;

namespace CapstoneProject.Services;

public class LampConnectionService
{
    private readonly ConnectedLampStore _connectedLampStore;
    private readonly DatabaseService _databaseService;
    private readonly Random _random;
    public static List<string> Ports;

    public LampConnectionService(
        ConnectedLampStore connectedLampStore, DatabaseService databaseService
    )
    {
        _connectedLampStore = connectedLampStore;
        _databaseService = databaseService;
        _random = new Random();
        Ports = SerialPort.GetPortNames().ToList();
        //Ports = new List<string> {"COM1", "COM2", "COM3"}; // Ports = SerialPort.GetPortNames().ToList();
    }

    // TODO Replace placeholder method logic.
    public async Task ConnectLamp(string selectedPort, int dummyLampId, string dummyLampName)
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
        int lampId = 132564; // int lampId = dummyLampId;
        string lampName = "Lamp132564"; // string lampName = dummyLampName;
        bool isLampNameChanged = false;
        string previousLampName = "";
        
        // Checks connected lamp is connected this PC before. If connected before, use the
        // database constructed lamp.
        if (_databaseService.IsDatabaseLampExist(lampId))
        {
            if (_databaseService.GetDatabaseLamp(lampId, out Lamp lamp))
            {
                if (lampName != lamp.Name)
                {
                    previousLampName = lamp.Name;
                    isLampNameChanged = true;
                }
                
                _connectedLampStore.Lamp = lamp;
                _connectedLampStore.Lamp.InitializeConnection(
                    lampName, new TimeSpan(20, 0, 0), new TimeSpan(6, 0, 0), 96, true
                );
            }
            else
            {
                MessageBox.Show(
                    $"Could not find database lamp {lampId}!", "Warning", MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }
        else // If not connected before, construct a new lamp and update the database.
        {
            _connectedLampStore.Lamp = new Lamp(
                lampId, lampName, true, 
                new TimeSpan(_random.Next(0,24), _random.Next(0,59), 0), 
                new TimeSpan(_random.Next(0,24), _random.Next(0,59), 0), 
                _random.Next(1,101), _random.NextDouble() >= 0.5
            );
            if (!_databaseService.AddDatabaseLamp(_connectedLampStore.Lamp))
            {
                MessageBox.Show(
                    $"Could not add database lamp {lampId}!", "Warning", MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }
            
        await Task.Delay(1000); // TODO Remove this line.
        await ReadDailyDataOfLamp(_connectedLampStore.Lamp);
        _connectedLampStore.Lamp.SortAllDailyData();
        if (isLampNameChanged)
            _databaseService.DeleteDatabaseLampOnLampNameChange(lampId, previousLampName);
        await _databaseService.WriteDatabaseLampData(_connectedLampStore.Lamp);
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
    private async Task ReadDailyDataOfLamp(Lamp lamp)
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

    public static List<string> GetPorts()
    {
        return Ports;
    }
}
