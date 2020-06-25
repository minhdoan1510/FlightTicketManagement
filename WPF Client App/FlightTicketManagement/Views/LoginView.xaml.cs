using FlightTicketManagement.Helper;
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

using FlightTicketManagement.BUS;
using Library.Models;


namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView() {
            InitializeComponent();

        }

        private void onMainAppClose(object sender, EventArgs e) {
            ShellView.Instance.Close();
        }

        //private async void login_Click(object sender, RoutedEventArgs e) {
        //    //string _userName = this.userName.Text;
        //    //string _password = this.passWord.Password.ToString();

        //    //InfoLogin user = await BusControl.Instance.Login(_userName, _password);

        //    //if (user.IsSuccess) {
        //    //    Console.WriteLine("id: " + user.Result.Id + '\n');
        //    //    Console.WriteLine("userName: " + user.Result.Name + '\n');
        //    //    Console.WriteLine("token: " + user.Result.Token + '\n');

        //    //    MainWindow.Instance.Hide();

        //    //    MainApp mainApp = new MainApp();
        //    //    mainApp.Closed += onMainAppClose;
        //    //    mainApp.Show();
        //    //}
        //    //else {
        //    //    MessageBox.Show("login failed\n");
        //    //}

        //    ShellView.Instance.Hide();

        //    MainAppView mainApp = new MainAppView();
        //    mainApp.Closed += onMainAppClose;
        //    mainApp.Show();
        //}

        //private void signUp_Click(object sender, RoutedEventArgs e) {
        //    ShellView.Instance.switchToSignUp();
        //}
    }
}
