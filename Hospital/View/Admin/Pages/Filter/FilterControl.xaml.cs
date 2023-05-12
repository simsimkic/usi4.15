using Hospital.Controller;
using Hospital.Model;
using Hospital.Model.Service;
using System;
using System.Collections.Generic;
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
using Hospital.View.Admin.Pages.Filter;
using System.Collections.ObjectModel;

namespace Hospital.View.Admin.Pages.Filter
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FilterControl : UserControl

    {

        private EquipmentController _controllerEquipment;
        private RoomController _controllerRoom;

        List<string> equipmentTypeOptions;

        List<string> roomPurposeOptions;

        List<string> countOptions = new List<string>
        {
            "All","0","0-10",">10"
        };

        FilterEquipmentService filter;
        SearchService search;
        public List<Equipment> filteredEquipment { get; set; }
        public FilterControl(EquipmentController equipmentController,RoomController roomController)
        {
            InitializeComponent();

            DataContext = this;

            _controllerEquipment = equipmentController;
            _controllerRoom = roomController;
            equipmentTypeOptions= new List<string>();
            roomPurposeOptions= new List<string>();

            search = new SearchService("", _controllerEquipment.GetDAO());
            filter = new FilterEquipmentService(_controllerRoom.GetDAO(), _controllerEquipment.GetDAO());
            filteredEquipment = new List<Equipment>(_controllerEquipment.GetAll());

            SetComboBox<equipmentType>(typeCb,equipmentTypeOptions);

            SetComboBox<roomPurpose>(roomCb,roomPurposeOptions);


            countCb.ItemsSource = countOptions;
            countCb.SelectedIndex = 0;
            countCb.SelectionChanged += ComboBox_SelectionChanged;

            storageCb.IsChecked = true;
            storageCb.Checked += storageCb_Checked;
            storageCb.Unchecked += storageCb_Checked;
           
            searchTb.TextChanged += searchTb_TextChanged;
        }


        void SetComboBox<T>(ComboBox cb, List<string> source)
        {

            source.Add("All");
            source.AddRange(Enum.GetNames(typeof(T)).ToList<string>());
            cb.ItemsSource = equipmentTypeOptions;
            cb.SelectedIndex = 0;
            cb.SelectionChanged += ComboBox_SelectionChanged;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndFilter();
            Console.WriteLine
                ("ComboBox_SelectionChanged");
        }

        public void SearchAndFilter()
        {
            search.input = this.searchTb.Text;
            List<Equipment> temp = new List<Equipment>();

            temp.AddRange(search.SearchEquipment());

            int eqType = this.typeCb.SelectedIndex - 1;
            int roomPurpose = this.roomCb.SelectedIndex - 1;
            bool storage = this.storageCb.IsChecked.Value;
            int quantityOption = this.countCb.SelectedIndex - 1;

            temp=filter.FilterEquipmentType(eqType,temp);
            temp=filter.FilterRoomPurpose(roomPurpose,temp);
            temp=filter.FilterEquipmentQuantity(quantityOption,temp);
            temp=filter.FilterStorage(storage,temp);

            filteredEquipment.Clear();
            filteredEquipment.AddRange(temp);
            this.lb1.Items.Refresh();

        }


        private void storageCb_Checked(object sender, RoutedEventArgs e)
        {
            SearchAndFilter();
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAndFilter();
        }
    }
}
