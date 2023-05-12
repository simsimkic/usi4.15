using Hospital.Controller;
using Hospital.Model.Service;
using Hospital.View.Admin.Pages.Filter;
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
using System.Windows.Shapes;
using Hospital.View.Admin.Pages.Reorganization;
using Hospital.View.Admin.Pages.Order;


namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdminMain : Window
    {

        private EquipmentController _controllerEquipment;
        private RoomController _controllerRoom;
        private OrderController _controllerOrder;
        private ReorganizationController _controllerReorganization;

        public AdminMain()
        {
            InitializeComponent();

            _controllerEquipment = new EquipmentController();
            _controllerRoom = new RoomController();
            _controllerOrder = new OrderController(_controllerEquipment.GetDAO(),_controllerRoom.GetDAO());
            _controllerReorganization = new ReorganizationController(_controllerEquipment.GetDAO(), _controllerRoom.GetDAO());



            FilterControl form = new FilterControl(_controllerEquipment,_controllerRoom);
            OrderControl oform = new OrderControl(_controllerEquipment,_controllerRoom,_controllerOrder);
            ReorganizationControl rview = new ReorganizationControl(_controllerRoom,_controllerEquipment, _controllerReorganization);

            this.FilterTab.Content= form;
            this.OrderTab.Content= oform;
            this.ReorganizeTab.Content= rview;

          



        }
    }
}
