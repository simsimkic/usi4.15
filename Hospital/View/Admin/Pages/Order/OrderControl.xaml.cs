using Hospital.Controller;
using Hospital.Model.DAO;
using Hospital.View.Admin.Pages.Order.DialogBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Admin.Pages.Order
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {

        private EquipmentController _controllerEquipment;

        private RoomController _controllerRoom;

        private OrderController _controllerOrder;

        Dictionary<Equipment, int> keyValuePairs { get; set; }

        List<Equipment> activeEquipment;



        public OrderControl(EquipmentController equipmentController,RoomController roomController, OrderController controllerOrder)
        {
            InitializeComponent();

            DataContext = this;

            _controllerEquipment = equipmentController;
            _controllerRoom = roomController;
            _controllerOrder = controllerOrder;

            activeEquipment = _controllerEquipment.GetActiveEquipment();

            FormTable();
           
        }

        void FormTable()
        {
            keyValuePairs = new Dictionary<Equipment, int>();

            foreach (Equipment equipment in activeEquipment)
            {
                int equipmentCount = _controllerRoom.GetEquipmentCount(equipment);

                if (equipmentCount < 5)
                {
                    keyValuePairs.Add(equipment, equipmentCount);
                }

            }

            this.dgActive.ItemsSource= keyValuePairs;
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dgActive.SelectedItem == null)
            {
                MessageBox.Show("Please select an equipment to order");
                return;
            }

            KeyValuePair<Equipment, int> selectedRow = (KeyValuePair<Equipment, int>)dgActive.SelectedItem;
            string selectedEquipment= selectedRow.Key.ToString();
      
            OrderForm orderDialog = new OrderForm(selectedEquipment,_controllerRoom,_controllerOrder);
            orderDialog.ShowDialog();

            dgActive.SelectedItem = null;
        }
    }
}
            