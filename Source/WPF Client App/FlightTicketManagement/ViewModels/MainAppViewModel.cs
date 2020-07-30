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
    class MainAppViewModel : Conductor<object>
    {
        private IEventAggregator _events;
        SimpleContainer _container;
        private string titleView;
        public string TitleView { get => titleView; set { titleView = value; NotifyOfPropertyChange(() => TitleView); } }

        public MainAppViewModel(IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _events.Subscribe(this);
            ActivateItem(_container.GetInstance<FlightListViewModel>());
        }

        public void ShowDashBoardView()
        {
            TitleView = _container.GetInstance<DashboardViewModel>().DisplayName;
            ActivateItem(_container.GetInstance<DashboardViewModel>());
        }

        public void ShowPlaneListView()
        {
            TitleView = _container.GetInstance<FlightListViewModel>().DisplayName;
            ActivateItem(_container.GetInstance<FlightListViewModel>());
        }

        public void ShowReportView()
        {
            TitleView = _container.GetInstance<ReportViewModel>().DisplayName;
            ActivateItem(_container.GetInstance<ReportViewModel>());
        }


        public void ShowCreateTicketView()
        {
            TitleView = _container.GetInstance<CreateTicketViewModel>().DisplayName;
            ActivateItem(_container.GetInstance<CreateTicketViewModel>());
        }

        public void CloseMainView()
        {
           // this.CloseMainView();
        }
    }

}
