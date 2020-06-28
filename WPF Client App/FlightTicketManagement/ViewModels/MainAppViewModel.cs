using Caliburn.Micro;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.ViewModels
{
    class MainAppViewModel : Conductor<object>,IHandle<GetTransitEvent>
    {
        private IEventAggregator _events;
        private IWindowManager _windowManager;
        SimpleContainer _container;
        public MainAppViewModel(IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _windowManager = _container.GetInstance<IWindowManager>();
            _events.Subscribe(this);
            ActivateItem(_container.GetInstance<TransitView>());

        }

        public void Handle(GetTransitEvent message)
        {

            _windowManager.ShowWindow(new TransitViewModel(message.FlightId));
        }

        public void ShowDashBoardView()
        {
            ActivateItem(_container.GetInstance<DashboardViewModel>());
        }

        public void ShowPlaneListView()
        {
            ActivateItem(_container.GetInstance<FlightListViewModel>());
        }
        public void ShowSettingView()
        {
            ActivateItem(_container.GetInstance<SettingViewModel>());
        }


    }
}
