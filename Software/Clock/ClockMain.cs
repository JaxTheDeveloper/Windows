using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.Software.Calendar;
using Windows.WindowsSystem;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.FormFactory;

namespace Windows.Software.Clock
{
    internal class ClockMain : ApplicationWindow, IWindow
    {
        private Label _time;
        private Label _date;
        private DateTime dateTime;
        private bool _isLoaded;

        public ClockMain() : this("Clock", 150, 200, 400, 500)
        {

        }

        public ClockMain(string title, int width, int height) : base(title, width, height)
        {
        }

        public ClockMain(string title, int pos_x, int pos_y, int width, int height) : base(title, pos_x, pos_y, width, height)
        {
            _isLoaded = false;

            dateTime = DateTime.Now;

            _time = LabelFactory.MakeLabelComponent<Label>(this, "lblTime", 65, 100, "00:00:00");
            _time.Font = FontLibrary.GetFont("Arial Bold");
            _time.FontSize = 30;

            _date = LabelFactory.MakeLabelComponent<Label>(this, "lblTime", 30, 135, "January 22, 2024");
            _date.Font = FontLibrary.GetFont("Arial");
            _date.FontSize = 24;

            _winFormComponents.Add("lblYear", _time);
            _winFormComponents.Add("lblMonth", _date);


            InitWindowButtons();
        }

        public override void Draw()
        {

            _width = 250;
            _height = 250;


            _isLoaded = true;
            base.Draw();
        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            dateTime = DateTime.Now;
            _time.Text = DateTime.Now.ToString("HH:mm:ss");
            _date.Text = DateTime.Now.ToString("MMMM dd, yyyy");
        }

        public override void InitWindowButtons()
        {
            _hasMenuList = false;
            InitWorkspaceWindow();
            base.InitWindowButtons();
            _isLoaded = true;
        }

        private void InitWorkspaceWindow()
        {
            if (!_isLoaded)
            {
                _width = 250;
                _height = 250;
                return;
            }

        }
    }
}
