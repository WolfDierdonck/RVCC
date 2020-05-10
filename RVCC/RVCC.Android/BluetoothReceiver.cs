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

namespace RVCC.Droid
{
    public class BluetoothReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            if (action == "android.bluetooth.device.action.ACL_DISCONNECTED")
            {
                MainPage.BluetoothDisconnect();
            }
        }
    }
}