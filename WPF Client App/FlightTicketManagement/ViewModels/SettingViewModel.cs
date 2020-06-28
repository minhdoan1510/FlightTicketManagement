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
    class SettingViewModel:Screen
    {
        private RestrictionsModel restrictions;

        public RestrictionsModel Restrictions
        {
            get => restrictions;
            set
            {
                restrictions = value;
                NotifyOfPropertyChange(() => Restrictions);
            }
        }

        private TimeSpan time;

        public TimeSpan TestTime
        {
            get { return time; }
            set {
                time = value;
                NotifyOfPropertyChange(() => TestTime);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadRestrictions();
        }
        private async Task LoadRestrictions()
        {
            Response<RestrictionsModel> response = await APIHelper<Response<RestrictionsModel>>.Instance.Get(ApiRoutes.Restriction.Get);
            if (response.IsSuccess)
            {
                Restrictions = response.Result;
            }

        }
    }
}
