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

    public partial class MainAppView : UserControl
    {
        const int controlTopLayer = 0;
        const int controlBottomLayer = -1;

        bool isMaximize = false;

        bool onMenuMode = false;

        List<Brush> buttonOrigionalColor = new List<Brush>();

        public MainAppView()
        {
            InitializeComponent();
            // this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        void bringToFront(UIElement control)
        {
            Canvas.SetZIndex(control, controlTopLayer);
            control.Visibility = Visibility.Visible;
        }

        void sendToBack(UIElement control)
        {
            Canvas.SetZIndex(control, controlBottomLayer);
            control.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            // this.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            //  this.WindowState = WindowState.Minimized;
        }

        private void tileBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        public void bringToFrontControlByName(string name)
        {
            foreach (var item in userControlGrid.Children)
            {
                if ((item as UserControl).Name == name)
                {
                    bringToFront(item as UserControl);
                }
                else sendToBack(item as UserControl);
            }
        }

        private void dashBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("dashBoardCtrl");
        }

        private void planeScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("planeScheduleCtrl");
        }

        private void createTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("createTicketCtrl");
        }

        private void planeListBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("planeListCtrl");
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("reportCtrl");
        }

        private void donateBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("donateCtrl");
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            bringToFront(userControlGrid);

            bringToFrontControlByName("settingCtrl");
        }
    }
}
