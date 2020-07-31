using Caliburn.Micro;
using FlightTicketManagement.Helper;
using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace FlightTicketManagement.ViewModels
{
    public class ReportViewModel : Screen
    {
        private BindingList<ChildMonthReport> monthReports;
        public BindingList<ChildMonthReport> MonthReports
        {
            get => monthReports;
            set
            {
                monthReports = value;
                NotifyOfPropertyChange(() => MonthReports);
            }
        }

        private BindingList<ChildYearReport> yearReports;
        public BindingList<ChildYearReport> YearReports
        {
            get => yearReports;
            set
            {
                yearReports = value;
                NotifyOfPropertyChange(() => YearReports);
            }
        }

        private DateTime monthProfit;
        public DateTime MonthProfit
        {
            get => monthProfit; set
            {
                monthProfit = value;
                LoadMonthReport();
            }
        }

        public BindingList<int> DataCbYear { get; set; }
        private int selectedIndex;
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; LoadYearReport(); NotifyOfPropertyChange(() => SelectedIndex); } }

        private Visibility isLoadingMonthReport;
        public Visibility IsLoadingMonthReport { get => isLoadingMonthReport; set { isLoadingMonthReport = value; NotifyOfPropertyChange(() => IsLoadingMonthReport); } }

        private Visibility isLoadingYearReport;
        public Visibility IsLoadingYearReport { get => isLoadingYearReport; set { isLoadingYearReport = value; NotifyOfPropertyChange(() => IsLoadingYearReport); } }



        public ReportViewModel()
        {
            DisplayName = "Báo cáo";
            MonthProfit = DateTime.Now;
            DataCbYear = new BindingList<int>();
            for (int i = DateTime.Now.Year; i >= 2000; i--)
            {
                DataCbYear.Add(i);
            }
            SelectedIndex = 0;
        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadMonthReport();
            await LoadYearReport();
        }

        private async Task LoadMonthReport()
        {
            IsLoadingMonthReport = Visibility.Visible;
            Response<List<ChildMonthReport>> response = await APIHelper.Instance.Get<Response<List<ChildMonthReport>>>(ApiRoutes.Report.GetMonthReport,
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>( "month",MonthProfit.Month.ToString()),
                    new KeyValuePair<string, string>("year",MonthProfit.Year.ToString())});
            if (response.IsSuccess)
            {
                MonthReports = new BindingList<ChildMonthReport>(response.Result);
                IsLoadingMonthReport = Visibility.Hidden;
            }
            else
            {
                Debug.WriteLine(response.ErrorCode + ": " + response.ErrorMessenge);
            }

        }

        private async Task LoadYearReport()
        {
            IsLoadingYearReport = Visibility.Visible;
            Response<List<ChildYearReport>> yearresponse = await APIHelper.Instance.Get<Response<List<ChildYearReport>>>(ApiRoutes.Report.GetYearReport,
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("year",DataCbYear[SelectedIndex].ToString())});
            if (yearresponse.IsSuccess)
            {
                YearReports = new BindingList<ChildYearReport>(yearresponse.Result);
                IsLoadingYearReport = Visibility.Hidden;
            }
            else
            {
                Debug.WriteLine(yearresponse.ErrorCode + ": " + yearresponse.ErrorMessenge);
            }
        }

    }
}
