using Caliburn.Micro;
using FlightTicketManagement.Helper;

using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.ViewModels
{
    public class TransitViewModel: Screen
    {
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            FlightId = "123123";
            await LoadTransits();
        }
        private async Task LoadTransits()
        {
            Response<List<TransitModel>> response = await APIHelper<Response<List<TransitModel>>>.Instance.Get(ApiRoutes.Transit.Get.Replace(ApiRoutes.Keybase,FlightId));
            if (response.IsSuccess)
            {
                var list = response.Result;

                Transits = new BindingList<TransitModel>(list);
            }

        }
        private BindingList<TransitModel> _transits;

        public BindingList<TransitModel> Transits
        {
            get => _transits;
            set
            {
                _transits = value;
                NotifyOfPropertyChange(() => Transits);
            }
        }
        private string flightId;

        public string FlightId
        {
            get { return flightId; }
            set { 
                flightId = value;
                NotifyOfPropertyChange(() => FlightId);
            }
        }

    }
}
