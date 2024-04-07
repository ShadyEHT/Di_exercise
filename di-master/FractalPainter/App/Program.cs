using System;
using System.Linq;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();
                container.Bind(b =>
                    b.FromThisAssembly()
                        .SelectAllClasses()
                        .BindAllInterfaces()
                        .Configure(c => c.InSingletonScope()));

                container.Bind<Palette>()
                    .ToConstant(new Palette());
                
                var settingsManager = container.Get<SettingsManager>();
                container.Get<IImageHolder>().RecreateImage(settingsManager.Load().ImageSettings);
                
                var createDragonSettings = container.Get<Func<Random, DragonSettings>>();
                var dragonSettings = createDragonSettings(new Random(DateTime.Now.Second));
                container.Bind<DragonSettings>().ToConstant(dragonSettings);
                container.Unbind<IImageDirectoryProvider>();
                container.Unbind<ImageSettings>();
                container.Bind<ImageSettings>().ToConstant(settingsManager.Load().ImageSettings);
                container.Bind<IImageDirectoryProvider>().ToConstant(new AppSettings()
                {
                    ImageSettings = container.Get<ImageSettings>(),
                    ImagesDirectory = settingsManager.Load().ImagesDirectory
                });
                
                Application.EnableVisualStyles();
                Application.Run(new MainForm(container.GetAll<IUiAction>().ToArray(), settingsManager, container.Get<IImageHolder>()));
            }
            catch (Exception e)
            { 
                MessageBox.Show(e.Message);
            }
        }
    }
}