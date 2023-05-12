using Hospital.Controller;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DynamicEquipmentAfterMeducalCheckUp.xaml
    /// </summary>
    public partial class DynamicEquipmentAfterMeducalCheckUp : Window
    {

        private EquipmentController _equipmentController;

        private RoomController _roomController;

        private Room _room;

        private List<int> _textBoxValues = new List<int>();

        private List<Equipment> _activeEquipment = new List<Equipment>();

        public Equipment SelectedEquipment { get; set; }

        public ObservableCollection<Equipment> Equipments { get; set; }

        public DynamicEquipmentAfterMeducalCheckUp()
        {
            InitializeComponent();
            DataContext = this;

            _equipmentController = new EquipmentController();
            _roomController = new RoomController();

            List<Room> rooms = _roomController.GetAll();
            _room = rooms[0];
            foreach(KeyValuePair<string, int> entry in _room.equipmentCount)
            {
                Equipment equipment = _equipmentController.FindByName(entry.Key);
                        if (equipment.type == equipmentType.active)
                        {
                            _activeEquipment.Add(equipment);
                        }
            }
            Equipments = new ObservableCollection<Equipment>(_activeEquipment);
            _textBoxValues = new List<int>(new int[_activeEquipment.Count]);

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Change value of text to int
            if (int.TryParse(((TextBox)sender).Text, out int value))
            {
                // get index of textbox
                int index = myDataGrid.Items.IndexOf(((TextBox)sender).DataContext);

                // update value in list
                _textBoxValues[index] = value;
            }
        }

        private void UpdateEquipment_Click(object sender, RoutedEventArgs e)
        {
            
            int i = 0;
            foreach (KeyValuePair<string, int> entry in _room.equipmentCount)
            {
                Equipment equipment = _equipmentController.FindByName(entry.Key);
                if (equipment.type == equipmentType.active)
                {
                    _room.equipmentCount[entry.Key] -= _textBoxValues[i];
                    i++;
                }
            }

            _roomController.Update();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
