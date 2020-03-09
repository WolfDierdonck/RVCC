using System.ComponentModel;
using Xamarin.Forms;
using System;
using RVCC;

namespace RVCC
{
    public class MainViewModel : BaseBind
    {
        public MainViewModel()
        {
            BluetoothTextColor = Color.FromHex("194C90");
            AudioTextColor = Color.Gray;
            BluetoothTextString = "Pair Bluetooth (Unpaired)";
            AudioTextString = "Record Audio (Disabled)";
        }

        private Color bluetoothTextColor;
        public Color BluetoothTextColor
        {
            get { return bluetoothTextColor; }
            set
            {
                SetProperty(ref bluetoothTextColor, value);
            }
        }

        private Color audioTextColor;
        public Color AudioTextColor
        {
            get { return audioTextColor; }
            set
            {
                SetProperty(ref audioTextColor, value);
            }
        }

        private string bluetoothTextString;
        public string BluetoothTextString
        {
            get { return bluetoothTextString; }
            set
            {
                SetProperty(ref bluetoothTextString, value);
            }
        }

        private string audioTextString;
        public string AudioTextString
        {
            get { return audioTextString; }
            set
            {
                SetProperty(ref audioTextString, value);
            }
        }
    }
}