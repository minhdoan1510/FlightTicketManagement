using Caliburn.Micro;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightTicketManagement.ViewModels
{
    class MainAppViewModel : Conductor<object>,IHandle<GetTransitEvent>
    {
        private IEventAggregator _events;
        private IWindowManager _windowManager;

        SimpleContainer _container;
        Dictionary<Screens, string> Titles;

        enum Screens
        {
           DashBoard,
           PlaneSchedule,
           CreateTicket,
           FlightList,
           Report,
            Donate,
            Setting
        }
        private string title;

        public string Title
        {
            get { return title; }
            set {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private Thickness buttonTracker;

        public Thickness ButtonTracker
        {
            get { return buttonTracker; }
            set { 
                buttonTracker = value;
                NotifyOfPropertyChange(() => ButtonTracker);
            }
        }

        public MainAppViewModel(IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _windowManager = _container.GetInstance<IWindowManager>();
            _events.Subscribe(this);

            SetTitles();
            ActivateScreen(Screens.DashBoard);
        }

        private void ActivateScreen(Screens screen)
        {
            switch(screen)
            {
                case Screens.FlightList:
                    ActivateItem(_container.GetInstance<FlightListViewModel>());
                    break;
                case Screens.PlaneSchedule:
                    ActivateItem(_container.GetInstance<PlaneScheduleViewModel>());
                    break;
                case Screens.CreateTicket:
                    ActivateItem(_container.GetInstance<CreateTicketViewModel>());
                    break;
                case Screens.Setting:
                    ActivateItem(_container.GetInstance<SettingViewModel>());
                    break;
                case Screens.DashBoard:
                default:
                    ActivateItem(_container.GetInstance<DashboardViewModel>());
                    break;
            }
            Title = Titles[screen];
            ButtonTracker = new Thickness(0, 146 + (51 * (int)screen), 0, 571 - (51 * (int)screen));
        }

        private void SetTitles()
        {
            Titles = new Dictionary<Screens, string>();
            Titles[Screens.DashBoard] = "DashBoard";
            Titles[Screens.PlaneSchedule] = "Lập Lịch Máy Bay";
            Titles[Screens.CreateTicket] = "Bán Vé";
            Titles[Screens.FlightList] = "Danh Sách Chuyến Bay";
            Titles[Screens.Report] = "Báo Cáo Doanh Số";
            Titles[Screens.Donate] = "Ủng Hộ Nhà Phát Triển";
            Titles[Screens.Setting] = "Cài Đặt";
        }

        public void Handle(GetTransitEvent message)
        {
            _windowManager.ShowWindow(new TransitViewModel(message.FlightId));
        }

        public void ShowDashBoardView()
        {
            ActivateScreen(Screens.DashBoard);
        }

        public void ShowPlaneListView()
        {
            ActivateScreen(Screens.FlightList);
        }
        public void ShowSettingView()
        {
            ActivateScreen(Screens.Setting);
        }
        public void ShowPlaneScheduleView()
        {
            ActivateScreen(Screens.PlaneSchedule);
        }
    }
}
