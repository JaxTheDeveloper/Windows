using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib.Helpers;
using Windows.lib;
using Windows.WindowsSystem;

namespace Windows.WinForms.FormComponent.Component
{
    public class TextBoxLabel : Label
    {
        public TextBoxLabel() : this("", 0, 0)
        {
        }

        public TextBoxLabel(string ctrlName, int x, int y) : base(ctrlName, x, y)
        {
             _isLoaded = false;
        }

        public override void ProcessEvents(int externalX, int externalY)
        {
            if (!isLoaded) { CalculateTextboxText(ref externalX, ref externalY); }
            base.ProcessEvents(externalX, externalY);
            
        }

        public override void Draw(int externalX, int externalY)
        {
            if (!_isVisible || !_isEnabled) return;

            SplashKit.DrawText(_displayText, _color, _font, 12, _x + externalX, _y + externalY);
        }

        private void CalculateTextboxText(ref int externalX, ref int externalY)
        {
            double textX;

            if (_text == "")
            {
                return;
            }

            if (!_isLoaded)
            {

                _displayText = _text;

                _titleWidth = MainHelper.GetEstimatedTextSize(_displayText, fontSize: 12);

                while (this._width - this._titleWidth <= 0)
                {
                    //if (_displayText.Length < 10)
                    //{ break; }
                    _titleWidth = MainHelper.TruncateTextRight(ref _displayText, _fontSize);
                }

                _isLoaded = true;

            }
        }
    }
}
