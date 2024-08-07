using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;

namespace Windows.Software.ProgramManager
{
    public interface IApplet
    {
        public Bitmap Bitmap { get; set; }
        public void Draw(int externalX, int externalY);
        public void ProcessEvents(int externalX, int externalY);

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }
    }
}
