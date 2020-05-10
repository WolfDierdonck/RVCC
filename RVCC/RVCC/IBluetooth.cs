using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public interface IBluetooth
{
    void PairDevice(string name);
    void Send(string message);
    List<String> GetDevices();
}
