using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;

namespace Windows.WindowsSystem
{
    public class SystemWindow
    {
        private static Window _systemWindow;
        private static SystemWindow instance;
        private SystemWindow()
        {
            _systemWindow = new Window("Test Window", 800, 600);

            // load fonts 
            FontLibrary.InitFont();
        }

        public Window GetWindow()
        {
            return _systemWindow;
        }

        public static SystemWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemWindow();
                }
                return instance;
            }
        }
    }
}
