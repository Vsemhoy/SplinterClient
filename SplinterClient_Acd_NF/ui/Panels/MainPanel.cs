using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplinterClient_Acd_NF.ui.Panels
{
    public class MainPanel
    {
        public RibbonPanel Panel { get; }
        public RibbonPanelSource PanelSource { get; }

        public MainPanel(string name = "Main Panel")
        {
            Panel = new RibbonPanel();
            PanelSource = new RibbonPanelSource
            {
                Name = "MainPanel",
                Title = name
            };

            AddButtons();
            Panel.Source = PanelSource;
        }

        private void AddButtons()
        {
            // Кнопка "Add"
            var addButton = new RibbonButton
            {
                Text = "Add",
                CommandParameter = "AddDemoButton",
                ShowText = true,
                ShowImage = false,
                // Image = LoadIcon("add_icon.png") // Добавь иконку
            };
            PanelSource.Items.Add(addButton);

            // Кнопка "Remove"
            var removeButton = new RibbonButton
            {
                Text = "Remove",
                CommandParameter = "RemoveDemoButton",
                ShowText = true
            };
            PanelSource.Items.Add(removeButton);
        }
    }
}


