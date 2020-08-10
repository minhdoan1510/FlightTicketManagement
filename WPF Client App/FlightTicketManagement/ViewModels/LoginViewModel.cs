using Caliburn.Micro;
using Library.Models;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Views;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Helper;
using System.Windows;

namespace FlightTicketManagement.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;

        private string _password;
        private IEventAggregator _events;

        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public string Username
        {
            get { return _username; }
            set { 
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Password
        {
            get { return _password; }
            set { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }
        public bool CanLogin
        {
            get
            {
                bool output = false;
                if (Username?.Length > 0 && Password?.Length > 0)
                    output = true;
                return output;
            }
        }

        bool LoginOnce = false;
        public async Task Login()
        {
            if (LoginOnce == true)
                return;

            LoginOnce = true;
            if (await APIHelper.Instance.Authenticate(Username, Password)) {
                ShellView.Instance.Hide();

                _events.PublishOnUIThread((int)EventModel.LogOnEventModel);
            }
            else {
                MessageBox.Show("Login failed");
                LoginOnce = false;
            }
        }

        public void SwitchToSignup()
        {
            _events.PublishOnUIThread((int)EventModel.SwitchToSignupEventModel);
        }
    }
}
