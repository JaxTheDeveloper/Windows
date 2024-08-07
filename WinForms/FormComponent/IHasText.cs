using System;
using System.Collections.Generic;
using Windows.lib;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.WinForms.UIElement
{
    public interface IHasText
    {
        public string Text { get; set; }
        public int FontSize { get; set; }
        public Font FontName { get; set; }
        public bool FontBold { get; set; }
        public bool FontItalic { get; set; }
        public bool FontUnderline { get; set; }
        public bool FontStrikeout { get; set; }
        public Color FontColor { get; set; }
    }
}
