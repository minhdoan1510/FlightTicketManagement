using Caliburn.Micro;
using Models;
using FlightTicketManagement.BUS;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Views;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Login()
        {

            //InfoLogin user = await BusControl.Instance.Login(_userName, _password);

            //if (user.IsSuccess)
            //{
            //    Console.WriteLine("id: " + user.Result.Id + '\n');
            //    Console.WriteLine("userName: " + user.Result.Name + '\n');
            //    Console.WriteLine("token: " + user.Result.Token + '\n');

            //    MainWindow.Instance.Hide();

            //    MainApp mainApp = new MainApp();
            //    mainApp.Closed += onMainAppClose;
            //    mainApp.Show();
            //}
            //else
            //{
            //    MessageBox.Show("login failed\n");
            //}

            //ShellView.Instance.Hide();

            //    MainApp mainApp = new MainApp();
            //    mainApp.Closed += onMainAppClose;
            //    mainApp.Show();

            _events.PublishOnUIThread((int)EventModel.LogOnEventModel);
        }

        public void SwitchToSignup()
        {
            //_events.PublishOnUIThread((int)EventModel.SwitchToSignupEventModel);
            ShellView.Instance.switchToSignUp();
        }
    }
}
