using System.ComponentModel;
using Xamarin.Forms;
using System;

namespace NFCProject.Pages
{
    public class ReadViewModel : BaseBind
    {
        public ReadViewModel()
        {
            PageBackColor = Color.White;
        }

        private Color pageBackColor;
        public Color PageBackColor
        {
            get { return pageBackColor; }
            set
            {
                SetProperty(ref pageBackColor, value);
            }
        }
    }
}