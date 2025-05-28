

using HostMgd.ApplicationServices;
using HostMgd.EditorInput;
using Teigha.Runtime;
//HostMgd.Db.dll → Работа с чертежами
//HostMgd.Geometry.dll → Геометрические операции

[assembly: ExtensionApplication(typeof(SplinterClient_Nacd_NC.Commander))]

namespace SplinterClient_Nacd_NC
{
    public class Plugin : IExtensionApplication
    {
        public void Initialize()
        {
            var editor = Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("\nSPlinterFlow инициализирован!\n");
        }

        public void Terminate()
        {
            
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
