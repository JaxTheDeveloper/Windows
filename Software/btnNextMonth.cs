using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Software.Calendar;
using Windows.WinForms.FormComponent.Component;

namespace Windows.Software
{
    internal class btnNextMonth : Button
    {
        public override void OnClick(int externalX, int externalY)
        {
            base.OnClick(externalX, externalY);

            CalendarMain calendarWindow = (CalendarMain)_windowLink;

            calendarWindow.dateTime = calendarWindow.dateTime.AddMonths(1);

        }
    }
}
