using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplinterClient_Acd_NC.ui.Panels
{
    internal class MainPanel
    {
        public RibbonPanel Panel;
        public RibbonPanelSource rps;
        public MainPanel(string name = "Main Panel")
        {
            this.Panel = new RibbonPanel();
            this.rps = new RibbonPanelSource();

            this.rps.Name = "MainPanel";
            this.rps.Title = name;

            this.AddButtons();

        }

        private void AddButtons()
        {
            // Add the "AddDemoButton" button to the panel
            RibbonButton buttonAdd = new RibbonButton();
            buttonAdd.Text = "Add";
            buttonAdd.CommandParameter = "AddDemoButton";
            buttonAdd.ShowText = true;
            buttonAdd.ShowImage = false;
            this.rps.Items.Add(buttonAdd);
            this.rps.Items.Add(buttonAdd);
            this.rps.Items.Add(buttonAdd);


            // Add the "RemoveDemoButton" button to the panel
            RibbonButton buttonRemove = new RibbonButton();
            buttonRemove.Text = "Remove";
            buttonRemove.CommandParameter = "RemoveDemoButton";
            buttonRemove.ShowText = true;
            buttonRemove.ShowImage = false;
            this.rps.Items.Add(buttonRemove);

            this.Panel.Source = this.rps;
        }
    }
}
