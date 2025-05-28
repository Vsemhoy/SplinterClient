using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows.Themes;
using SplinterClient_Acd_NC.ui.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SplinterClient_Acd_NC.ui.Windows.RoomManager
{
    /// <summary>
    /// Interaction logic for FlatRoomManagerWindow.xaml
    /// </summary>
    public partial class FlatRoomManagerWindow : Window
    {
        public ObservableCollection<Room> Rooms { get; } = new();
        public FlatRoomManagerWindow()
        {
            InitializeComponent();
            LocalThemeManager.ApplyTheme(this);
            RoomsDataGrid.ItemsSource = Rooms;
            LoadRooms();

            Autodesk.AutoCAD.ApplicationServices.Application.SystemVariableChanged += OnSystemVariableChanged;
        }

        private void OnSystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
        {
            if (e.Name == "COLORTHEME")
            {
                Dispatcher.Invoke(() => LocalThemeManager.ApplyTheme(this));
            }
        }


        private void LoadRooms()
        {
            // Загрузка данных из AutoCAD
            Rooms.Add(new Room { Number = "101", Name = "Гостиная", Area = 25.5 });
            Rooms.Add(new Room { Number = "102", Name = "Кухня", Area = 18.0 });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Rooms.Add(new Room { Number = "Новый", Name = "Новое помещение", Area = 0 });
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is Room selected)
                Rooms.Remove(selected);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение данных в чертёж
            DialogResult = true;
            Close();
        }
    }



    public class Room
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
    }
}
