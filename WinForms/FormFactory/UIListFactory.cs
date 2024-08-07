using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormFactory
{
    public class UIListFactory
    {
        public static T CreateFormComponent<T>() where T : IWinForm
        {
            Console.WriteLine($"Creating a form...");
            return Activator.CreateInstance<T>();
        }

        public static UIList MakeListComponent<T>(IWindow window, string ctrlName, int x, int y, int width, int height, List<string> listItems, bool isVisible = true) where T : UIList
        {
            UIList btn = CreateFormComponent<T>();
            btn.CtrlName = ctrlName;
            btn.X = x;
            btn.Y = y;
            btn.Width = width;
            btn.Height = height;
            btn.IsVisible = isVisible;
            btn.ConfWindowLink = window;
            btn.ListItems = listItems;
            return btn;
        }
    }
}
