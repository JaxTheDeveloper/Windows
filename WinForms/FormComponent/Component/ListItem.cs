using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.lib;
using Windows.lib.Helpers;
using Windows.WindowsSystem;
using Windows.WinForms.UIElement;

namespace Windows.WinForms.FormComponent.Component
{
    public class ListItem : Button
    {
        private Font systemFont;
        protected UIList _parentForm;

        public UIList ParentForm
        {
            get => _parentForm;
            set => _parentForm = value;
        }

        public ListItem()
        {
            IsVisible = true;
            isEnabled = true;
            systemFont = FontLibrary.GetSystemFont;
        }

        public ListItem(string ctrlName, int x, int y)
        {
            systemFont = FontLibrary.GetSystemFont;
        }

        public override void ProcessEvents(int externalX, int externalY)
        {
            _selected = false;
            if (MainHelper.IsClickedInsideRectangle(externalX + _x, externalY + _y, _parentForm.MaxListWidth, 18))
            {
                OnClick(externalX, externalY);
            }

            if (MainHelper.IsPointInsideRectangle(externalX + _x, externalY + _y, _parentForm.MaxListWidth, 18))
            {
                _selected = true;
            }
        }

        public override void Draw(int externalX, int externalY)
        {
            if (_selected)
            {
                SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.Highlight], externalX, externalY, 18, _parentForm.MaxListWidth);
                SplashKit.DrawText(_text, Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.HighlightedText], systemFont, 12, externalX + _x + 16, externalY + _y + 3);
                return;
            }

            SplashKit.FillRectangle(Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.MenuBar], externalX, externalY, 18, _parentForm.MaxListWidth);
            SplashKit.DrawText(_text, Theme.Instance.GetColorDictionary()[WindowsSystem.UIConfig.ScreenElements.MenuText], systemFont, 12, externalX + _x + 16, externalY + _y + 3);

        }

        public override void OnClick(int externalX, int externalY)
        {
            base.OnClick(externalX, externalY);
        }
    }
}
