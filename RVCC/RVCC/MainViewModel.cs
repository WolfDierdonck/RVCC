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
            BluetoothTextColor = Color.Green;
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
    }
}