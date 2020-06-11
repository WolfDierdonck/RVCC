using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RVCC;
using Android.Content;
using Android.Speech;
using Xamarin.Forms;
using System.Collections.Generic;
using Android.Bluetooth;

namespace RVCC.Droid
{
    [Activity(Label = "RVCC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IMessageSender
    {
        private readonly int VOICE = 10;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            BluetoothReceiver receiver = new BluetoothReceiver();
            IntentFilter disconnect = new IntentFilter(BluetoothDevice.ActionAclDisconnected);
            RegisterReceiver(receiver, disconnect);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {

            if (requestCode == VOICE)
            {
                if (resultCode == Result.Ok)
                {
                    IList<String> tempMatches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (tempMatches.Count != 0)
                    {

                        List<String> matches = new List<String>(tempMatches);
                        matches = matches.ConvertAll(d => d.ToLower());
                        MainPage mainPage = new MainPage();
                        List<String> commands = new List<String>() { "forwards", "backwards", "go", "stop", "left", "go left", "right", "go right" };

                        bool foundCommand = false;

                        foreach (String command in matches)
                        {
                            if (commands.Contains(command)) {
                                mainPage.DisplayResult(command, "(Recognized Command)");
                                foundCommand = true;
                            }
                        }
                        if (matches.Contains("right") || matches.Contains("height") || matches.Contains("hertz") || matches.Contains("heights") || matches.Contains("light") || matches.Contains("lights") || matches.Contains("crates"))
                        {
                            mainPage.DisplayResult("right", "(Recognized Command)");
                            foundCommand = true;
                        }
                        else if (matches.Contains("go right") || matches.Contains("go height") || matches.Contains("go hertz") || matches.Contains("go heights") || matches.Contains("go light") || matches.Contains("go lights"))
                        {
                            mainPage.DisplayResult("go right", "(Recognized Command)");
                            foundCommand = true;
                        }
                        if (foundCommand == false)
                        {
                            mainPage.DisplayResult(matches[0], "(Not a recognized command)");
                        }   
                    }

                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}