using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App
{
    public class MainForm : Form
    { 
        public MainForm(IUiAction[] actions, SettingsManager settingsManager, IImageHolder imageHolder)
        {
            var imageSettings = settingsManager.Load().ImageSettings;
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            
            imageHolder.RecreateImage(settingsManager.Load().ImageSettings);
            (imageHolder as Control).Dock = DockStyle.Fill;
            Controls.Add(imageHolder as Control);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}