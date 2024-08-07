using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib.Helpers;
using Windows.lib;
using Windows.WindowsSystem;

namespace Windows.WindowUI
{
    public class DialogWindow : GenericWindow
    {
        protected int _titleWidth;

        public DialogWindow() : this("dialog", 0, 0, 100, 100)
        {
            
        }

        public DialogWindow(string title, int width, int height) : base(title, 30, 40, width, height, false) { }

        public DialogWindow(string title, int pos_x, int pos_y, int width, int height) : base(title, pos_x, pos_y, width, height, false)
        {
            Console.WriteLine($"Regular Dialog Window called!!!!!!, {Title}");
            _titleWidth = -1;
        }

        public override void Draw()
        {

            base.Draw();

            DrawWindowTitle(_fontSize);

            foreach (var item in _winFormComponents.Values)
            {
                item.Draw(_posX, _posY);
            }
        }

        public override void DrawWindowTitle(int fontSize)
        {
            Console.WriteLine(Title);
            double textX;


            if (!_titleUpdated)
            {
                _displayTitle = _title;

                _titleWidth = MainHelper.GetEstimatedTextSize(_displayTitle, fontSize);

                while (_width - 20 - _titleWidth <= 0)
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
