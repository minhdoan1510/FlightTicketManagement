using Caliburn.Micro;
using FlightTicketManagement.Helper;

using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.ViewModels
{
    class FlightListViewModel : Screen
    {
        public FlightListViewModel()
        {

        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadFlights();
        }
        private async Task LoadFlights()
        {
            Response <List<FlightModel>> response = await APIHelper<Response<List<FlightModel>>>.Instance.Get(ApiRoutes.Flight.GetAll);
            if (response.IsSuccess)
            {
                var list = response.Result;

                Flights = new BindingList<FlightModel>(list);
            }

        }
        private BindingList<FlightModel> _flights;

        public  BindingList<FlightModel> Flights
        {
            get => _flights;
            set
            {
                _flights = value;
                NotifyOfPropertyChange(() => Flights);
            }
        }
    }
}
