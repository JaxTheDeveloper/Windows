using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowsSystem;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.FormFactory;
using Windows.WinForms.UIElement;

namespace Windows.WindowUI
{
    public class ApplicationWindow : GenericWindow
    {

        protected int _titleWidth;

        protected Button _btnMaximize;
        protected Button _btnMinimize;

        protected bool _hasMenuList = false;

        public enum NavigationalButtons
        {
            Maximize,
            Minimize
        }

        public ApplicationWindow(string title, int width, int height) : base(title, 30, 40, width, height, false) { }

        public ApplicationWindow(string title, int pos_x, int pos_y, int width, int height) : base(title, pos_x, pos_y, width, height, false)
        {
            Console.WriteLine($"Regular Application Window called!!!!!!, {Title}");
            _titleWidth = -1;
        }

        public override void InitWindowButtons()
        {
            base.InitWindowButtons();

            // default buttons (control button)
            // _posX + _width - 23, _posY + 3, 20, 20
            _btnMaximize = ButtonFactory.MakeButtonComponent<BtnMaximize>(this, "maximize", _width - 23, 3, 20, 20);
            _btnMaximize.RemainsSelected = false;

            // _posX + _width - 42, _posY + 3, 20, 20
            _btnMinimize = ButtonFactory.MakeButtonComponent<BtnMinimize>(this, "minimize", _width - 42, 3, 20, 20);
            _btnMinimize.RemainsSelected = false;
        }

        public override void Draw()
        {

            base.Draw();
            DrawNavigationalButtons();
            DrawWindowTitle(_fontSize);

            _btnMaximize.Draw(_posX, _posY);
            _btnMinimize.Draw(_posX, _posY);

            if (_hasMenuList)
            {
                SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame], _posX + 3, _posY + 22, _width - 6, 20);
                SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.MenuBar], _posX + 4, _posY + 23, _width - 8, 18);
            }

            foreach (var item in _winFormComponents.Values)
            {
                item.Draw(_posX, _posY);
            }
        }

        protected void DrawNavigationalButtons()
        {
            // minimize
            //SplashKit.FillRectangle(SplashKit.StringToColor("#C3C7CBFF"), _posX + _width - 42, _posY + 3, 20, 20);
            //SplashKit.DrawRectangle(Color.Black, _posX + _width - 42, _posY + 3, 20, 20);
            DrawMaximizeButton();
            DrawMinimizeButton();

            // maximize

        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            _btnMaximize.ProcessEvents(PosX, PosY);
            _btnMinimize.ProcessEvents(PosX, PosY);

            if (_hasMenuList)
            {


            }

            foreach (IWinForm form in _winFormComponents.Values)
            {
                form.ProcessEvents(_posX, _posY);
            }
        }

        protected void DrawMaximizeButton()
        {
            DrawNavigationalButton(_posX + _width - 23, _posY + 3, 20, 20, NavigationalButtons.Maximize);
        }

        protected void DrawMinimizeButton()
        {
            DrawNavigationalButton(_posX + _width - 42, _posY + 3, 20, 20, NavigationalButtons.Minimize);
        }

        public void DrawNavigationalButton(int posX, int posY, int width, int height, Enum @enum)
        {
            // color values stay unchanged (static)
            SplashKit.FillRectangle(SplashKit.StringToColor("#C3C7CBFF"), posX, posY, width, height);
            SplashKit.DrawRectangle(Color.Black, posX, posY, width, height);

            // adding dimension to the buttons


            SplashKit.FillRectangle(SplashKit.StringToColor("#868A8EFF"), posX + width - 3, posY + 1, 2, height - 2);
            SplashKit.FillRectangle(SplashKit.StringToColor("#868A8EFF"), posX + 1, posY + height - 3, width - 2, 2);
            SplashKit.DrawLine(Color.White, posX + 1, posY + 1, posX + 1, posY + height - 3);
            SplashKit.DrawLine(Color.White, posX + 1, posY + 1, posX + width - 3, posY + 1);
            DrawNavigationalArrows(posX, posY, @enum);
        }

        public static void DrawNavigationalArrows(int posX, int posY, Enum @enum)
        {
            switch (@enum)
            {
                case (NavigationalButtons.Maximize):
                    SplashKit.FillTriangle(Color.Black, posX + 9, posY + 7, posX + 6, posY + 10, posX + 12, posY + 10);
                    break;
                case (NavigationalButtons.Minimize):
                    SplashKit.FillTriangle(Color.Black, posX + 6, posY + 8, posX + 12, posY + 8, posX + 9, posY + 11);
                    break;
            }
        }

        public bool IsTextOverflow(ref int parentWidth, ref int titleWidth, int threshold = 60)
        {
            return parentWidth - 60 - titleWidth <= 0;
        }

        public override void DrawWindowTitle(int fontSize)
        {
            double textX;


            if (!_titleUpdated)
            {
                _displayTitle = _title;

                _titleWidth = MainHelper.GetEstimatedTextSize(_displayTitle, fontSize);

                while (_width - 60 - _titleWidth <= 0)
                {
                    _titleWidth = MainHelper.TruncateTextLeft(ref _displayTitle, fontSize);
                }

                _titleUpdated = true;

            }

            textX = _posX + 20 + (_width - 60 - _titleWidth) / 2;


            SplashKit.DrawText(_displayTitle, (_isFocused) ? Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.ActiveTitleBarText] :
                Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.InactiveTitleBarText],
                systemFont, fontSize, textX, _posY + 5);

        }
    }
}
