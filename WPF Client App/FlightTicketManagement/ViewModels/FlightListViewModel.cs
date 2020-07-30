using Caliburn.Micro;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Helper;

using Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace FlightTicketManagement.ViewModels
{

    class FlightListViewModel : Screen
    {
        private readonly IEventAggregator _events;
        public FlightListViewModel(IEventAggregator events)
        {
            _events = events;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadFlights();
            await LoadCity();
        }

        private async Task LoadFlights()
        {
            Response <List<FlightDisplayModel>> response = await APIHelper.Instance.Get<Response<List<FlightDisplayModel>>>(ApiRoutes.FlightT.GetAll);
            if (response.IsSuccess)
            {
                var list = response.Result;

                Flights = new ObservableCollection<FlightDisplayModel>(list);
            }

        }

        private async Task LoadFlightsForCity()
        {
            Response<List<FlightDisplayModel>> response = await APIHelper.Instance.Get<Response<List<FlightDisplayModel>>>(ApiRoutes.FlightT.GetFlightForCity.Replace(ApiRoutes.Keybase,SelectedCityId));
            if (response.IsSuccess)
            {
                var list = response.Result;
                
                Flights = new ObservableCollection<FlightDisplayModel>(list);
            }

        }
        private async Task LoadCity()
        {
            Response<List<CityModel>> response = await APIHelper.Instance.Get<Response<List<CityModel>>>(ApiRoutes.City.GetAll);
            if (response.IsSuccess)
            {
                var list = response.Result;

                Cities = new BindingList<CityModel>(list);
            }

        }

        private ObservableCollection<FlightDisplayModel> _flights;

        public ObservableCollection<FlightDisplayModel> Flights
        {
            get => _flights;
            set
            {
                _flights = value;
                NotifyOfPropertyChange(() => Flights);
            }
        }

        private BindingList<CityModel> _cities;

        public BindingList<CityModel> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                NotifyOfPropertyChange(() => Cities);
            }
        }

        //private CityModel _selectedCity;
        //public CityModel SelectedCity
        //{
        //    get { return _selectedCity; }
        //    set { 
        //        _selectedCity = value;
                
                

        //    }
        //}
        private string _selectedCityId;

        public string SelectedCityId
        {
            get { return _selectedCityId; }
            set { 
                _selectedCityId = value;
                NotifyOfPropertyChange(() => SelectedCityId);
                Flights.Clear();
                Task.WhenAll(Task.Run(() => LoadFlightsForCity()));
                
            }
        }
        private FlightDisplayModel _selectedFlight;

        public FlightDisplayModel SelectedFlight
        {
            get { return _selectedFlight; }
            set { _selectedFlight = value; }
        }

        public void ShowTransit()
        {
            _events.PublishOnUIThread(new GetTransitEvent(SelectedFlight.Id));
        }


    }
}
