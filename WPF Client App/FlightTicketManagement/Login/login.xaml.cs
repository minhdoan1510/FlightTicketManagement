﻿using FlightTicketManagement.Helper;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : UserControl
    {
        public login() {
            InitializeComponent();

        }

        private void onMainAppClose(object sender, EventArgs e) {
            MainWindow.Instance.Close();
        }

        private async void login_Click(object sender, RoutedEventArgs e) {
            string _userName = this.userName.Text;
            string _password = this.passWord.Password.ToString();

            InfoLogin user = await BusControl.Instance.Login(_userName, _password);

            if (user.IsSuccess) {
                Console.WriteLine("id: " + user.Result.Id + '\n');
                Console.WriteLine("userName: " + user.Result.Name + '\n');
                Console.WriteLine("token: " + user.Result.Token + '\n');

                MainWindow.Instance.Hide();

                MainApp mainApp = new MainApp();
                mainApp.Closed += onMainAppClose;
                mainApp.Show();
            }
            else {
                MessageBox.Show("login failed\n");
            }
        }

        private void signUp_Click(object sender, RoutedEventArgs e) {
            MainWindow.Instance.switchToSignUp();
        }
    }
}
