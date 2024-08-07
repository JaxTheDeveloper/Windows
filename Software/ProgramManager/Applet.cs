using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowsSystem;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.UIElement;

namespace Windows.Software.ProgramManager
{
    internal class Applet<T> : Button, IApplet where T : ApplicationWindow
    {
        protected Bitmap _bitmap;

        private static int _iconOrder = 1;

        public Bitmap Bitmap { get => _bitmap; set => _bitmap = value; }

        public Applet(string text)
        {

            _width = _height = 64;
            _text = text;
            _displayText = _text;
            _font = FontLibrary.GetSystemFont;

            // get bitmap image from type logic
            string type = typeof(T).Name;
            type = "../../../Software/" + type.Substring(0, type.Length - 4) + "/icon/icon.PNG";
            _bitmap = SplashKit.LoadBitmap(Guid.NewGuid().ToString(), type);
            _iconOrder += 1;
        }

        public override void Draw(int externalX, int externalY)
        {
            //base.Draw(externalX, externalY);
            SplashKit.DrawBitmap(_bitmap, _x + 16, _y);
            SplashKit.DrawText(_text, Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.ButtonText],
                _font, _fontSize, _x, _y + 32);
        }

        public override void ProcessEvents(int externalX, int externalY)
        {

            if (MainHelper.IsDoubleClickedInsideRectangle(X, Y, _width, _height))
            {
                Console.WriteLine(typeof(T).Name);

                WindowManager.Instance.RegisterWindow(FactoryApplicationWindow.MakeWindow<T>(_text, 30, 30, 512, 336));
            }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

    }
}