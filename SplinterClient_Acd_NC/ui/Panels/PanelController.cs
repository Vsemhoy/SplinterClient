using Autodesk.Windows;
using System.Collections.Generic;

namespace SplinterClient_Acd_NC.ui.Panels
{
    internal static class PanelController
    {
        private static readonly List<RibbonPanel> _panels = new();

        // Статические панели (инициализируются один раз)
        internal static MainPanel MainPanel { get; } = new MainPanel();
        internal static SettingsPanel SettingsPanel { get; } = new SettingsPanel();

        /// <summary>
        /// Инициализация всех панелей в указанную вкладку
        /// </summary>
        internal static void Initialize(RibbonTab ribbonTab)
        {
            // Добавляем основные панели
            AddPanel(ribbonTab, MainPanel.Panel);
            AddPanel(ribbonTab, SettingsPanel.Panel);

            // Можно добалить кастомные панели динамически
            // AddPanel(ribbonTab, new CustomPanel("Extra").Panel);
        }

        /// <summary>
        /// Безопасное добавление панели с проверкой дубликатов
        /// </summary>
        internal static void AddPanel(RibbonTab ribbonTab, RibbonPanel panel)
        {
            if (ribbonTab.Panels.Contains(panel))
                return;

            ribbonTab.Panels.Add(panel);
            _panels.Add(panel); // Для последующего управления
        }

        /// <summary>
        /// Удаление всех панелей при закрытии
        /// </summary>
        internal static void Terminate(RibbonControl ribbonControl)
        {
            foreach (var panel in _panels)
            {
                foreach (var tab in ribbonControl.Tabs)
                {
                    if (tab.Panels.Contains(panel))
                        tab.Panels.Remove(panel);
                }
            }
            _panels.Clear();
        }
    }
}