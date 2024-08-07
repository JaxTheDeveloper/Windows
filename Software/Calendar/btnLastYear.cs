using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib.Helpers;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;

namespace Windows.Software.Calendar
{
    internal class btnLastYear : Button
    {
        public override void OnClick(int externalX, int externalY)
        {
            base.OnClick(externalX, externalY);

            CalendarMain calendarWindow = (CalendarMain)_windowLink;

            calendarWindow.dateTime = calendarWindow.dateTime.AddYears(-1);

        }
    }
}
