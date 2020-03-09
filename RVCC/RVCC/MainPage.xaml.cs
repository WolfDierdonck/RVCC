using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RVCC;

namespace RVCC
{

    public partial class MainPage : ContentPage
    {

        public static MainViewModel ViewModel => App.ViewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;

            var tapBluetooth = new TapGestureRecognizer();
            tapBluetooth.Tapped += (s, e) =>
            {
                BluetoothClicked();
            };
            bluetoothText.GestureRecognizers.Add(tapBluetooth); 
            bluetoothImage.GestureRecognizers.Add(tapBluetooth);

            var tapMicrophone = new TapGestureRecognizer();
            tapMicrophone.Tapped += (s, e) =>
            {
                RecordAudio();
            };
            microphoneText.GestureRecognizers.Add(tapMicrophone);
            microphoneImage.GestureRecognizers.Add(tapMicrophone);
        }
        private void BluetoothClicked()
        {
            Console.WriteLine("test");
            ViewModel.BluetoothTextColor = Color.Gray;
            ViewModel.AudioTextColor = Color.Black;
            ViewModel.BluetoothTextString = "Pair Bluetooth (Paired)";
            ViewModel.AudioTextString = "Record Audio (Enabled)";
        }

        async void RecordAudio()
        {
            ViewModel.BluetoothTextColor = Color.FromHex("194C90");
            ViewModel.AudioTextColor = Color.Gray;
            ViewModel.BluetoothTextString = "Pair Bluetooth (Unpaired)";
            ViewModel.AudioTextString = "Record Audio (Disabled)";
        }
    }
}
