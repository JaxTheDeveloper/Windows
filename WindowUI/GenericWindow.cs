using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowsSystem;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.FormFactory;
using Windows.WinForms.UIElement;


namespace Windows.WindowUI
{
    public abstract class GenericWindow : IWindow
    {
        protected lib.Font systemFont = FontLibrary.GetSystemFont;
        protected int _fontSize = 12;

        protected string _title;
        protected string _displayTitle;
        protected int _posX;
        protected int _posY;
        protected int _width;
        protected int _height;
        protected bool _isFocused;
        protected bool _canLoseFocus;
        protected int _minWidth = 35;
        protected int _minHeight = 35;

        public Bitmap _bitmap;

        protected bool _isVisible;

        protected readonly Guid _guid;

        protected Button _btnControlMenu;

        protected bool _titleUpdated;

        protected Dictionary<string, IWinForm> _winFormComponents;

        //winForm factories


        public GenericWindow(string title, int pos_x, int pos_y, int width, int height, bool focusPriority)
        {
            _guid = Guid.NewGuid();

            _title = title;
            _displayTitle = title;
            _posX = pos_x;
            _posY = pos_y;
            _width = width;
            _height = height;

            _isFocused = true;
            _canLoseFocus = focusPriority;

            _isVisible = true;

            _titleUpdated = false;

            // init winform record
            _winFormComponents = new Dictionary<string, IWinForm>();
            InitWindowButtons();
        }

        public virtual void InitWindowButtons()
        {
            // default buttons (control button)
            _btnControlMenu = ButtonFactory.MakeButtonComponent<BtnCtrlMenu>(this, "ctrlButton", 3, 3, 20, 20);
            _btnControlMenu.RemainsSelected = true;
        }

        public virtual void ProcessEvents()
        {
            if (!_isVisible) { return; }

            _btnControlMenu.ProcessEvents(_posX, _posY);

        }


        public bool IsVisible { get => _isVisible; set => _isVisible = value; }

        public virtual void Draw()
        {
            DrawWindowBackground();
            DrawWindowBorders();
            DrawWindowControl();
            DrawTitleBar();
            DrawWorkspace();

            // draw control menu button

            _btnControlMenu.Draw(_posX, _posY);

        }

        private void DrawWorkspace()
        {
            SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.ApplicationWorkspace],
                _posX + 4, _posY + 24, _width - 8, _height - 28);
        }

        private void DrawWindowBackground()
        {
            SplashKit.FillRectangle(SplashKit.StringToColor("#C3C7CBFF"), _posX, _posY, _width, _height);
        }

        public void MoveWindow(double deltaX, double deltaY)
        {
            _posX += (int)deltaX;
            _posY += (int)deltaY;
            InitWindowButtons();
        }

        public void ResizeWindow(double posx, double posy, double width, double height)
        {

            _width = _width + (int)width < _minWidth ? _width : _width + (int)width;
            _height = _height + (int)height < _minHeight ? _height : _height + (int)height; ;
            _posX += (int)posx;
            _posY += (int)posy;

            _titleUpdated = false;
            InitWindowButtons();
        }

        protected void DrawWindowBorders()
        {

            SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX, _posY, _width, _height);

            SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + 3, _posY + 3, _width - 6, _height - 6);

            // border decorations (colors unchanged)
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX, _posY + 22, _posX + 2, _posY + 22);
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + 22, _posY, _posX + 22, _posY + 2);
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX, _posY + _height - 22, _posX + 2, _posY + _height - 22);
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + 23, _posY + _height - 3, _posX + 23, _posY + _height - 1);

            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + _width - 23, _posY, _posX + _width - 23, _posY + 2);
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + _width - 3, _posY + 22, _posX + _width - 1, _posY + 22);
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + _width - 22, _posY + _height - 3, _posX + _width - 22, _posY + _height - 1);
            SplashKit.DrawLine(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + _width - 3, _posY + _height - 23, _posX + _width - 1, _posY + _height - 23);

        }

        protected void DrawWindowControl()
        {
            // this cannot be altered by themes
            SplashKit.DrawRectangle(Color.Black, _posX + 3, _posY + 3, 20, 20);

        }

        protected void DrawTitleBar()
        {
            SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.WindowFrame],
                _posX + 22, _posY + 3, _width - 22 - 3, 20);

            SplashKit.FillRectangle((_isFocused) ? Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.ActiveTitleBar] :
                Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.InactiveTitleBar],
                _posX + 23, _posY + 4, _width - 22 - 5, 18);
        }

        public bool IsPointInsideWindow()
        {
            float mouseX = SplashKit.MouseX();
            float mouseY = SplashKit.MouseY();

            if (mouseX < _posX || mouseX > _posX + _width)
            {
                return false;
            }

            if (mouseY < _posY || mouseY > _posY + _height)
            {
                return false;
            }

            return true;
        }

        public bool IsPointInsideTitleBar()
        {
            float mouseX = SplashKit.MouseX();
            float mouseY = SplashKit.MouseY();

            if (mouseX < _posX + 20 || mouseX > _posX + _width)
            {
                return false;
            }

            if (mouseY < _posY + 3 || mouseY > _posY + 23)
            {
                return false;
            }

            return true;
        }

        public bool IsPointInsideTopBorder()
        {
            return MainHelper.IsPointInsideRectangle(_posX, _posY, _width, 0, 3);
        }

        public bool IsPointInsideBottomBorder()
        {
            return MainHelper.IsPointInsideRectangle(_posX, _posY + _height, _width, 0, 3);
        }

        public bool IsPointInsideLeftBorder()
        {
            return MainHelper.IsPointInsideRectangle(_posX, _posY, 0, _height, 3);
        }

        public bool IsPointInsideRightBorder()
        {
            return MainHelper.IsPointInsideRectangle(_posX + _width, _posY, 0, _height, 3);
        }

        public abstract void DrawWindowTitle(int fontSize);

        public void ChangeWindowFocus(bool focus)
        {
            throw new NotImplementedException();
        }

        // invoke this upon closing, i.e. to save important data
        public virtual void CloseWindow()
        {
            WindowManager.Instance.UnregisterWindow();
        }

        public void IsMouseClickedInside()
        {
            throw new NotImplementedException();
        }

        // close window calls its destructor
        ~GenericWindow()
        {

        }

        public bool IsFocused
        {
            get => _isFocused;
            set => _isFocused = value;
        }

        public bool CanLoseFocus
        {
            get => _canLoseFocus;
            set => _canLoseFocus = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }
        public int PosX { get => _posX; set => _posX = value; }
        public int PosY { get => _posY; set => _posY = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }

        public Guid WindowID => _guid;

        public Bitmap GetBitmap()
        {
            return _bitmap;
        }
    }
}
