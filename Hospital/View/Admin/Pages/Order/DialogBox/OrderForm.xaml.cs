using System;
using Hospital.Model;
using Hospital.Controller;
using System.Collections.Generic;
using System.Globalization;
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
using Hospital.Model.DAO;

namespace Hospital.View.Admin.Pages.Order.DialogBox;

/// <summary>
/// Interaction logic for QuantityDialog.xaml
/// </summary>
public partial class OrderForm : Window
{
    string selectedEquipment;
    List<Room> storageRooms;
    OrderController orderController;
    RoomController roomController;

    public OrderForm(string equipment, RoomController roomController,OrderController orderController)
    {

        this.roomController = roomController;
        this.orderController = orderController;

        InitializeComponent();


        selectedEquipment = equipment;

        storageRooms = roomController.GetStorageRooms();

        storageCb.ItemsSource = storageRooms;
        storageCb.SelectedIndex = 0;
       
    }

    private void okButton_Click(object sender, RoutedEventArgs e)
    {
       
        if (CheckIfNumber(quantityTextBox.Text))
        {
            int quantity = int.Parse(quantityTextBox.Text);
            DateTime arrivalDate= DateTime.Now.AddDays(1);
            string storage = this.storageCb.SelectedItem.ToString();

            Model.Order order = new Model.Order(storage,selectedEquipment, quantity, arrivalDate);

            orderController.Create(order);

            this.Close();
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

