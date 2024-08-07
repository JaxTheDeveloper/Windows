using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.WindowsSystem;
using Windows.WindowUI;
using Windows.WinForms.FormComponent.Component;
using Windows.WinForms.FormFactory;
using Windows.WinForms.UIElement;

namespace Windows.Software.TestApp
{
    public class TestAppMain : ApplicationWindow, IWindow
    {
        private Button _testButton;
        private Label _testLabel;
        private TextBox _testTextBox;

        private List<string> _menuBarList;

        public TestAppMain() : this("Test Drive", 150, 200, 400, 500)
        {
            
        }

        public TestAppMain(string title, int pos_x, int pos_y, int width, int height) : base(title, pos_x, pos_y, width, height)
        {
            _testButton = ButtonFactory.MakeButtonComponent<Button>(this, "test", 30, 50, 70, 23);
            _testButton.Text = "a test";

            _bitmap = SplashKit.LoadBitmap("icon", "../../../Software/TestApp/icon/icon.PNG");

            _testLabel = LabelFactory.MakeLabelComponent<Label>(this, "lblText", 30, 90, "i dont know");

            _testTextBox = TextBoxFactory.MakeTextboxComponent<TextBox>(this, "txtbox", 30, 120, 70, 23, "idk lol adsada dsadsadsd");

            _menuBarList = new List<string>();

            _winFormComponents.Add("A test", _testButton);
            _winFormComponents.Add("lblText", _testLabel);
            _winFormComponents.Add("txtbox", _testTextBox);

            _hasMenuList = true;

        }

        public override void InitWindowButtons()
        {
            base.InitWindowButtons();
            //Console.WriteLine("Hello world!");

            //_testButton = ButtonFactory.MakeButtonComponent<Button>(this, "test", 30, 50, 70, 23);
            //_testButton.Text = "a test";

            //_testLabel = LabelFactory.MakeLabelComponent<Label>(this, "lblText", 30, 90, "i dont know");

            //_testTextBox = TextBoxFactory.MakeTextboxComponent<TextBox>(this, "this is what", 30, 100, 70, 23, "idk lol");

        }

        public override void Draw()
        {
            base.Draw();
            foreach (IWinForm item in _winFormComponents.Values)
            {
                item.Draw(_posX, _posY);
               
            }
        }
    }
}
