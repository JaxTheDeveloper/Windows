using System;
using System.Drawing;
using Windows.lib;
using Windows.Software.ProgramManager;
using Windows.Software.TestApp;
using Windows.WindowsSystem;
using Windows.WindowUI;

namespace Windows
{
    public class Program
    {
        public static void Main()
        {

            //Window testWindow = new Window("Test Window", 800, 600);
            SystemWindow instanceWindow = SystemWindow.Instance;
            Window testWindow = instanceWindow.GetWindow(); 

            // create factory classes
            FactoryApplicationWindow factoryApplicationWindow = new FactoryApplicationWindow();

            // create a window handler (simpleton)
            WindowManager windowHandler = WindowManager.Instance;

            IWindow app = FactoryApplicationWindow.MakeWindow<ProgramManagerMain>("Program Manager", 30, 30, 512, 336);
            // will put this onto a separate thread on system.

            // add it inside the record
            windowHandler.RegisterWindow(app);

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                windowHandler.DrawWindows();
                windowHandler.ProcessEvents();

                Bitmap bitmap = SplashKit.LoadBitmap("icon", "../../../Software/TestApp/icon/TESTAPP.PNG");


                SplashKit.RefreshWindow(testWindow);
            } while (!testWindow.CloseRequested);

            testWindow.Dispose();
        }

        private void InitializeSystem()
        {
            // start a record to keep track of the windows
            
        }
    }

}

