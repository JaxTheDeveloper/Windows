using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WindowUI;
using Windows.WinForms.UIElement;
using Windows.lib;
using Windows.WindowsSystem;
using Windows.WinForms.FormFactory;
using System.ComponentModel.Design;
using Windows.lib.Helpers;

namespace Windows.WinForms.FormComponent.Component
{
    public class TextBox : IWinForm, IClickable, IHasText
    {
        protected Label _textLabel;

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

        // textBox specifics:
        protected string _textBoxValue;
        protected string _placeHolderValue;
        protected bool _isFocused;


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

        public string PlaceHolderValue
        {
            get => _placeHolderValue;
            set => _placeHolderValue = value;
        }

        public string TextBoxValue
        {
            get => _textBoxValue;
            set => _textBoxValue = value;
        }

        public TextBox() : this("", 0, 0, 0, 0, "")
        {

        }

        public TextBox(string ctrlName, int x, int y, int width, int height, string placeholderValue, bool isVisible = true)
        {
            _ctrlName = ctrlName;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _placeHolderValue = placeholderValue;
            _isVisible = isVisible;

            isEnabled = true;
            isLoaded = false;

            _font = FontLibrary.GetSystemFont;

        }

        public Font FontName { get => _font; set => _font = value; }
        public bool isLoaded { get => _isLoaded; set => _isLoaded = value; }

        // click properties
        public bool RemainsSelected { get => _remainsSelected; set => _remainsSelected = value; }

        public List<IWinForm> GetWinFormsLinks => _winFormsLink;

        public IWindow ConfWindowLink { get => _windowLink; set => _windowLink = value; }

        public void Draw(int externalX, int externalY)
        {
            if (_isLoaded)
            {
                _textLabel.Draw(externalX + _x, externalY + _y);
            }
            SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame], externalX + _x, externalY + _y, _width, _height);
        }

        public void ProcessEvents(int externalX, int externalY)
        {
            if (!_isLoaded)
            {
                _textLabel = LabelFactory.MakeLabelComponent<TextBoxLabel>(_windowLink, $"{_ctrlName}.label", 5, 2, "");
                _textLabel.Width = _width;
                _textLabel.Height = _height;
                isLoaded = true;
                return;
            }

            _textLabel.ProcessEvents(externalX, externalY);

            if (MainHelper.IsClickedInsideRectangle(externalX + _x, externalY + _y, _width, _height))
            {

                _selected = true;
            }
            else if (SplashKit.MouseClicked(MouseButton.LeftButton) && !MainHelper.IsClickedInsideRectangle(externalX + _x, externalY + _y, _width, _height))
            {
                _selected = false;
            }

            if (_selected)
            {
                KeyCode PressedKey = MainHelper.GetKeyPressed();
                // backspace case
                if (PressedKey == KeyCode.BackspaceKey)
                {
                    try
                    {
                        _textLabel.Text = _textLabel.Text.Substring(0, _textLabel.Text.Length - 1);
                        _textLabel.isLoaded = false;
                    }
                    catch
                    {

                    }

                }
                else if (PressedKey != KeyCode.UnknownKey)
                {
                    Console.WriteLine((char)PressedKey);
                    _textLabel.Text += (char)PressedKey;
                    _textLabel.isLoaded = false;
                }
            }


        }

        public bool IsMouseOnButton(int externalX, int externalY)
        {
            return MainHelper.IsPointInsideRectangle(externalX + _x, externalY + _y, _width, _height);
        }

        public void ProcessButtonClicked(int externalX, int externalY)
        {

        }

        public void OnClick(int externalX, int externalY)
        {

        }

        public void UpdatePosition(int externalX, int externalY)
        {
            X = externalX; Y = externalY;
        }
    }
}
