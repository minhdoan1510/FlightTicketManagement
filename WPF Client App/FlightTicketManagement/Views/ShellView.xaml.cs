
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

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class ShellView : Window
    {
        enum winProp
        {
            width = 480,
            height = 640
        }

        public static ShellView Instance;

        public ShellView()
        {
            InitializeComponent();

            Instance = this;
            Instance.Closed += MainApp_Closed;
        }

        private void MainApp_Closed(object sender, EventArgs e) {
            ShellView.Instance.Close();
        }

        public void switchToSignUp()
        {
            //this.signupCtrl.Visibility = Visibility.Visible;
            //this.loginCtrl.Visibility = Visibility.Hidden;
        }

        public void switchToLogin()
        {
            //this.signupCtrl.Visibility = Visibility.Hidden;
            //this.loginCtrl.Visibility = Visibility.Visible;
        }

        private void tileBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
