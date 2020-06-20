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

    public partial class MainApp : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        const int menuMarginButtonArea = 50;
        const float menuModeOpacity = 0.45f;

        const int origionalMarginTopButtonArea = 150;
        const int origionalMarginBottomButtonArea = 50;
        Thickness origionalBorderThicknessButtonArea = new Thickness(1);
        Thickness origionMarginButtonArea;

        const int controlTopLayer = 0;
        const int controlBottomLayer = -1;

        bool isMaximize = false;
        bool onHomeScreen = true;

        bool onMenuMode = false;
        public bool checkMenuMode {
            get { return onMenuMode; }
            set {
                onMenuMode = value;
                
                if (PropertyChanged != null) {
                    PropertyChanged(this, 
                        new PropertyChangedEventArgs("checkMenuMode"));
                }
            }
        }

        List<Brush> buttonOrigionalColor = new List<Brush>();

        public MainApp() {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            foreach (var item in this.buttonGrid.Children) {
                buttonOrigionalColor.Add((item as Button).Background);
            }
            origionMarginButtonArea = this.buttonArea.Margin;
           
        }

        void bringToFront(UIElement control) {
            Canvas.SetZIndex(control, controlTopLayer);
            control.Visibility = Visibility.Visible;
        }

        void sendToBack(UIElement control) {
            Canvas.SetZIndex(control, controlBottomLayer);
            control.Visibility = Visibility.Hidden;
        }

        void turnOnMenuMode() {
            this.sendToBack(this.userControlGrid);
            this.bringToFront(this.homeGrid);

            this.appTile.Visibility = Visibility.Hidden;
            this.userArea.Visibility = Visibility.Hidden;

            foreach (var item in this.buttonGrid.Children) {
                (item as Button).Background = Brushes.Black;

                ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(
                    new Uri("Resource/resource.xaml", UriKind.Relative));

                (item as Button).Style = (Style)resource["menuStyle"];
            }

            Thickness newMargin = this.buttonArea.Margin;
            newMargin.Top = origionalMarginTopButtonArea - menuMarginButtonArea;
            newMargin.Bottom = origionalMarginBottomButtonArea + menuMarginButtonArea;

            this.buttonArea.BorderThickness = new Thickness(0);
            this.buttonArea.Margin = newMargin;

            // activate animation on changing property 
            this.scrollView.Visibility = Visibility.Hidden;
            this.scrollView.Visibility = Visibility.Visible;
        }

        void undoMenuMode() {
            this.sendToBack(this.homeGrid);

            if (onHomeScreen) {
                this.turnOnHomeScreen();
            }
            else {
                this.bringToFront(userControlGrid);
            }
        }

        private void turnOnHomeScreen() {
            this.sendToBack(this.userControlGrid);
            this.bringToFront(this.homeGrid);

            this.homeGrid.Opacity = 1;

            this.appTile.Visibility = Visibility.Visible;
            this.userArea.Visibility = Visibility.Visible;

            int i = 0;
            foreach (var item in this.buttonGrid.Children) {
                (item as Button).Background = buttonOrigionalColor[i++];

                ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(
                    new Uri("Resource/resource.xaml", UriKind.Relative));

                (item as Button).Style = (Style)resource["tileButton"];
            }

            this.buttonArea.Margin = origionMarginButtonArea;
            this.buttonArea.BorderThickness = origionalBorderThicknessButtonArea;

            // activate animation on changing property 
            this.scrollView.Visibility = Visibility.Hidden;
            this.scrollView.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void maximizeBtn_Click(object sender, RoutedEventArgs e) {
            
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void menuBtn_Click(object sender, RoutedEventArgs e) {
            this.checkMenuMode = !this.checkMenuMode;
        }

        private void tileBar_MouseDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e) {
            this.turnOnHomeScreen();

            this.onHomeScreen = true;
            this.checkMenuMode = false;
        }

        private void turnOffHomeScreen() {
            this.homeGrid.Visibility = Visibility.Hidden;
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
            sendToBack(homeGrid);

            bringToFrontControlByName("DashboardCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }

        private void planeScheduleBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);
            sendToBack(homeGrid);

            bringToFrontControlByName("PlaneScheduleCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }

        private void createTicketBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);
            sendToBack(homeGrid);

            bringToFrontControlByName("CreateTicketCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);
            sendToBack(homeGrid);

            bringToFrontControlByName("ReportCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }

        private void planeListBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);
            sendToBack(homeGrid);

            bringToFrontControlByName("PlaneListCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);
            sendToBack(homeGrid);

            bringToFrontControlByName("SettingCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }

        private void donateBtn_Click(object sender, RoutedEventArgs e) {
            bringToFront(userControlGrid);
            sendToBack(homeGrid);

            bringToFrontControlByName("DonateCtrl");

            this.onHomeScreen = false;
            this.checkMenuMode = false;
        }
    }
}
