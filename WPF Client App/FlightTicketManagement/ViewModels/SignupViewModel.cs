using Caliburn.Micro;
using Library.Models;
using FlightTicketManagement.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using FlightTicketManagement.Helper;

namespace FlightTicketManagement.ViewModels
{
    public class SignupViewModel : Screen
    {
        private string _username;
        private string _password;
        private string _name;
        private int _acctype;

        private IEventAggregator _events;

        public SignupViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public string Name { 
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSignup);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanSignup);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanSignup);
            }
        }

        public bool CanSignup
        {
            get
            {
                bool output = false;
                if (Username?.Length > 0 && Password?.Length > 0 && Name?.Length > 0)
                    output = true;
                return output;
            }
        }

        public int AccountType { get => _acctype; set => _acctype = value; }

        public async Task Signup()
        {
            UserAccount user = new UserAccount();
            user.Username = Username;
            user.Password = Password;
            user.Name = Name;

            Response<object> result = await APIHelper.Instance.Post<Response<object>>
                (ApiRoutes.Account.SignUp, user);

            if (result.IsSuccess) {
                MessageBox.Show("Signup success\n"); 
                _events.PublishOnUIThread((int)EventModel.SwitchToLoginEventModel);
            }

            else {
                MessageBox.Show("signup failed\n");
            }
        }

        public void backToLogin()
        {
            _events.PublishOnUIThread((int)EventModel.SwitchToLoginEventModel);
        }
    }
}
