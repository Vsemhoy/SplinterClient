using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Ribbon;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using SplinterClient_Acd_NC.ui.Panels;
using SplinterClient_Acd_NC.ui.Windows.RoomManager;
using SplinterCoreNTS;
using SplinterCoreNTS.Entity;
using System.Diagnostics;
using System.Runtime.InteropServices;



[assembly: CommandClass(typeof(SplinterClient_Acd_NC.Commander))]



namespace SplinterClient_Acd_NC
{
    
    public class Plugin : IExtensionApplication
    {
        private RibbonTab? _ribbonTab;  // Сохраняем вкладку как поле класса
        private RibbonControl? _ribbonControl;  // И риббон-контроль тоже

        public void Initialize()
        {
            var editor = Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("\nSPlinterFlow инициализирован!\n");

            _ribbonControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            _ribbonTab = new RibbonTab
            {
                Title = Names.TabName,
                Id = "CyberMooduleTab_" + Guid.NewGuid().ToString("N"), // Уникальный ID
                Name = "MAINTAB",
                Tag = "CustomTab" // Добавляем Tag для идентификации
            };

            _ribbonControl.Tabs.Add(_ribbonTab);

            PanelController.Initialize(_ribbonTab);


            FixWpfBindings(_ribbonTab);
        }


        public void Terminate()
        {
            try
            {   
                PanelController.Terminate(RibbonServices.RibbonPaletteSet.RibbonControl);

                if (_ribbonControl == null) return;

                // Важно: удаляем в обратном порядке создания
                Application.Idle += (s, e) =>
                {
                    if (_ribbonTab != null && _ribbonControl.Tabs.Contains(_ribbonTab))
                    {
                        _ribbonControl.Tabs.Remove(_ribbonTab);
                        _ribbonTab = null;
                    }
                };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"Terminate error: {ex}");
            }
        }

        private void FixWpfBindings(RibbonTab tab)
        {
            try
            {
                // Принудительно инициализируем свойства, которые AutoCAD ожидает
                var prop = typeof(RibbonTab).GetProperty("AutomationId",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                prop?.SetValue(tab, tab.Id);
            }
            catch { /* Игнорируем ошибки рефлексии */ }
        }
    }



    public class Commander
    {
        [CommandMethod("CYBER_HELLO")]
        public static void TestCommand()
        {
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SPlinterFlow работает!");

            Application.ShowAlertDialog("Команда CYBER_HELLO вызвана успешно!");

            
        }


        [CommandMethod("SHOW_ROOMS")]
        public static void ShowRooms()
        {
            var rmw = new FlatRoomManagerWindow();
            Application.ShowModelessWindow(rmw);
        }
    }
}

//Application does not support just-in-time (JIT)
//debugging. See the end of this message for details.

//************** Exception Text **************
//System.ArgumentNullException: Value cannot be null. (Parameter 'key')
//   at System.Collections.Generic.Dictionary`2.FindValue(TKey key)
//   at System.Collections.Generic.Dictionary`2.TryGetValue(TKey key, TValue& value)
//   at Autodesk.AutoCAD.Internal.Windows.RibbonWorkspace.SaveWorkspace(RibbonControl ribbonControl)
//   at Autodesk.AutoCAD.Ribbon.RibbonPaletteSet.RibbonPaletteSet_Save(Object sender, PalettePersistEventArgs e)
//   at Autodesk.AutoCAD.Windows.PaletteSet.FireSaving(IUnknown* pUnk)
//   at Autodesk.AutoCAD.Windows.AcMgPaletteSet.preSaveWorker(AcMgPaletteSet*, IUnknown* pUnk)