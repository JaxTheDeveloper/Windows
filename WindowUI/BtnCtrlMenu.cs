using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.WindowsSystem.UIConfig;
using Windows.WindowsSystem;
using Windows.WinForms.FormComponent.Component;

namespace Windows.WindowUI
{
    public class BtnCtrlMenu : Button
    {

        public override void Draw(int externalX, int externalY)
        {
            if (_selected)
            {
                SplashKit.FillRectangle(SplashKit.StringToColor("#AAAAAAFF"),
                _x + externalX + 1, _y + externalY + 1, _width - 2, _height - 2);
            } else
            {
                SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[ScreenElements.ButtonFace],
                _x + externalX + 1, _y + externalY + 1, _width - 2, _height - 2);
            }

            SplashKit.FillRectangle(SplashKit.StringToColor("#868A8EFF"), externalX + 7, externalY + 12, 13, 3);
            SplashKit.FillRectangle(Color.White, externalX + 6, externalY + 11, 13, 3);
            SplashKit.DrawRectangle(Color.Black, externalX + 6, externalY + 11, 13, 3);
        }

        public override void OnClick(int externalX, int externalY)
        {
            base.OnClick(externalX, externalY);
            WindowManager.Instance.UnregisterWindow();
        }
    }
}
