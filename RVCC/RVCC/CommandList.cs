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
    public class CommandList
    {
        public static StackLayout CreateCommandList()
        {
            StackLayout stackLayout = new StackLayout();
            stackLayout.Padding = 20;

            Label test = new Label { Text = "This page will be implemented in the future." };

            stackLayout.Children.Add(test);


            return stackLayout;
        }
    }
}
