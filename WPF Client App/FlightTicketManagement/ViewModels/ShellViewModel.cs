using Caliburn.Micro;
using FlightTicketManagement.EventModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<int>
    {
        private IEventAggregator _events;
        SimpleContainer _container;
        public ShellViewModel(IEventAggregator events,SimpleContainer container)
        {
            _events = events;
            _container = container;
            _events.Subscribe(this);
            ActivateItem(_container.GetInstance<LoginViewModel>());

        }

       
        public void Handle(int message)
        {
            switch (message)
            {
                case (int)EventModel.LogOnEventModel:
                    ActivateItem(_container.GetInstance<MainAppViewModel>());
                    break;
                case (int)EventModel.SwitchToLoginEventModel:
                    ActivateItem(_container.GetInstance<LoginViewModel>());
                    break;
                case (int)EventModel.SwitchToSignupEventModel:
                    ActivateItem(_container.GetInstance<SignupViewModel>());
                    break;
                default:
                    break;
            }
        }
    }
}
