using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormComponent
{
    public interface ILinkWinForms
    {
        private static List<IWinForm> _winFormsLink = new List<IWinForm>();

        public static List<IWinForm> WinFormsLink
        {
            get { return _winFormsLink; }
        }
    }
}
