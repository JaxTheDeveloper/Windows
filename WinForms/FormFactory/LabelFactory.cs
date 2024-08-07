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
    public class LabelFactory : IFormFactory
    {
        public static T CreateFormComponent<T>() where T : IWinForm
        {
            Console.WriteLine($"Creating a form...{typeof(T).FullName}");
            return Activator.CreateInstance<T>();
        }

        public static Label MakeLabelComponent<T>(IWindow window, string ctrlName, int x, int y, string text, bool isVisible = true) where T : Label
        {
            Label lbl = CreateFormComponent<T>();
            lbl.CtrlName = ctrlName;
            lbl.X = x;
            lbl.Y = y;
            lbl.IsVisible = isVisible;
            lbl.ConfWindowLink = window;
            lbl.Text = text;
            return lbl;
        }
    }
}
