using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;


[assembly: CommandClass(typeof(AutoCADPlugin.Commander))]

namespace AutoCADPlugin
{
    public class Plugin : IExtensionApplication
    {
        public void Initialize()
        {
            // Adapter for ribbons are wait
            var editor = Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("\nSPlinterFlow инициализирован!\n");

        }


        public void Terminate()
        {
            // Cleanup code, if any
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
    }
}
