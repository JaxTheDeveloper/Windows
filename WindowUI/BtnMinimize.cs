using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WinForms.FormComponent.Component;

namespace Windows.WindowUI
{
    public class BtnMinimize : Button
    {

        public override void Draw(int externalX, int externalY)
        {

        }

        public override void OnClick(int externalX, int externalY)
        {
            _windowLink.IsVisible = false;
            _windowLink.InitWindowButtons();

            Console.WriteLine("Minimized");
        }
    }
}
