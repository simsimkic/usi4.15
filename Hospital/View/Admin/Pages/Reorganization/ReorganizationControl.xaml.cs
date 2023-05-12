using Hospital.Controller;
using Hospital.Model.DAO;
using Hospital.Model;
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
using System.Data.Common;
using Hospital.View.Admin.Pages.Reorganization.DialogBox;


namespace Hospital.View.Admin.Pages.Reorganization

{
    /// <summary>
   
    /// </summary>
    public partial class ReorganizationControl : UserControl
    {

        private EquipmentController _controllerEquipment;
        private RoomController _controllerRoom;
        private ReorganizationController _controllerReorganization;
        Dictionary<string,Dictionary<string,int>> rooms =new Dictionary<string,Dictionary<string,int>>();
        

        public ReorganizationControl(RoomController roomController,EquipmentController equipmentController,ReorganizationController reorganizationController)
        {
            _controllerRoom = roomController;
            _controllerEquipment = equipmentController;
            _controllerReorganization = reorganizationController;

            InitializeComponent();

            List< DataGridTextColumn> columns = CreateColumns();
           
            foreach(DataGridTextColumn columnn in columns)
            {

                    dgRooms.Columns.Add(columnn);

            }

            dgRooms.DataContext = rooms;
            dgRooms.ItemsSource = rooms;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ReorganizationForm reorganizeDialog = new ReorganizationForm(_controllerEquipment,_controllerRoom,_controllerReorganization);
            reorganizeDialog.ShowDialog();

            dgRooms.ItemsSource = null;
            dgRooms.ItemsSource = rooms;
            

        }

        List<DataGridTextColumn> CreateColumns()
        {

            List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Broj";
            column.Binding = new Binding("Key");
            columns.Add(column);

            foreach (Room room in _controllerRoom.GetAll())
            {
                rooms[room.number] = room.equipmentCount;

            }

            foreach (Equipment equipment in _controllerEquipment.GetAll())
            {
                column = GetCustomizedColumn(equipment);
    
                columns.Add(column);
            }

            return columns;

        }

        public DataGridTextColumn GetCustomizedColumn(Equipment equipment)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = equipment.name;
            column.Binding = new Binding("Value[" + equipment.name + "]");
            


            Style style = new Style();
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Value[" + equipment.name + "]");
            

            binding.Converter= new ValueToBrushConverter();
            style.Setters.Add(new Setter(DataGridCell.BackgroundProperty, binding));

            column.CellStyle = style;
            return column;

        }
    }

           
}
