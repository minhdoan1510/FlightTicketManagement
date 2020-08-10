using Caliburn.Micro;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Helper;
using FlightTicketManagement.Views;
using Library.Models;
using Models;
using ServerFTM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace FlightTicketManagement.ViewModels
{
    public class CreateTicketViewModel : Conductor<object>
    {
        private IEventAggregator _events;
        SimpleContainer _container;

        public CreateTicketViewModel(IEventAggregator events, SimpleContainer container) {
            _events = events;
            _container = container;
        }

        public void BookTicket() {
            _events.PublishOnUIThread(new BookTicketEvent());
        }
    }
}
