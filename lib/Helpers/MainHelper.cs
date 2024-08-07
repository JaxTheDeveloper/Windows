using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;

namespace Windows.lib.Helpers
{
    public static class MainHelper
    {
        public static bool IsPointInsideRectangle(float rectX, float rectY, float rectWidth, float rectHeight, float borderThickness = 1)
        {
            double x = SplashKit.MouseX();
            double y = SplashKit.MouseY();

            return x >= rectX - borderThickness && x <= rectX + rectWidth + borderThickness &&
                   y >= rectY - borderThickness && y <= rectY + rectHeight + borderThickness;
        }

        public static bool IsClickedInsideRectangle(float rectX, float rectY, float rectWidth, float rectHeight, float borderThickness = 1)
        {
            if (!SplashKit.MouseClicked(MouseButton.LeftButton)) return false;

            return IsPointInsideRectangle(rectX, rectY, rectWidth, rectHeight, borderThickness);
        }

        public static int GetEstimatedTextSize(string title, int fontSize)
        {
            int totalWidth = 0;
            int characterSpacing = 1;


            foreach (char c in title)
            {
                int charWidth = (char.IsUpper(c) | c == 'm') ? 10 : 6;
                if (characterSpacing == '.') { charWidth = 3; }
                totalWidth += charWidth + characterSpacing;
            }
            return totalWidth;
        }

        public static int TruncateTextLeft(ref string text, int fontSize)
        {
            if (text.Length > 4)
            {
                text = text.Substring(0, text.Length - 4);
            }

            text += "...";
            return MainHelper.GetEstimatedTextSize(text, fontSize);
        }

        public static int TruncateTextRight(ref string text, int fontSize)
        {
            if (text.Length > 6)
            {
                text = text.Substring(6);
            }

            text = ".." + text;
            return MainHelper.GetEstimatedTextSize(text, fontSize);
        }

        public static T PopAt<T>(this List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }

        public static KeyCode GetKeyPressed()
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if ((int)key < 32 || (int)key > 272)
                {
                }
                else if (SplashKit.KeyTyped(key))
                {
                    return key;
                }
            }

            if (SplashKit.KeyTyped(KeyCode.BackspaceKey))
            {
                return KeyCode.BackspaceKey;
            }
            return KeyCode.UnknownKey;
        }

        private static DateTime _lastClickTime;
        private const double _doubleClickThreshold = 250; // Adjust as needed

        public static bool IsDoubleClickedInsideRectangle(float rectX, float rectY, float rectWidth, float rectHeight, float borderThickness = 1)
        {
            if (IsClickedInsideRectangle(rectX, rectY, rectWidth, rectHeight, borderThickness))
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan deltaTime = currentTime - _lastClickTime;
                if (deltaTime.TotalMilliseconds < _doubleClickThreshold)
                {
                    _lastClickTime = DateTime.MinValue;
                    return true;
                }
                _lastClickTime = currentTime;
            }
            return false;
        }

        //public static 

        //if (mouseX >= posX - 3 || mouseX <= _posX + _width + 3)
        //    {
        //        return false;
        //    }

        //    if (mouseY<_posY - 3 || mouseY> _posY + 3)
        //    {
        //        return false;
        //    }
    }
}
