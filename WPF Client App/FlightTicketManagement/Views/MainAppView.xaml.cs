using Caliburn.Micro;
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

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for MainApp.xaml
    /// </summary>

    public partial class MainAppView : Window
    {
        public MainAppView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }




        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void tileBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

     

        //private void dashBoardBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("dashBoardCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void planeScheduleBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("planeScheduleCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void createTicketBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("createTicketCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void planeListBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("planeListCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void reportBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("reportCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void donateBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("donateCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void settingBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    bringToFront(userControlGrid);

        //    bringToFrontControlByName("settingCtrl");
        //    this.setSelectionBtn(sender);

        //    this.tile.Text = this.usercontrolTiles[sender as UIElement];
        //}

        //private void setSelectionBtn(object btn)
        //{
        //    int index = btnOrder[btn as UIElement];

        //    buttonFlag.Margin = new Thickness(0, 146 + (51 * index), 0, 571 - (51 * index));
        //}

    }
}
