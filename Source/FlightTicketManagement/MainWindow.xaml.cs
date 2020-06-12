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
using FlightTicketManagement.DTO;
using FlightTicketManagement.BUS;

namespace FlightTicketManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSignUpLogin_Click(object sender, RoutedEventArgs e) 
        {
            Account account = new Account(txbUsernameLogin.Text, txbPasswordLogin.Password);
            if (BUS_Controls.Controls.Login(account))
                MessageBox.Show("Đăng nhập thành công");
            else
                MessageBox.Show("Thông tin tài khoản chưa chính xác");
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            gridLogin.Visibility = Visibility.Hidden;
            gridSignUp.Visibility = Visibility.Visible;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account(txbUsernameLogin.Text, txbPasswordLogin.Password, txbNameSignUp.Text, 1);

            //foreach (object item in gridSignUp.Children)
            //{
            //    if(item.GetType().Equals(typeof(PasswordBox))|| item.GetType().Equals(typeof(TextBox)))
            //    {
            //    }    

            //}

            if (BUS_Controls.Controls.Signup(account))
            {
                MessageBox.Show("Đăng kí thành công");
                gridLogin.Visibility = Visibility.Visible;
                gridSignUp.Visibility = Visibility.Hidden;
            }
            else
                MessageBox.Show("Đăng kí thất bại. User đã có sẵn");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gridLogin.Visibility = Visibility.Visible;
            gridSignUp.Visibility = Visibility.Hidden;
        }
    }
}
