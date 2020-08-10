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
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;
using ServerFTM.Models;

namespace FlightTicketManagement.ViewModels
{
    class FlightListViewModel : Screen
    {
        private readonly IEventAggregator _events;
        public FlightListViewModel(IEventAggregator events) {
            _events = events;
        }

        protected override async void OnViewLoaded(object view) {
            base.OnViewLoaded(view);
            await LoadFlights();
            await LoadCity();
        }

        private async Task LoadFlights() {
            Response<List<FlightDisplayModel>> response = await APIHelper.Instance.Get<Response<List<FlightDisplayModel>>>(ApiRoutes.FlightT.GetAll);
            if (response.IsSuccess) {
                var list = response.Result;

                Flights = new ObservableCollection<FlightDisplayModel>(list);
            }

        }

        private async Task LoadFlightsForCity() {
            Response<List<FlightDisplayModel>> response = await APIHelper.Instance.Get<Response<List<FlightDisplayModel>>>(ApiRoutes.FlightT.GetFlightForCity.Replace(ApiRoutes.Keybase, SelectedCityId));
            if (response.IsSuccess) {
                var list = response.Result;

                Flights = new ObservableCollection<FlightDisplayModel>(list);
            }
        }

        private async Task LoadCity() {
            Response<List<CityModel>> response = await APIHelper.Instance.Get<Response<List<CityModel>>>(ApiRoutes.City.GetAll);
            if (response.IsSuccess) {
                var list = response.Result;

                list.Insert(0, new CityModel() {
                    Id = "000",
                    Name = "*** Get All Flights ***",
                    Country = "None"
                });
                Cities = new BindingList<CityModel>(list);
            }
        }

        private ObservableCollection<FlightDisplayModel> _flights;

        public ObservableCollection<FlightDisplayModel> Flights {
            get => _flights;
            set {
                _flights = value;
                NotifyOfPropertyChange(() => Flights);
            }
        }

        private BindingList<CityModel> _cities;

        public BindingList<CityModel> Cities {
            get => _cities;
            set {
                _cities = value;
                NotifyOfPropertyChange(() => Cities);
            }
        }

        //private MapCore flightMap;

        //public MapCore FlightMap
        //{
        //    get { return flightMap; }
        //    set { 
        //        flightMap = value;
        //        NotifyOfPropertyChange(() => FlightMap);
        //    }
        //}

        private ObservableCollection<object> mapItem;

        public ObservableCollection<object> MapItem {
            get { return mapItem; }

            set {
                mapItem = value;
                NotifyOfPropertyChange(() => MapItem);
            }
        }

        private Location center;

        public Location Center {
            get {
                if (center == null)
                    return new Location(0, 0);
                return center;
            }
            set {
                center = value;
                NotifyOfPropertyChange(() => Center);
            }
        }
        private double zoom;

        public double Zoom {
            get { return zoom; }
            set {
                zoom = value;
                NotifyOfPropertyChange(() => Zoom);
            }
        }
        public void CZoom() {
            Zoom = 5;
        }


        public void addNewPolygon() {
            MapPolygon polygon = new MapPolygon();
            polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            polygon.StrokeThickness = 5;
            polygon.Opacity = 0.7;
            polygon.Locations = new LocationCollection() {
                new Location(47.6424,-122.3219),
                new Location(47.8424,-122.1747),
                new Location(47.5814,-122.1747) };
            if (MapItem == null)
                MapItem = new ObservableCollection<object>();
            MapItem.Add(polygon);

        }

        public async void Refresh() {
            await LoadFlights();
            await LoadCity();
        }

        private string _selectedCityId;

        public string SelectedCityId {
            get { return _selectedCityId; }
            set {
                if (value == "000") {
                    Flights.Clear();
                    Task.WhenAll(Task.Run(() => LoadFlights()));
                    return;
                }
                _selectedCityId = value;
                NotifyOfPropertyChange(() => SelectedCityId);
                Flights.Clear();
                Task.WhenAll(Task.Run(() => LoadFlightsForCity()));
            }
        }
        private FlightDisplayModel _selectedFlight;

        public FlightDisplayModel SelectedFlight {
            get { return _selectedFlight; }
            set {
                _selectedFlight = value;

                NotifyOfPropertyChange(() => SelectedFlight);
                if (MapItem != null)
                    MapItem.Clear();
                Task.Run(() => {
                    loadFLightMap();
                });
            }
        }

        public void ShowTransit() {
            _events.PublishOnUIThread(new GetTransitEvent(SelectedFlight.Id));
        }

        public void BookTicket() {
            _events.PublishOnUIThread(new CreateTicketEvent(SelectedFlight.Id)); 
        }

        private async void loadFLightMap() {
            if (SelectedFlight == null)
                return;

            Response<List<FlightRoute>> response = await APIHelper.Instance.Get
                <Response<List<FlightRoute>>>(ApiRoutes.Flight.GetFlightRoute.Replace
                (ApiRoutes.Keybase, SelectedFlight.Id));

            FlightRoute flightRoute = response.Result[0];
            await Task.Factory.StartNew(() => {
                Application.Current.Dispatcher.Invoke(() => {
                    Pushpin originPin = createOriginPin(flightRoute);
                    Pushpin destinationPin = createDestinationPin(flightRoute);

                    List<Pushpin> transitPin = createTransitPin(flightRoute.transitList);
                    MapPolyline path = createPolygonLine(flightRoute);
                    if (MapItem == null)
                        MapItem = new ObservableCollection<object>();
                    MapItem.Add(path);
                    MapItem.Add(originPin);
                    MapItem.Add(destinationPin);

                    foreach (var transitPoint in transitPin) {
                        MapItem.Add(transitPoint);
                    }
                });

            });
            Center = new Location((flightRoute.latOrigin + flightRoute.latDestination) / 2, (flightRoute.lonOrigin + flightRoute.lonDestination) / 2);
        }

        private MapPolyline createPolygonLine(FlightRoute item) {
            MapPolyline result = new MapPolyline();
            result.Stroke = Brushes.Red;
            result.StrokeThickness = 1;

            LocationCollection line = new LocationCollection();

            line.Add(new Location() {
                Latitude = item.latOrigin,
                Longitude = item.lonOrigin
            });

            foreach (TransitLocation trans in item.transitList) {
                line.Add(new Location() {
                    Latitude = trans.transitLat,
                    Longitude = trans.transitLon
                });
            }

            line.Add(new Location() {
                Latitude = item.latDestination,
                Longitude = item.lonDestination
            });
            result.Locations = line;

            return result;
        }

        private Pushpin createOriginPin(FlightRoute item) {
            Pushpin originPin = new Pushpin();
            originPin.Background = Brushes.Green;
            originPin.Location = new Location();

            originPin.Location.Latitude = item.latOrigin;
            originPin.Location.Longitude = item.lonOrigin;
            originPin.ToolTip = item.originName;

            return originPin;
        }

        private Pushpin createDestinationPin(FlightRoute item) {
            Pushpin destinationPin = new Pushpin();
            destinationPin.Background = Brushes.Red;
            destinationPin.Location = new Location() {
                Latitude = item.latDestination,
                Longitude = item.lonDestination
            };
            destinationPin.ToolTip = item.destinationName;

            return destinationPin;
        }

        private List<Pushpin> createTransitPin(List<TransitLocation> transitLocation) {
            List<Pushpin> result = new List<Pushpin>();

            foreach (TransitLocation item in transitLocation) {
                Pushpin transitPin = new Pushpin();
                transitPin.Background = Brushes.Yellow;
                transitPin.Location = new Location() {
                    Latitude = item.transitLat,
                    Longitude = item.transitLon
                };
                transitPin.ToolTip = item.transitName;

                result.Add(transitPin);
            }
            return result;
        }

    }
}
namespace Helpers
{
    public static class MapHelper
    {
        public static readonly DependencyProperty Center = DependencyProperty.RegisterAttached(
            "Center",
            typeof(Location),
            typeof(MapHelper),
            new PropertyMetadata(new Location(), new PropertyChangedCallback(CenterChanged))
        );

        public static void SetCenter(DependencyObject obj, Location value) {
            obj.SetValue(Center, value);
        }

        public static Location GetCenter(DependencyObject obj) {
            return (Location)obj.GetValue(Center);
        }

        private static void CenterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
            Map map = (Map)obj;
            if (map != null)
                map.Center = (Location)args.NewValue;
        }
    }
}