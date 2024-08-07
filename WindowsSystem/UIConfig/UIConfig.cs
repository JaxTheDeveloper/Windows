using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.WindowsSystem.UIConfig
{
    public enum ScreenElements
    {
        Desktop,
        ApplicationWorkspace,
        WindowBackground,
        WindowText,
        MenuBar,
        MenuText,
        ActiveTitleBar,
        InactiveTitleBar,
        ActiveTitleBarText,
        InactiveTitleBarText,
        ActiveBorder,
        InactiveBorder,
        WindowFrame,
        ScrollBars,
        ButtonFace,
        ButtonShadow,
        ButtonText,
        ButtonHighlight,
        DisabledText,
        Highlight,
        HighlightedText
    }

    public enum WindowType
    {
        ApplicationWindow,
        DialogWindow
    }

    public enum BorderType
    {
        None,
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

}
