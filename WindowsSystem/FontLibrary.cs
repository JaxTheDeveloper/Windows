using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.lib;

namespace Windows.WindowsSystem
{
    public class FontLibrary
    {
        private static Dictionary<string, Font> _fonts = new Dictionary<string, Font>();

        public static void InitFont()
        {
            // initialize fonts
            _fonts.Add("System Bold", SplashKit.LoadFont("System Bold", "systemdata/fonts/vgasys.fon"));
            _fonts.Add("VGA Tahoma", SplashKit.LoadFont("VGA Tahoma", "systemdata/fonts/vgatahoma.ttf"));
            _fonts.Add("Times New Roman", SplashKit.LoadFont("Times New Roman", "systemdata/fonts/times new roman.ttf"));
            _fonts.Add("Times New Roman Bold", SplashKit.LoadFont("Times New Roman Bold", "systemdata/fonts/times new roman bold.ttf"));
            _fonts.Add("Arial", SplashKit.LoadFont("Arial", "systemdata/fonts/ARIAL.TTF"));
            _fonts.Add("Arial Bold", SplashKit.LoadFont("Arial Bold", "systemdata/fonts/ARIALBD 1.TTF"));
        }

        public static void AddFont(string fontName, Font font)
        {
            _fonts.Add(fontName, font);
        }

        public static Font GetFont(string fontName)
        {
            // attempts to get font, if it doesnt exists, fetch default font
            try
            {
                return _fonts[fontName];
            }
            catch 
            {
                return GetSystemFont;
            }
        }

        public static Font GetSystemFont
        {
            get => _fonts["System Bold"];
        }
    }
}
