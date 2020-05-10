using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Plugin.BLE.Android;
using RVCC.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SendBluetooth))]

namespace RVCC.Droid
{
    public class SendBluetooth : IBluetooth
    {

        private CancellationTokenSource _cancellationToken { get; set; }
        public string MessageToSend { get; set; }

        public bool disconnect = false;

        public SendBluetooth()
        {
            _cancellationToken = new CancellationTokenSource();
        }

        public void PairDevice(string name)
        {
            Console.WriteLine(name);
            Task.Run(async () => await Connect(name));
        }

        public List<String> GetDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            List <String> devices = new List<String>();
            foreach (var bondedDevice in adapter.BondedDevices)
            {
                devices.Add(bondedDevice.Name);
            }

            return devices;
        }

        private async Task Connect(string name)
        {
            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            BluetoothSocket bthSocket = null;

            _cancellationToken = new CancellationTokenSource();

            while (_cancellationToken.IsCancellationRequested == false)
            {

                try
                {
                    Thread.Sleep(250);

                    adapter = BluetoothAdapter.DefaultAdapter;

                    Console.Write("Trying to connect to " + name + ". ");

                    foreach (var bondedDevice in adapter.BondedDevices)
                    {

                        if (bondedDevice.Name.ToUpper().IndexOf(name.ToUpper()) >= 0)
                        {
                            Console.Write("Found " + bondedDevice.Name);
                            device = bondedDevice;
                            break;
                        }
                    }

                    if (device == null)
                        Console.Write("Not found");
                    else
                    {
                        Console.WriteLine();
                        UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");
                        bthSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);

                        if (bthSocket != null)
                        {
                            Console.WriteLine("test");
                            await bthSocket.ConnectAsync();

                            if (bthSocket.IsConnected)
                            {
                                Console.WriteLine("Connected");
                                RVCC.MainPage.BluetoothConnect();
                                var mReader = new Java.IO.InputStreamReader(bthSocket.InputStream);
                                var buffer = new Java.IO.BufferedReader(mReader);


                                while (_cancellationToken.IsCancellationRequested == false)
                                {

                                    if (MessageToSend != null)
                                    {
                                        Console.WriteLine(MessageToSend);
                                        var chars = MessageToSend.ToCharArray();
                                        var bytes = new List<byte>();

                                        foreach (var character in chars)
                                        {
                                            bytes.Add((byte)character);
                                        }

                                        await bthSocket.OutputStream.WriteAsync(bytes.ToArray(), 0, bytes.Count);

                                        MessageToSend = null;
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    Console.Write(ex.Message);
                }
                finally
                {
                    if (bthSocket != null)
                        bthSocket.Close();

                    device = null;
                    adapter = null;
                }
            }
        }
        public void Send(string message)
        {
            if (MessageToSend == null)
                MessageToSend = message;
        }
    }
}