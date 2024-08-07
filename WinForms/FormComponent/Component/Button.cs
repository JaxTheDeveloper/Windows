using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowsSystem;
using Windows.WindowsSystem.UIConfig;
using Windows.WindowUI;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormComponent.Component
{
    public class Button : IWinForm, IClickable, IHasText
    {
        protected string _ctrlName;
        protected bool _isVisible;
        protected int _width;
        protected int _height;
        protected int _x;
        protected int _y;
        protected Color _color;
        protected Font _font;
        protected int _fontSize;
        protected bool _fontBold;
        protected bool _fontItalic;
        protected bool _fontUnderline;
        protected bool _fontStrikeout;
        protected Color _fontColor;
        protected string _text;
        protected bool _isEnabled;
        protected bool _remainsSelected;
        protected bool _selected;

        // non-exposed properties
        protected bool _isLoaded;
        protected string _displayText;
        protected int _titleWidth;

        protected readonly int _minWidth = 10;
        protected readonly int _minHeight = 10;

        // linkers
        protected IWindow _windowLink; // compulsory linker
        private List<IWinForm> _winFormsLink = new List<IWinForm>();


        public string CtrlName
        {
            get => _ctrlName;
            set => _ctrlName = value;
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => _isVisible = value;
        }

        public bool isEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        public int Width
        {
            get => _width;
            set => _width = value >= 0 ? value : 0; // Ensure non-negative width
        }

        public int Height
        {
            get => _height;
            set => _height = value >= 0 ? value : 0; // Ensure non-negative height
        }

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public Font Font
        {
            get => _font;
            set => _font = value;
        }

        public int FontSize { get => _fontSize; set => _fontSize = value; }

        public bool FontBold
        {
            get => _fontBold;
            set
            {
                _fontBold = value;
                //UpdateFont();
            }
        }

        public bool FontItalic
        {
            get => _fontItalic;
            set
            {
                _fontItalic = value;
                //UpdateFont();
            }
        }

        public bool FontUnderline
        {
            get => _fontUnderline;
            set
            {
                _fontUnderline = value;
                //UpdateFont();
            }
        }

        public bool FontStrikeout
        {
            get => _fontStrikeout;
            set
            {
                _fontStrikeout = value;
                //UpdateFont();
            }
        }

        public Color FontColor
        {
            get => _fontColor;
            set => _fontColor = value;
        }

        public string Text
        {
            get => _text;
            set => _text = value;
        }

        public Font FontName { get => _font; set => _font = value; }
        public bool isLoaded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // click properties
        public bool RemainsSelected { get => _remainsSelected; set => _remainsSelected = value; }

        public List<IWinForm> GetWinFormsLinks => _winFormsLink;

        public IWindow ConfWindowLink { get => _windowLink; set => _windowLink = value; }

        // Helper method to update font styles
        private void UpdateFont()
        {
            //FontStyle style = FontStyle.Regular;
            //if (_fontBold) style |= FontStyle.Bold;
            //if (_fontItalic) style |= FontStyle.Italic;
            //if (_fontUnderline) style |= FontStyle.Underline;
            //if (_fontStrikeout) style |= FontStyle.Strikeout;

            //_font = new Font(_font.Name, _font.Size, style, _font.Unit);
        }

        // iwinform-specific init
        public Button() : this("", 24, 3, 35, 10, true)
        {
        }

        public Button(string ctrlName, int x, int y, int width, int height, bool isVisible = true) : this(ctrlName, x, y, width, height, "", isVisible)
        {

        }

        public Button(string ctrlName, int x, int y, int width, int height, string text = "", bool isVisible = true)
        {

            _color = Theme.Instance.GetColorDictionary()[ScreenElements.ButtonFace];

            _fontColor = Theme.Instance.GetColorDictionary()[ScreenElements.ButtonText];


            _ctrlName = ctrlName;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _text = text;
            _isVisible = isVisible;
            _isEnabled = true;

            _selected = false;

            _font = FontLibrary.GetSystemFont;

            _fontBold = false;
            _fontItalic = false;
            _fontUnderline = false;
            _fontStrikeout = false;
        }

        public virtual void Draw(int externalX, int externalY)
        {
            if (!_isVisible)
            {
                return;
            }

            DrawButtonBorder(ref externalX, ref externalY);
            DrawButtonFace(ref externalX, ref externalY);
            if (!_selected)
            {
                DrawButtonShadow(ref externalX, ref externalY);
            }
            DrawButtonText(ref externalX, ref externalY);
        }

        private static void NewMethod(ref int externalX, ref int externalY)
        {
            externalX *= 2;
            externalY *= 2;
        }

        private void DrawButtonText(ref int externalX, ref int externalY)
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

                while (this._width - 20 - this._titleWidth <= 0)
                {
                    if (_displayText.Length < 4)
                    { break; }
                    _titleWidth = MainHelper.TruncateTextLeft(ref _displayText, _fontSize);
                }

                _isLoaded = true;

            }

            textX = externalX + _x + 20 + (_width - 60 - _titleWidth) / 2;


            SplashKit.DrawText(_displayText, (!_isEnabled) ? Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.DisabledText] :
                Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.ButtonText],
                _font, _fontSize, textX, externalY + _y + (0.3 * _height - FontSize) / 2);
        }

        private void DrawButtonShadow(ref int externalX, ref int externalY)
        {
            SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[ScreenElements.ButtonShadow],
                _x + externalX + _width - 3, _y + externalY + 1, 2, _height - 2);
            SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[ScreenElements.ButtonShadow],
                _x + externalX + 1, _y + externalY + _height - 3, _width - 2, 2);

            SplashKit.DrawLine(Color.White,
                _x + externalX + 1, _y + externalY + 1, _x + externalX + 1, _y + externalY + _height - 3);
            SplashKit.DrawLine(Color.White,
                _x + externalX + 1, _y + externalY + 1, _x + externalX + _width - 3, _y + externalY + 1);

            //SplashKit.FillRectangle(SplashKit.StringToColor("#868A8EFF"), posX + width - 3, posY + 1, 2, height - 2);
            //SplashKit.FillRectangle(SplashKit.StringToColor("#868A8EFF"), posX + 1, posY + height - 3, width - 2, 2);
            //SplashKit.DrawLine(Color.White, posX + 1, posY + 1, posX + 1, posY + height - 3);
            //SplashKit.DrawLine(Color.White, posX + 1, posY + 1, posX + width - 3, posY + 1);
        }

        protected void DrawButtonFace(ref int externalX, ref int externalY)
        {
            if (_selected)
            {
                SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[ScreenElements.ButtonFace],
                _x + externalX + 1, _y + externalY + 1, _width - 2, _height - 2);
                return;
            }

            SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[ScreenElements.ButtonFace],
                _x + externalX + 1, _y + externalY + 1, _width - 2, _height - 2);
        }

        private void DrawButtonBorder(ref int externalX, ref int externalY)
        {
            SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[ScreenElements.WindowFrame],
                _x + externalX, _y + externalY, _width, _height);
        }

        // process button events (remember, this is 1992, hover didn't exist yet)
        public void ProcessButtonClicked(int externalX, int externalY)
        {
            if (_remainsSelected)
            {
                ProcessMouseWhenRemainSelected(externalX, externalY);
                return;
            }

            ProcessMouseWhenNoRemainSelected(externalX, externalY);
        }

        private void ProcessMouseWhenRemainSelected(int externalX, int externalY)
        {
            if (!SplashKit.MouseClicked(MouseButton.LeftButton)) { return; }

            _selected = !_selected;


        }

        private void ProcessMouseWhenNoRemainSelected(int externalX, int externalY)
        {
            if (!SplashKit.MouseDown(MouseButton.LeftButton)) { return; }
            _selected = IsMouseOnButton(externalX, externalY);
        }

        public bool IsMouseOnButton(int externalX, int externalY)
        {
            return MainHelper.IsPointInsideRectangle(externalX + _x, externalY + _y, _width, _height);
        }

        // buttons have different implementatiom
        public virtual void OnClick(int externalX, int externalY)
        {

            Console.WriteLine("yayyyy you are clicked");
        }

        private static DateTime _lastClickTime;
        private const double _doubleClickThreshold = 250;

        public virtual void ProcessEvents(int externalX, int externalY)
        {
            if (!_remainsSelected)
            {
                _selected = false;
            }

            if (!_isEnabled || !_isVisible)
            {
                return;
            }

            if (!IsMouseOnButton(externalX, externalY)) { return; }

            ProcessButtonClicked(externalX, externalY);

            if (MainHelper.IsClickedInsideRectangle(externalX + _x, externalY + _y, _width, _height))
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan deltaTime = currentTime - _lastClickTime;
                if (deltaTime.TotalMilliseconds < _doubleClickThreshold)
                {

                    return;
                }

                OnClick(externalX, externalY);
            }

        }

        public void UpdatePosition(int externalX, int externalY)
        {
            X = externalX; Y = externalY;
        }
    }
}
