using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowsSystem.UIConfig;
using Windows.WindowUI;

namespace Windows.WindowsSystem
{
    public class WindowManager
    {
        // lazy implementation (dropped)
        //private static readonly Lazy<WindowManager> instance = new Lazy<WindowManager>(() => new WindowManager());

        private static WindowManager? _instance;
        private static object _sLock = new object();
        private List<IWindow> _inactiveWindows;
        private List<IWindow> _invisibleWindows;
        private IWindow _activeWindow;
        private bool _isDraggingWindow;
        private bool _isResizingWindow;
        private Point2D _mouseOriginPosition;
        private double deltaX = 0;
        private double deltaY = 0;

        private double dPosX = 0;
        private double dPosY = 0;
        private double dHeight = 0;
        private double dWidth = 0;

        private bool _isLoaded = false;


        // border type (defaults to 0)
        private List<BorderType> _borderType;

        private WindowManager()
        {
            _inactiveWindows = new List<IWindow>();
            _invisibleWindows = new List<IWindow>();
            _isDraggingWindow = false;
            _isResizingWindow = false;
            _borderType = new List<BorderType>();

            _isLoaded = true;
        }

        public int WindowCount
        {
            get => _inactiveWindows.Count + (_activeWindow == null ? 0 : 1);
        }

        public static WindowManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WindowManager();
                }
                return _instance;
            }

        }

        public void ProcessEvents()
        {

            if (!_activeWindow.IsVisible && _isLoaded)
            {
                try
                {
                    _invisibleWindows.Add(_activeWindow);

                    _activeWindow = _inactiveWindows.PopAt(_inactiveWindows.Count - 1);
                    _activeWindow.IsFocused = true;
                }
                catch
                {
                    return;
                }
            }

            if (_activeWindow == null) { return; }
            if (!_activeWindow.IsVisible) { return; }

            _activeWindow.ProcessEvents();

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                ProcessChangeActiveWindow();
            }

            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                ProcessResizeWindow();
                ProcessDragWindow();
            }

            if (SplashKit.MouseUp(MouseButton.LeftButton))
            {
                ProcessMoveDraggedWindow();
                ProcessMoveResizeWindow();
            }

        }

        private void ProcessResizeWindow()
        {
            if (_isDraggingWindow) return;

            if ((_activeWindow.IsPointInsideTopBorder() | _activeWindow.IsPointInsideBottomBorder() |
                _activeWindow.IsPointInsideLeftBorder() | _activeWindow.IsPointInsideRightBorder()) & !_isResizingWindow)
            {
                // get border type
                _borderType = GetBorderType();

                _isResizingWindow |= true;
                _mouseOriginPosition = SplashKit.MousePosition();

            }

            deltaX = (SplashKit.MouseX() - _mouseOriginPosition.X);
            deltaY = (SplashKit.MouseY() - _mouseOriginPosition.Y);

            // dependency injection pattern
            ProcessDrawResizingBox();

            // minimum UWP window size (view documentation)
            dWidth = Math.Max(_activeWindow.Width + dWidth, 192) - _activeWindow.Width;
            dHeight = Math.Max(_activeWindow.Height + dHeight, 48) - _activeWindow.Height;

            // draws the ghost resize box
            SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[UIConfig.ScreenElements.WindowFrame],
                        _activeWindow.PosX + dPosX, _activeWindow.PosY + dPosY, _activeWindow.Width + dWidth, _activeWindow.Height + dHeight);

        }

        private void ProcessDrawResizingBox()
        {
            if (_borderType.Contains(BorderType.Top))
            {
                dPosY = deltaY;
                dHeight = -deltaY;
            }

            if (_borderType.Contains(BorderType.Bottom))
            {
                dHeight = deltaY;
            }

            if (_borderType.Contains(BorderType.Left))
            {
                dPosX = deltaX;
                dWidth = -deltaX;
            }

            if (_borderType.Contains(BorderType.Right))
            {
                dWidth = deltaX;
            }


        }
        private List<BorderType> GetBorderType()
        {
            bool isOnTop = _activeWindow.IsPointInsideTopBorder();
            bool isOnBottom = _activeWindow.IsPointInsideBottomBorder();
            bool isOnLeft = _activeWindow.IsPointInsideLeftBorder();
            bool isOnRight = _activeWindow.IsPointInsideRightBorder();

            if (isOnTop) _borderType.Add(BorderType.Top);
            if (isOnBottom) _borderType.Add(BorderType.Bottom);
            if (isOnLeft) _borderType.Add(BorderType.Left);
            if (isOnRight) _borderType.Add(BorderType.Right);

            return _borderType;
        }

        private void ProcessMoveResizeWindow()
        {
            if (_isDraggingWindow || !_isResizingWindow) return;

            if (_isResizingWindow)
            {
                _isResizingWindow = false;
                _borderType.Clear();

                _activeWindow.ResizeWindow(dPosX, dPosY, dWidth, dHeight);

                dPosX = 0;
                dPosY = 0;
                dHeight = 0;
                dWidth = 0;

            }
        }

        private void ProcessMoveDraggedWindow()
        {
            if (!_isDraggingWindow || _isResizingWindow) return;
            _activeWindow.MoveWindow(deltaX, deltaY);
            deltaX = deltaY = 0;
            _isDraggingWindow = false;
        }

        private void ProcessDragWindow()
        {
            if (_isResizingWindow) return;

            if (_activeWindow.IsPointInsideTitleBar())
            {
                _isDraggingWindow = true;
                _mouseOriginPosition = SplashKit.MousePosition();
            }

            //if (!_isDraggingWindow) ProcessChangeActiveWindow();

            if (_isDraggingWindow)
            {
                deltaX = (SplashKit.MouseX() - _mouseOriginPosition.X);
                deltaY = (SplashKit.MouseY() - _mouseOriginPosition.Y);

                SplashKit.DrawRectangle(Theme.Instance.GetColorDictionary()[UIConfig.ScreenElements.WindowFrame],
                    _activeWindow.PosX + deltaX, _activeWindow.PosY + deltaY, _activeWindow.Width, _activeWindow.Height);
            }

        }

        public void UpdateWindow()
        {
            _activeWindow.IsFocused = true;
            foreach (IWindow window in _inactiveWindows)
            {
                window.IsFocused = false;
            }
        }

        private IWindow FetchEvents(Predicate<IWindow> checkPredicate)
        {
            IWindow? resultWindow;
            resultWindow = _inactiveWindows.FindLast(checkPredicate);
            return resultWindow;
        }

        private void ProcessChangeActiveWindow()
        {
            if (_activeWindow == null) return;

            if (_isDraggingWindow || _isResizingWindow)
            {
                return;
            }

            if (_activeWindow.IsPointInsideWindow()) return;

            IWindow nextActiveWindow = FetchEvents(window => window.IsPointInsideWindow());

            if (nextActiveWindow != null)
            {
                _inactiveWindows.Remove(nextActiveWindow);
                RegisterWindow(nextActiveWindow);
            }

        }

        // observer register
        public void RegisterWindow(IWindow window)
        {
            if (_activeWindow == null)
            {
                _activeWindow = window;
                return;
            }

            // add the previously active window
            _activeWindow.IsFocused = false;
            _inactiveWindows.Add(_activeWindow);
            _activeWindow = window;

            UpdateWindow();
        }

        public void UnregisterWindow()
        {
            _activeWindow = _inactiveWindows.LastOrDefault();
            try
            {
                _inactiveWindows.PopAt(_inactiveWindows.Count - 1);
            }
            catch (Exception ex)
            {
                SplashKit.CloseWindow("Test Window");
                
            }
        }

        public void DrawWindows()
        {
            if (_activeWindow == null) return;

            SplashKit.ClearScreen(SplashKit.StringToColor("#C3C7CBFF"));

            foreach (IWindow window in _inactiveWindows)
            {
                if (window.IsVisible)
                {
                    window.Draw();
                }

            }

            if (_activeWindow.IsVisible) _activeWindow.Draw();
        }
    }
}
