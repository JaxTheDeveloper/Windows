using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.WindowUI
{
    public interface IWindow
    {
        // every window needs to draw itself
        public void Draw();

        // every window can be dragged
        public void MoveWindow(double deltaX, double deltaY);

        // every window can be closed
        public void CloseWindow();

        // every window can 
        public void IsMouseClickedInside();

        // check if mouse is inside window
        public bool IsPointInsideWindow();
        public bool IsPointInsideTitleBar();

        // check if mouse is inside each border
        public bool IsPointInsideTopBorder();
        public bool IsPointInsideBottomBorder();
        public bool IsPointInsideLeftBorder();
        public bool IsPointInsideRightBorder();

        // windows can be resized
        public void ResizeWindow(double posx, double posy, double width, double height);

        // windows can process their own events 
        public void ProcessEvents();

        // windows re-update their button locations
        public void InitWindowButtons();

        // every windows needs to know if it's focused
        public bool IsFocused
        {
            get; set;
        }

        // every windows needs to know if it can lose focus
        public bool CanLoseFocus
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }
        
        public Guid WindowID { get;}

        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsVisible { get; set; }

    }
}
