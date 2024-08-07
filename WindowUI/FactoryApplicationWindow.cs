using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.UIElement;

namespace Windows.WindowUI
{
    public class FactoryApplicationWindow : IFactoryWindow
    {
        public IWindow CreateWindow(string title, int posX, int posY, int width, int height)
        {
            return new ApplicationWindow(title, posX, posY, width, height);
        }

        public static T CreateWindow<T>() where T : IWindow
        {
            Console.WriteLine($"Creating a window...");
            return Activator.CreateInstance<T>();
        }

        public static IWindow MakeWindow<T>(string title, int posX, int posY, int width, int height, bool overridePos = false) where T : ApplicationWindow
        {
            ApplicationWindow app = CreateWindow<T>();
            app.Title = title;
            app.Width = width;
            app.Height = height;
            app.PosX = posX;
            app.PosY = posY;
            return app;
        }

    }
}
