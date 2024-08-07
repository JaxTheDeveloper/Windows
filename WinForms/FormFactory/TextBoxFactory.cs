using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormFactory
{
    public class TextBoxFactory : IFormFactory
    {
        public static T CreateFormComponent<T>() where T : IWinForm
        {
            Console.WriteLine($"Creating a form...{typeof(T).FullName}");
            return Activator.CreateInstance<T>();
        }

        public static TextBox MakeTextboxComponent<T>(IWindow window, string ctrlName, int x, int y, int width, int height, string placeHolderValue, bool isVisible = true) where T : TextBox
        {
            TextBox txtBox = CreateFormComponent<T>();
            txtBox.ConfWindowLink = window;
            txtBox.Text = ctrlName;
            txtBox.Width = width;
            txtBox.Height = height;
            txtBox.X = x;
            txtBox.Y = y;
            txtBox.PlaceHolderValue = placeHolderValue;
            txtBox.IsVisible = isVisible;
            return txtBox;
        }
    }
}

