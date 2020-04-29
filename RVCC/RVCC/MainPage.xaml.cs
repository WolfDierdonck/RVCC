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

            #region messagingCenters
            MessagingCenter.Subscribe<IMessageSender, string>(this, "Left", (sender, args) => {
                ViewModel.RecognizeString = "(Recognized command)";
                ViewModel.CurCommandString = "Left";

                int[] vars = new int[2] { 1, 3 };

                try
                {
                    _bluetoothInstance.SendMessage(vars);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "Stop", (sender, args) => {
                ViewModel.RecognizeString = "(Recognized command)";
                ViewModel.CurCommandString = "Stop";
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "Go", (sender, args) => {
                ViewModel.RecognizeString = "(Recognized command)";
                ViewModel.CurCommandString = "Go";
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "Go left", (sender, args) => {
                ViewModel.RecognizeString = "(Recognized command)";
                ViewModel.CurCommandString = "Go left";
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "Right", (sender, args) => {
                ViewModel.RecognizeString = "(Recognized command)";
                ViewModel.CurCommandString = "Right";
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "Go right", (sender, args) => {
                ViewModel.RecognizeString = "(Recognized command)";
                ViewModel.CurCommandString = "Go right";
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "Nothing", (sender, args) =>
            {
                ViewModel.RecognizeString = "(Not a recognized command)";
                ViewModel.CurCommandString = args;
            });

            #endregion messagingCenters
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
