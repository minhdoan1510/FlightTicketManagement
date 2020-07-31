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
    class SettingViewModel : Screen
    {

        private DateTime _minFlightTime;

        public DateTime MinFlightTime
        {
            get { return _minFlightTime; }
            set
            {
                _minFlightTime = value;
                NotifyOfPropertyChange(() => MinFlightTime);
            }
        }
        private int _maxTransit;

        public int MaxTransit
        {
            get { return _maxTransit; }
            set
            {
                _maxTransit = value;
                NotifyOfPropertyChange(() => MaxTransit);
            }
        }
        private DateTime _minTransitTime;

        public DateTime MinTransitTime
        {
            get { return _minTransitTime; }
            set
            {
                _minTransitTime = value;
                NotifyOfPropertyChange(() => MinTransitTime);
            }
        }

        private DateTime _maxTransitTime;

        public DateTime MaxTransitTime
        {
            get { return _maxTransitTime; }
            set
            {
                _maxTransitTime = value;
                NotifyOfPropertyChange(() => MaxTransitTime);
            }
        }

        private int _latestBookingTime;

        public int LatestBookingTime
        {
            get { return _latestBookingTime; }
            set
            {
                _latestBookingTime = value;
                NotifyOfPropertyChange(() => LatestBookingTime);
            }
        }

        private int _latestCancelingTime;

        public int LatestCancelingTime
        {
            get { return _latestCancelingTime; }
            set
            {
                _latestCancelingTime = value;
                NotifyOfPropertyChange(() => LatestCancelingTime);
            }
        }



        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadRestrictions();
        }

        private async Task LoadRestrictions()
        {
            Response<RestrictionsModel> response = await APIHelper.Instance.Get<Response<RestrictionsModel>>(ApiRoutes.Restriction.Get);
            if (response.IsSuccess)
            {
                RestrictionsModel result = response.Result;
                MinFlightTime = new DateTime() + result.MinFlightTime;
                MaxTransit = result.MaxTransit;
                MinTransitTime = new DateTime() + result.MinTransitTime;
                MaxTransitTime = new DateTime() + result.MaxTransitTime;
                LatestBookingTime = result.LatestBookingTime;
                LatestCancelingTime = result.LatestCancelingTime;

            }

        }

        public async void Reset()
        {
            await LoadRestrictions();
            Editting = false;
        }

        private bool _editting;

        public bool Editting
        {
            get { return _editting; }
            set
            {
                _editting = value;
                NotifyOfPropertyChange(() => Editting);
                NotifyOfPropertyChange(() => CanStartEdit);
            }

        }

        public bool CanStartEdit
        {
            get { return !Editting; }

        }
        public void StartEdit()
        {
            Editting = true;
        }
        public async Task Save()
        {
            RestrictionsModel restrictionsModel = new RestrictionsModel { 
                LatestBookingTime = this.LatestBookingTime,
                LatestCancelingTime = this.LatestCancelingTime,
                MaxTransit = this.MaxTransit,
                MaxTransitTime = this.MaxTransitTime.TimeOfDay,
                MinFlightTime = this.MinFlightTime.TimeOfDay,
                MinTransitTime = this.MinTransitTime.TimeOfDay };
            await APIHelper.Instance.Post<RestrictionsModel>(ApiRoutes.Restriction.Post, restrictionsModel);
            Editting = false;
            Reset();
        }
    }
}
