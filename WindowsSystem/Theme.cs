using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;

namespace Windows.WindowsSystem
{
    // simpleton implemetation. class is sealed to prevent accidental inheritance
    public sealed class Theme
    {
        private static Theme ?_instance;
        private static object _sLock = new object();
        private Dictionary<UIConfig.ScreenElements, Color> _themeData;

        private Theme()
        {
            _themeData = new Dictionary<UIConfig.ScreenElements, Color>();

            // static theme assignment (will move to dynamic later)
            _themeData.Clear();
            LoadDefaultTheme();
            
        }

        public static Theme Instance
        {
            get
            {
                if (_instance == null) ;
                // locks the thread access to prevent data racing (reads theme data from json in the future)
                lock (_sLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Theme();
                        //_instance._themeData = ThemeManager.LoadTheme("...");
                    }
                }

                return _instance;
            }
        }

        public Dictionary<UIConfig.ScreenElements, Color> GetColorDictionary()
        {
            return _themeData;
        }

        public void LoadDefaultTheme()
        {
            _themeData.Add(UIConfig.ScreenElements.Desktop, SplashKit.StringToColor("#C3C7CBFF"));
            _themeData.Add(UIConfig.ScreenElements.ApplicationWorkspace, SplashKit.StringToColor("#FFFFFFFF"));
            _themeData.Add(UIConfig.ScreenElements.WindowBackground, SplashKit.StringToColor("#FFFFFFFF"));
            _themeData.Add(UIConfig.ScreenElements.WindowText, SplashKit.StringToColor("#000000FF"));
            _themeData.Add(UIConfig.ScreenElements.MenuBar, SplashKit.StringToColor("#FFFFFFFF"));
            _themeData.Add(UIConfig.ScreenElements.MenuText, SplashKit.StringToColor("#000000FF"));
            _themeData.Add(UIConfig.ScreenElements.ActiveTitleBar, SplashKit.StringToColor("#0000AAFF"));
            _themeData.Add(UIConfig.ScreenElements.InactiveTitleBar, SplashKit.StringToColor("#FFFFFFFF"));
            _themeData.Add(UIConfig.ScreenElements.ActiveTitleBarText, SplashKit.StringToColor("#FFFFFFFF"));
            _themeData.Add(UIConfig.ScreenElements.InactiveTitleBarText, SplashKit.StringToColor("#000000FF"));
            _themeData.Add(UIConfig.ScreenElements.ActiveBorder, SplashKit.StringToColor("#C3C7CBFF"));
            _themeData.Add(UIConfig.ScreenElements.InactiveBorder, SplashKit.StringToColor("#C3C7CBFF"));
            _themeData.Add(UIConfig.ScreenElements.WindowFrame, SplashKit.StringToColor("#000000FF"));
            _themeData.Add(UIConfig.ScreenElements.ScrollBars, SplashKit.StringToColor("#C3C7CBFF"));
            _themeData.Add(UIConfig.ScreenElements.ButtonFace, SplashKit.StringToColor("#C3C7CBFF"));
            _themeData.Add(UIConfig.ScreenElements.ButtonShadow, SplashKit.StringToColor("#868A8EFF"));
            _themeData.Add(UIConfig.ScreenElements.ButtonText, SplashKit.StringToColor("#000000FF"));
            _themeData.Add(UIConfig.ScreenElements.ButtonHighlight, SplashKit.StringToColor("#FFFFFFFF"));
            _themeData.Add(UIConfig.ScreenElements.DisabledText, SplashKit.StringToColor("#C3C7CBFF"));
            _themeData.Add(UIConfig.ScreenElements.Highlight, SplashKit.StringToColor("#0000AAFF"));
            _themeData.Add(UIConfig.ScreenElements.HighlightedText, SplashKit.StringToColor("#FFFFFFFF"));
        }

    }
}
