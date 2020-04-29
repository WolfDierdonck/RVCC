using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using RVCC.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextImplementation))]

namespace RVCC.Droid
{

    public class SpeechToTextImplementation : ISpeechToText
    {
        private Activity _activity;
        public SpeechToTextImplementation()
        {
            _activity = CrossCurrentActivity.Current.Activity;

        }



        public void StartSpeechToText()
        {
            string rec = global::Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec == "android.hardware.microphone")
            {
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);

                _activity.StartActivityForResult(voiceIntent, 10);
            }
            else
            {
                throw new Exception("No mic found");
            }
        }

        public void StopSpeechToText()
        {

        }
    }
}