using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.WindowUI;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormFactory
{
    public interface IFormFactory
    {
        public virtual static T CreateFormComponent<T>() where T : IWinForm
        {
            Console.WriteLine($"Creating a form...");
            return Activator.CreateInstance<T>();
        }

    }
}
