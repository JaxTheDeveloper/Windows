using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.WindowUI;

namespace Windows.WinForms.UIElement
{
    public interface IWinForm
    {
        public string CtrlName { get; set; }
        public bool IsVisible { get; set; }
        public bool isEnabled { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public bool isLoaded { get; set; }

        public List<IWinForm> GetWinFormsLinks { get; }
        public IWindow ConfWindowLink { get; set; }



        // Unfortunately, SplashKit doesn't have a built-in concept of "inner screens" or "sub-windows" within a single window.
        // One workaround is to pass the point of which it is 
        void Draw(int externalX, int externalY);
        void ProcessEvents(int externalX, int externalY);
        void UpdatePosition(int externalX, int externalY);
    }
}
