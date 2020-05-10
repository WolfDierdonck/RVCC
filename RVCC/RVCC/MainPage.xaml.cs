using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RVCC;
using Plugin.Toast;
using Xamarin.Forms.Xaml;

namespace RVCC
{

    public partial class MainPage : ContentPage
    {

        public static bool paired = false;

        private ISpeechToText _speechRecongnitionInstance;
        private IBluetooth _bluetoothInstance;

        public static MainViewModel ViewModel => App.ViewModel;


        public MainPage()
        {
            InitializeComponent();

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
                _bluetoothInstance = DependencyService.Get<IBluetooth>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            BindingContext = ViewModel;

            #region gestures
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

            var tapCommandList = new TapGestureRecognizer();
            tapCommandList.Tapped += (s, e) =>
            {
                ViewCommandList();
            };
            commandListText.GestureRecognizers.Add(tapCommandList);
            #endregion gestures
        }

        public void DisplayResult(string message, string recognize)
        {
            ViewModel.RecognizeString = recognize;
            ViewModel.CurCommandString = message;
            if (recognize != "(Not a recognized command)")
            {
                Console.WriteLine(message);
                _bluetoothInstance.Send(message);
            }
        }

        private async void ViewCommandList()
        {
            StackLayout stackLayout = CommandList.CreateCommandList();

            Label close = new Label { Text = "Close", FontSize = 20, TextColor = Color.HotPink, VerticalOptions=LayoutOptions.End};

            var tapClose = new TapGestureRecognizer();
            tapClose.Tapped += (s, e) =>
            {
                Navigation.PopModalAsync();
            };
            close.GestureRecognizers.Add(tapClose);

            stackLayout.Children.Add(close);


            ContentPage contentPage = new ContentPage
            {
                Content = stackLayout,
            };

            await Navigation.PushModalAsync(contentPage);
        }

        private void BluetoothClicked()
        {
            List<String> devices = _bluetoothInstance.GetDevices();
            string name = "DSD TECH HC-05";

            if (devices.Contains(name) && paired == false)
            {
                _bluetoothInstance.PairDevice(name);
            }
        }

        public static void BluetoothConnect()
        {
            paired = true;
            ViewModel.BluetoothTextColor = Color.Gray;
            ViewModel.AudioTextColor = Color.Black;
            ViewModel.BluetoothTextString = "Pair Bluetooth (Paired)";
            ViewModel.AudioTextString = "Record Audio (Enabled)";
        }

        public static void BluetoothDisconnect()
        {
            paired = false;
            ViewModel.BluetoothTextColor = Color.FromHex("194C90");
            ViewModel.AudioTextColor = Color.Gray;
            ViewModel.BluetoothTextString = "Pair Bluetooth (Unpaired)";
            ViewModel.AudioTextString = "Record Audio (Disabled)";
        }

        private void RecordAudio()
        {
            if (paired == true)
            {
                try
                {
                    _speechRecongnitionInstance.StartSpeechToText();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
