using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RVCC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
            Console.WriteLine("yeet");
        }

        async void RecordAudio()
        {
            Console.WriteLine("yeet2");
        }
    }
}
