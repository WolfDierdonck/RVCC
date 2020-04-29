using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using RVCC.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SendBluetooth))]

namespace RVCC.Droid
{
    public class SendBluetooth : IBluetooth
    {

        public SendBluetooth()
        {

        }

        public bool SendMessage(int[] vars)
        {
            Console.WriteLine("[{0}]", string.Join(", ", vars));

            bool success = true;

            //Connect to Bluetooth

            //Send data

            if (success == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}