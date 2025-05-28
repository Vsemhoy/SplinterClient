using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Autodesk.AutoCAD.ApplicationServices;


namespace SplinterClient_Acd_NC.ui.Style
{
    internal class LocalThemeManager
    {
        public static bool IsDarkTheme
        {
            get
            {
                try
                {
                    // Получаем значение как short (Int16)
                    short themeValue = (short)Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("COLORTHEME");
                    return themeValue == 1; // 1 - тёмная тема, 0 - светлая
                }
                catch
                {
                    return false; // Fallback на светлую тему
                }
            }
        }

        public static void ApplyTheme(Window window)
        {
            if (window == null) return;

            if (IsDarkTheme)
            {
                // Тёмная тема AutoCAD
                window.Background = new SolidColorBrush(Color.FromRgb(0x2D, 0x2D, 0x30));
                window.Foreground = Brushes.White;

                // Опционально: стили для контролов
                var darkBrush = new SolidColorBrush(Color.FromRgb(0x3F, 0x3F, 0x46));
                darkBrush.Freeze();
                window.Resources[SystemColors.ControlBrushKey] = darkBrush;
            }
            else
            {
                // Светлая тема
                window.Background = new SolidColorBrush(Color.FromRgb(0xF0, 0xF0, 0xF0));
                window.Foreground = Brushes.Black;
            }
        }
    }
}
