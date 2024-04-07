using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private DragonPainter _painter;  
        private DragonSettings _dragonSettings;

        public DragonFractalAction(DragonPainter painter, DragonSettings dragonSettings)
        {
            _painter = painter;
            _dragonSettings = dragonSettings;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            // редактируем настройки:
            SettingsForm.For(_dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            _painter.Paint();
        }
    }
}