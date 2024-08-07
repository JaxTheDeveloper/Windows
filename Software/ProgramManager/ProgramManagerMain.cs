using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.Software.Calendar;
using Windows.Software.Clock;
using Windows.Software.TestApp;
using Windows.WindowsSystem;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;

namespace Windows.Software.ProgramManager
{
    public struct ApplicationProgram
    {

    }

    public class ProgramManagerMain : ApplicationWindow
    {
        private int _maximumAppHorizontalGrids;
        private int _windowX;
        private int _windowY;

        private bool _isLoaded;

        private List<IApplet> _applets = new List<IApplet>();

        public ProgramManagerMain() : this("Program Manager", 32, 27, 512, 336)
        {

        }

        public ProgramManagerMain(string title, int width, int height) : base(title, width, height)
        {
        }

        public ProgramManagerMain(string title, int pos_x, int pos_y, int width, int height) : base(title, pos_x, pos_y, width, height)
        {
            _bitmap = SplashKit.LoadBitmap("icon", "../../../Software/ProgramManager/icon/PROGM.PNG");

            _applets.Add(new Applet<TestAppMain>("Test Drive"));
            _applets.Add(new Applet<ClockMain>("Clock"));
            _applets.Add(new Applet<CalendarMain>("Calendar"));

            InitWindowButtons();

        }

        public override void InitWindowButtons()
        {
            _windowX = _posX + 9;
            _windowY = _posY + 50;
            _hasMenuList = true;
            InitWorkspaceWindow();
            base.InitWindowButtons();
        }

        private void InitWorkspaceWindow()
        {
            _maximumAppHorizontalGrids = (int)Math.Floor((double)_width / 74);

            int baseDrawCursorX = _windowX;
            int drawCursorX = baseDrawCursorX;
            int drawCursorY = _windowY;

            for (int i = 0; i < _applets.Count; i++)
            {
                _applets[i].X = drawCursorX;
                _applets[i].Y = drawCursorY;
                drawCursorX += 74;
                if ((i + 1) % _maximumAppHorizontalGrids == 0)
                {
                    drawCursorX = baseDrawCursorX;
                    drawCursorY += 74;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();

            foreach (var applet in _applets)
            {
                applet.Draw(_posX, _posY);
            }
        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            foreach (var applet in _applets)
            {
                applet.ProcessEvents(_posX, _posY);
            }
        }

        public override void CloseWindow()
        {
            IWindow dialog = FactoryDialogWindow.MakeDialog<DialogWindow>("Exit Windows?", 175, 175, 450, 150);
            WindowManager.Instance.RegisterWindow(dialog);
        }

    }
}
