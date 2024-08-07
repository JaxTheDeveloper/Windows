using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowUI;
using Windows.WinForms.FormFactory;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormComponent.Component
{
    public class UIList : IWinForm, IClickable
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

        // list specifics
        protected Dictionary<string, Button> _listButtons;
        protected List<Button> _listButtonValues;
        protected int _maxListWidth;
        protected List<string> _listItems;


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

        public int MaxListWidth
        {
            get => _maxListWidth;
        }

        public Font FontName { get => _font; set => _font = value; }
        public bool isLoaded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<string> ListItems
        {
            get => _listItems;
            set => _listItems = value;
        }

        // constructor 
        public UIList()
        {

        }

        // click properties
        public bool RemainsSelected { get => _remainsSelected; set => _remainsSelected = value; }

        public List<IWinForm> GetWinFormsLinks => _winFormsLink;

        public IWindow ConfWindowLink { get => _windowLink; set => _windowLink = value; }

        // add list item
        public void AddListOption(string listItemName, Button button)
        {
            _listButtons.Add(listItemName, button);
        }

        public void Draw(int externalX, int externalY)
        {
            if (!_isLoaded)
            {
                return;
            }

            for (int i = 0; i < _listButtons.Count; i++)
            {
                _listButtonValues[i].Draw(externalX + _x, externalY + _y + 18 * i);
            }
        }

        public bool IsMouseOnButton(int externalX, int externalY)
        {
            throw new NotImplementedException();
        }

        public void OnClick(int externalX, int externalY)
        {
            throw new NotImplementedException();
        }

        public void ProcessButtonClicked(int externalX, int externalY)
        {
            throw new NotImplementedException();
        }

        public void ProcessEvents(int externalX, int externalY)
        {
            if (!_isLoaded)
            {
                int maxWidth = 0;
                foreach (string item in _listItems)
                {
                    maxWidth = Math.Max(maxWidth, MainHelper.GetEstimatedTextSize(item, 12));
                    //AddListOption("item", ListItemFactory.MakeListItemComponent<ListItem>(ConfWindowLink, this, CtrlName + "." + item, externalX + ));
                }
                _maxListWidth = maxWidth + 40;
                _isLoaded = true;
            }
        }

        public void UpdatePosition(int externalX, int externalY)
        {
            X = externalX; Y = externalY;
        }
    }
}
