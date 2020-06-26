using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightTicketManagement
{
    /// <summary>
    /// Interaction logic for MainApp.xaml
    /// </summary>

    public partial class MainApp : Window
    {
        const int controlTopLayer = 0;
        const int controlBottomLayer = -1;

        Dictionary<UIElement, int> btnOrder;
        Dictionary<UIElement, string> usercontrolTiles;

        public MainApp() {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            btnOrder = new Dictionary<UIElement, int>();
            usercontrolTiles = new Dictionary<UIElement, string>();

            btnOrder[dashBoardBtn] = 0;
            btnOrder[planeScheduleBtn] = 1;
            btnOrder[createTicketBtn] = 2;
            btnOrder[planeListBtn] = 3;
            btnOrder[reportBtn] = 4;
            btnOrder[donateBtn] = 5;
            btnOrder[settingBtn] = 6;

            usercontrolTiles[dashBoardBtn] = "DashBoard";
            usercontrolTiles[planeScheduleBtn] = "Lập Lịch Máy Bay";
            usercontrolTiles[createTicketBtn] = "Bán Vé";
            usercontrolTiles[planeListBtn] = "Danh Sách Chuyến Bay";
            usercontrolTiles[reportBtn] = "Báo Cáo Doanh Số";
            usercontrolTiles[donateBtn] = "Ủng Hộ Nhà Phát Triển";
            usercontrolTiles[settingBtn] = "Cài Đặt";
        }

        void bringToFront(UIElement control) {
            Canvas.SetZIndex(control, controlTopLayer);
            control.Visibility = Visibility.Visible;
        }

        void sendToBack(UIElement control) {
            Canvas.SetZIndex(control, controlBottomLayer);
            control.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void tileBar_MouseDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        public void bringToFrontControlByName(string name) {
            foreach (var item in userControlGrid.Children) {
                if ((item as UserControl).Name == name) {
                    bringToFront(item as UserControl);
                }
                else sendToBack(item as UserControl);
            }
        }

        private void dashBoardBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("dashBoardCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void planeScheduleBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("planeScheduleCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void createTicketBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("createTicketCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void planeListBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("planeListCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("reportCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void donateBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("donateCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);

            bringToFrontControlByName("settingCtrl");
            this.setSelectionBtn(sender);

            this.tile.Text = this.usercontrolTiles[sender as UIElement];
        }

        private void setSelectionBtn(object btn) {
            int index = btnOrder[btn as UIElement];

            buttonFlag.Margin = new Thickness(0, 146 + (51 * index), 0, 571 - (51 * index));
        }
    }
}
