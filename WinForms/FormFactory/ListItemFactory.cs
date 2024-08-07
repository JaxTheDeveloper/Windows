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
    public class ListItemFactory : IFormFactory
    {
        public static T CreateFormComponent<T>() where T : IWinForm
        {
            Console.WriteLine($"Creating a form...");
            return Activator.CreateInstance<T>();
        }

        public static ListItem MakeListItemComponent<T>(IWindow window, UIList list, string ctrlName, string listItemText, int x, int y, int width, int height, bool isVisible = true) where T : ListItem
        {
            ListItem btn = CreateFormComponent<T>();
            btn.CtrlName = ctrlName;
            btn.ParentForm = list;
            btn.X = x;
            btn.Y = y;
            btn.Width = width;
            btn.Height = height;
            btn.IsVisible = isVisible;
            btn.ConfWindowLink = window;
            btn.Text = listItemText;
            return btn;
        }
    }
}
