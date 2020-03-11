using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RVCC;
using Plugin.Toast;

namespace RVCC
{

    public partial class MainPage : ContentPage
    {

        public static bool paired = false;

        private ISpeechToText _speechRecongnitionInstance;
        private string speechMessage;

        public static MainViewModel ViewModel => App.ViewModel;

        public MainPage()
        {
            InitializeComponent();

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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

            var tapCommandList = new TapGestureRecognizer();
            tapCommandList.Tapped += (s, e) =>
            {
                ViewCommandList();
            };
            commandListText.GestureRecognizers.Add(tapCommandList);

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
        }

        private void ViewCommandList()
        {
            Console.WriteLine("test");
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            speechMessage = char.ToUpper(args[0]) + args.Substring(1); ;
            ViewModel.CurCommandString = speechMessage;

            if (speechMessage == "go" || speechMessage == "stop" || speechMessage == "left" || speechMessage == "right") {
                ViewModel.RecognizeString = "(Recognized command)";
            }
            else
            {
                ViewModel.RecognizeString = "(Not a recognized command)";
            }

        }
        private void BluetoothClicked()
        {
            paired = true;
            ViewModel.BluetoothTextColor = Color.Gray;
            ViewModel.AudioTextColor = Color.Black;
            ViewModel.BluetoothTextString = "Pair Bluetooth (Paired)";
            ViewModel.AudioTextString = "Record Audio (Enabled)";
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
