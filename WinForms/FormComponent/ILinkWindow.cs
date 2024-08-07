using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WindowUI;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormComponent
{
    public interface ILinkWindow
    {

        public List<IWindow> WinFormsLink
        {
            get;
        }

    }
}
