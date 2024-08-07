using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.WindowUI
{
    public class FactoryDialogWindow
    {
        public static T CreateWindow<T>() where T : IWindow
        {
            Console.WriteLine($"Creating a window...");
            return Activator.CreateInstance<T>();
        }

        public static IWindow MakeDialog<T>(string title, int posX, int posY, int width, int height, bool overridePos = false) where T : DialogWindow
        {
            DialogWindow app = CreateWindow<T>();
            app.Title = title;
            app.Width = width;
            app.Height = height;
            app.PosX = posX;
            app.PosY = posY;
            return app;
        }


    }
}
