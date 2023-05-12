using Hospital.Controller;
using Hospital.Model;
using Hospital.Model.Service;
using Hospital.Storage;
using Microsoft.Windows.Themes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Hospital.View.Admin.Pages.Reorganization.DialogBox
{
    /// <summary>
    /// Interaction logic for ReorganizeDialog.xaml
    /// </summary>
    public partial class ReorganizationForm : Window
    {
        EquipmentController _controllerEquipment;
        RoomController _controllerRoom;
        ReorganizationController _controllerReorganization;

        FilterRoomService filterRoomService;

        public List<Equipment> equipment { get; set; }

        public List<Room> from { get; set; }

        public List<Room> to { get; set; }


        public ReorganizationForm(EquipmentController equipmentController,RoomController roomController,ReorganizationController reorganizationController)
        {

            _controllerEquipment = equipmentController;
            _controllerRoom=roomController;
            _controllerReorganization=reorganizationController;
            filterRoomService= new FilterRoomService(_controllerRoom.GetDAO());

           
            InitializeComponent();

            equipment = _controllerEquipment.GetAll();
            from= _controllerRoom.GetAll();
            to= _controllerRoom.GetAll();

            DataContext = this;

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {   
            if(CheckIfDataValid()==false) { return; }

            int quantity = int.Parse(tbQuantity.Text);
            Room fromRoom = (Room)cbFrom.SelectedItem;
            string to = cbTo.SelectedItem.ToString();
            string equipment = cbEquipment.SelectedItem.ToString();
            DateTime dueDate = GetDatePickerDate();

            if (quantity > fromRoom.equipmentCount[equipment])
            {
                MessageBox.Show("Too much equipment.");
                return;
            }
            
            Model.Reorganization reorganization = new Model.Reorganization(fromRoom.number, to, quantity,dueDate,equipment);

            _controllerReorganization.Create(reorganization);
            _controllerRoom.Update();

            this.Close();
        }

        private void cbEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            Equipment selectedEquipment= (Equipment)cbEquipment.SelectedItem;

            if (selectedEquipment == null) { return; }

            SetDatePickerVisibility(selectedEquipment);

            from =_controllerRoom.GetRoomsWithEquipment(selectedEquipment.name);

            Room oldSelected= (Room)cbFrom.SelectedItem;

            UpdateSource<Room>(cbFrom, from, oldSelected);

        }

        private void cbFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selectedRoom = (Room)cbFrom.SelectedItem;

            if (selectedRoom == null) { return; }

            HashSet<Equipment> temp= new HashSet<Equipment>();

            foreach(KeyValuePair<string,int> entry in selectedRoom.equipmentCount)
            {
                temp.Add(_controllerEquipment.FindByName(entry.Key));
            }

            equipment = temp.ToList();

            Equipment oldSelected = (Equipment)cbEquipment.SelectedItem;

            UpdateSource<Equipment>(cbEquipment, equipment, oldSelected);

           
        }

        void UpdateSource<T>(ComboBox cb,List<T> newSource,T oldValue)
        {

            cb.ItemsSource = newSource;


            if (cbEquipment.Items.Contains(oldValue))
            {
                cbEquipment.SelectedItem = oldValue;
            }

        }

        void SetDatePickerVisibility(Equipment selectedEquipment)
        {
            if (selectedEquipment.type == equipmentType.passive)
            {
                datePicker.Visibility = Visibility.Visible;
            }
            else
            {
                datePicker.Visibility = Visibility.Collapsed;
            }

        }

        public bool CheckIfDataValid()
        {
                

                if (cbEquipment.SelectedItem == null)
                {
                    MessageBox.Show("Please select equipment.");
                    return false;
                }
                if (cbFrom.SelectedItem == null)
                {
                    MessageBox.Show("Please select room from.");
                    return false;
                }
                if (cbTo.SelectedItem == null)
                {
                    MessageBox.Show("Please select room to.");
                    return false;
                }
                if (tbQuantity.Text == "")
                {
                    MessageBox.Show("Please enter quantity.");
                    return false;
                }
                if (!CheckIfNumber(tbQuantity.Text))
                {
                    return false;
                }
                if (datePicker.Visibility == Visibility.Visible)
                {
                    DateTime date= (DateTime)datePicker.SelectedDate;
                    if (datePicker.SelectedDate == null||date.Date<DateTime.Now.Date)
                    {
                        MessageBox.Show("Please select due date.");
                        return false;
                    }
                }
                return true;    

        }

        public DateTime GetDatePickerDate()
        {
            if (datePicker.SelectedDate == null)
            {

                return DateTime.Now;
            }
            else
            {
                return (DateTime)datePicker.SelectedDate;
            }

        }

        public bool CheckIfNumber(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(text))
            {
                MessageBox.Show("Please enter only numbers.");
                return false;
            }
            return true;
        }


    }
}
