using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.WinForms.FormComponent.Component;

namespace Windows.WindowUI
{
    internal class BtnMaximize : Button
    {
        private bool _maximizeWindow;

        public override void Draw(int externalX, int externalY)
        {
            return;
        }

        public override void OnClick(int externalX, int externalY)
        {
            _windowLink.PosX = 0;
            _windowLink.PosY = 0;
            _windowLink.Width = 800;
            _windowLink.Height = 600;

            // re-organize the buttons
            _windowLink.InitWindowButtons();
        }
    }
}
