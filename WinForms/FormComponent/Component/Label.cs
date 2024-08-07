using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.WindowsSystem;
using Windows.WindowUI;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormComponent.Component
{
    public class Label : IWinForm, IHasText
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
            set
            {
                _text = value;
                _displayText = _text;
            }
        }

        public Font FontName { get => _font; set => _font = value; }
        public bool isLoaded { get => _isLoaded; set => _isLoaded = value; }

        public List<IWinForm> GetWinFormsLinks => _winFormsLink;

        public IWindow ConfWindowLink { get => _windowLink; set => _windowLink = value; }

        public Label() : this("", 0, 0, "")
        {

        }

        public Label(string ctrlName, int x, int y, string text = "", bool isVisible = true)
        {
            _ctrlName = ctrlName;
            _x = x;
            _y = y;
            _text = text;
            _isVisible = isVisible;
            _font = FontLibrary.GetSystemFont;
            _color = Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.MenuText];
            _isEnabled = true;
        }

        public virtual void Draw(int externalX, int externalY)
        {
            if (!_isVisible || !_isEnabled) return;

            SplashKit.DrawText(_text, _color, _font, FontSize, _x + externalX, _y + externalY);
        }


        public virtual void ProcessEvents(int externalX, int externalY)
        {

        }

        public void UpdatePosition(int externalX, int externalY)
        {
            X = externalX; Y = externalY;
        }
    }
}
