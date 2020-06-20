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
using DTO;

namespace FlightTicketManagement
{
    /// <summary>
    /// Interaction logic for signUp.xaml
    /// </summary>
    public partial class signUp : UserControl
    {
        public signUp() {
            InitializeComponent();
        }

        private async void signUp_Click(object sender, RoutedEventArgs e) {
            UserAccount user = new UserAccount();
            user.Username = this.userName.Text;
            user.Password = this.passWord.Password.ToString();
            user.Name = this.name.Text;
            user.Acctype = Int32.Parse(this.accountType.Text);

            Signup signUpAccount = await BusControl.Instance.Signup(user);

            if (signUpAccount.IsSuccess)
                MainWindow.Instance.switchToLogin();
            else {
                MessageBox.Show("signup failed\n");
            }
        }

        private void backToLogin_Click(object sender, RoutedEventArgs e) {
            MainWindow.Instance.switchToLogin();
        }
    }
}
