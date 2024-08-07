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
    public class ButtonFactory : IFormFactory
    {
        public static T CreateFormComponent<T>() where T : IWinForm
        {
            Console.WriteLine($"Creating a form...");
            return Activator.CreateInstance<T>();
        }

        public static Button MakeButtonComponent<T>(IWindow window, string ctrlName, int x, int y, int width, int height, bool isVisible = true) where T : Button
        {
            Button btn = CreateFormComponent<T>();
            btn.CtrlName = ctrlName;
            btn.X = x;
            btn.Y = y;
            btn.Width = width;
            btn.Height = height;
            btn.IsVisible = isVisible;
            btn.ConfWindowLink = window;
            return btn;
        }
    }
}
