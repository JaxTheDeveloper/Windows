using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.WindowsSystem;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.FormFactory;

namespace Windows.Software.Calendar
{
    public class CalendarMain : ApplicationWindow, IWindow
    {
        private bool _isLoaded;
        private Label _lblMonth;
        private Label _lblYear;

        // nav buttons
        private Button _btnLastYear;
        private Button _btnLastMonth;
        private Button _btnNextMonth;
        private Button _btnNextYear;
        private Button _btnPlanA;
        private Button _btnPlanB;
        private Button _btnToday;
        private int _windowX;
        private int _windowY;

        private int _marginLeft, _marginRight, _marginTop, _marginBottom;

        public DateTime dateTime;

        private bool _aggressiveDates;
        private bool _weekends;

        private int dividerWidth;
        private int cursorX;

        public bool Weekends
        {
            get => _weekends;
            set => _weekends = value;
        }

        public bool AggressiveDates
        {
            get => _aggressiveDates;
            set => _aggressiveDates = value;
        }

        //private Dictionary<int, string> _months = new Dictionary<int, string> { 1, "January"};
        private List<string> _daysOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private List<string> _agressiveDaysOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Octaday", "Enneaday", "Decaday" };

        private List<string> _currentCalendar;

        private List<Label> _dateLabel;

        public CalendarMain() : this("Calendar", 150, 200, 400, 500)
        {

        }

        public CalendarMain(string title, int width, int height) : base(title, width, height)
        {
        }

        public CalendarMain(string title, int pos_x, int pos_y, int width, int height) : base(title, pos_x, pos_y, width, height)
        {
            _isLoaded = false;

            _aggressiveDates = false;

            _weekends = true;

            _marginLeft = _marginRight = _marginTop = _marginBottom = 5;

            _dateLabel = new List<Label>();

            // initialize the calendar
            dateTime = DateTime.Now;
            string currentMonth = dateTime.ToString("MMMM");

            _lblYear = LabelFactory.MakeLabelComponent<Label>(this, "lbl_year", _windowX + _width - _marginRight - 100, _windowY + _marginTop, $"{dateTime.Year}");
            _lblYear.Font = FontLibrary.GetFont("Times New Roman Bold");
            _lblYear.FontSize = 36;

            _lblMonth = LabelFactory.MakeLabelComponent<Label>(this, "lbl_month", _windowX + _marginLeft, _windowY + _marginTop, $"{dateTime.ToString("MMMM")}");
            _lblMonth.Font = FontLibrary.GetFont("Times New Roman Bold");
            _lblMonth.FontSize = 36;

            CheckAggressiveCalendar();

            // navigational buttons
            _btnLastYear = ButtonFactory.MakeButtonComponent<btnLastYear>(this, "btnLastYear", _windowX + _marginLeft, _height - _marginBottom - 25, 35, 20);
            _btnLastYear.Text = "<<";

            _btnLastMonth = ButtonFactory.MakeButtonComponent<btnLastMonth>(this, "btnLastMonth", _windowX + _marginLeft + 40, _height - _marginBottom - 25, 35, 20);
            _btnLastMonth.Text = "<";

            _btnNextMonth = ButtonFactory.MakeButtonComponent<btnNextMonth>(this, "btnNextMonth", _windowX + _width + _marginLeft - 100, _height - _marginBottom - 25, 35, 20);
            _btnNextMonth.Text = ">";

            _btnNextYear = ButtonFactory.MakeButtonComponent<btnNextYear>(this, "btnNextYear", _windowX + _width + _marginLeft - 60, _height - _marginBottom - 25, 35, 20);
            _btnNextYear.Text = ">>";
            
            _btnPlanA = ButtonFactory.MakeButtonComponent<btnPlanA>(this, "btnPlanA", _windowX + _marginLeft + 80, _height - _marginBottom - 25, 200, 20);
            _btnPlanA.Text = "Plan A";

            _btnPlanB = ButtonFactory.MakeButtonComponent<btnPlanB>(this, "btnPlanB", _windowX + _marginLeft + 390, _height - _marginBottom - 25, 200, 20);
            _btnPlanB.Text = "Plan B";

            _btnToday = ButtonFactory.MakeButtonComponent<btnToday>(this, "btnToday", _windowX + _marginLeft + 285, _height - _marginBottom - 25, 100, 20);
            _btnToday.Text = "Today";

            

            cursorX = _windowX + _marginLeft;

            foreach (string date in _currentCalendar)
            {
                Label dateLabel = LabelFactory.MakeLabelComponent<Label>(this, $"dateOfWeek_{date}", cursorX, _windowY + 50, date);
                dateLabel.Font = FontLibrary.GetFont("Times New Roman");
                dateLabel.FontSize = 12;
                _dateLabel.Add(dateLabel);
                cursorX += dividerWidth;
            }


            _winFormComponents.Add("lblYear", _lblYear);
            _winFormComponents.Add("lblMonth", _lblMonth);
            _winFormComponents.Add("btnLastYear", _btnLastYear);
            _winFormComponents.Add("btnLastMonth", _btnLastMonth);
            _winFormComponents.Add("btnNextMonth", _btnNextMonth);
            _winFormComponents.Add("btnNextYear", _btnNextYear);
            _winFormComponents.Add("btnPlanA", _btnPlanA);
            _winFormComponents.Add("btnPlanB", _btnPlanB);
            _winFormComponents.Add("btnToday", _btnToday);

            InitWindowButtons();

        }

        public void CheckAggressiveCalendar()
        {
            _dateLabel.Clear();
            if (!_aggressiveDates)
            {

                _currentCalendar = _daysOfWeek;
                dividerWidth = (_width - _marginLeft - _marginRight - 16) / _currentCalendar.Count;
                return;
            }
            _currentCalendar = _agressiveDaysOfWeek;
            dividerWidth = (_width - _marginLeft - _marginRight - 16) / _currentCalendar.Count;
        }

        public override void InitWindowButtons()
        {
            _windowX = 9;
            _windowY = 30;

            _hasMenuList = false;
            InitWorkspaceWindow();
            base.InitWindowButtons();
        }

        private void InitWorkspaceWindow()
        {
            if (!_isLoaded)
            {
                _posX = 100;
                _posY = 200;
                _width = 700;
                _height = 350;
            }

        }

        public override void Draw()
        {

            if (!_isLoaded)
            {
                _posX = 100;
                _posY = 200;
                _width = 700;
                _height = 350;
                CheckAggressiveCalendar();
            }

            _isLoaded = true;

            base.Draw();

            foreach (var formItem in _winFormComponents.Values)
            {
                formItem.Draw(PosX, PosY);
            }

            foreach (var label in _dateLabel)
            {
                label.Draw(PosX, PosY);
            }

            // draw calendar numbers
            int cursorX = _windowX + _marginLeft;
            int cursorY = _windowY + _marginTop + 60;
            bool locatedDate = false;
            int tempDay = 1;
            DateTime tempDateTime = new DateTime(dateTime.Year, dateTime.Month, 1);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < _currentCalendar.Count; j++)
                {
                    SplashKit.DrawRectangle(Color.Black, PosX + cursorX, PosY + cursorY, dividerWidth, 40);
                    if (!AggressiveDates)
                    {
                        if (tempDateTime.DayOfWeek.ToString() == _currentCalendar[j])
                        {
                            SplashKit.DrawText(
                                tempDateTime.Day.ToString(),
                                (IsToday(tempDateTime)) ? Color.Red : (_weekends && (_currentCalendar[j] == "Saturday" || _currentCalendar[j] == "Sunday")) ? Color.Gray : Color.Black,
                                FontLibrary.GetFont("Arial Bold"), 30, PosX + cursorX + 3, PosY + cursorY + 3
                                );
                            tempDateTime = tempDateTime.AddDays(1);
                        }

                    }
                    
                    else if (_aggressiveDates)
                    {
                        if (tempDateTime.DayOfWeek.ToString() == _currentCalendar[j])
                        {
                            locatedDate = true;
                        }
                        
                        if (locatedDate)
                        {
                            SplashKit.DrawText(
                            tempDay.ToString(),
                            (IsToday(tempDateTime) ? Color.Red : Color.Black),
                            FontLibrary.GetFont("Arial Bold"), 30, PosX + cursorX + 3, PosY + cursorY + 3
                            );
                            tempDay += 1;
                        }
                    }
                    cursorX += dividerWidth;
                }
                cursorY += 40;
                cursorX = _windowX + _marginLeft;
            }

        }

        private static bool IsToday(DateTime tempDateTime)
        {
            return tempDateTime.Day == DateTime.Now.Day && tempDateTime.Month == DateTime.Now.Month && tempDateTime.Year == DateTime.Now.Year;
        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            _lblMonth.Text = dateTime.ToString("MMMM");
            _lblYear.Text = dateTime.Year.ToString();
        }


    }
}
