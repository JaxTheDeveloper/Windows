using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.WinForms.UIElement
{
    public interface IClickable
    {
        public bool RemainsSelected { get; set; }

        public bool IsMouseOnButton(int externalX, int externalY);

        public void ProcessButtonClicked(int externalX, int externalY);

        public void OnClick(int externalX, int externalY); // -> do something
    }
}
