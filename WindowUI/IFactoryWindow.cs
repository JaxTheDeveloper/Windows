using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.WindowUI
{
    public interface IFactoryWindow
    {
        //Remember the GoF definition which says "....Factory method lets a class
        //defer instantiation to subclasses." Following method will create a Tiger
        //or Dog But at this point it does not know whether it will get a Dog or a
        //Tiger. It will be decided by the subclasses i.e.DogFactory or TigerFactory.
        //So, the following method is acting like a factory (of creation).

        public abstract IWindow CreateWindow(string title, int posX, int posY, int width, int height);

        public IWindow MakeWindow(string title, int posX, int posY, int width, int height)
        {
            Console.WriteLine("Creating a window...");
            IWindow currentWindow = CreateWindow(title, posX, posY, width, height);
            return currentWindow;
        }
    }
}
