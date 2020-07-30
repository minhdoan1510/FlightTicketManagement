using Caliburn.Micro;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace FlightTicketManagement.ViewModels
{
    public class ChairGridViewModel : Screen
    {
        private int maxSizeItem; //kích thước item
        private int maxW; //số lượng item ngang
        //private int maxH;
        private int maxSize;
        private BindingList<ChairBooking> chairList;
        private ChairBooking chairSelected;
        private int xchairSelected;
        private int ychairSelected;

        public int MaxW { get => maxW; 
            set { 
                maxW = value; 
                MaxSize = MaxSizeItem * (maxW+5); 
                NotifyOfPropertyChange(() => MaxW); } }
        //public int MaxH { get => maxH; set { maxH = value; NotifyOfPropertyChange(() => MaxH); } }
        public BindingList<ChairBooking> ChairList { get => chairList; set { chairList = value; NotifyOfPropertyChange(() => ChairList); } }

        public int MaxSize { get => maxSize; set { maxSize = value; NotifyOfPropertyChange(() => MaxSize); } }

        public int MaxSizeItem { 
            get => maxSizeItem; 
            set { maxSizeItem = value; 
                NotifyOfPropertyChange(() => MaxSizeItem);
            } 
        }

        public ChairBooking ChairSelected
        {
            get => chairSelected;
            set { 
                chairSelected = value;
                XchairSelected = Convert.ToInt32(chairSelected.IDchair.Substring(1));
                YchairSelected = Convert.ToInt32(chairSelected.IDchair[0]) - 65;
                NotifyOfPropertyChange(() => ChairSelected);
            }
        }

        public int XchairSelected { 
            get => xchairSelected;
            set {
                xchairSelected = value;
            }
        }
        public int YchairSelected { 
            get => ychairSelected;
            set { 
                ychairSelected = value;
            }
        }

        public ChairGridViewModel()
        {
            Random random = new Random() ;
            MaxSizeItem = 50;
            MaxW = 10;
            ChairList = new BindingList<ChairBooking>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < maxW; j++)
                {
                    ChairList.Add(new ChairBooking() { IDchair = Convert.ToChar(i + 65).ToString() + j.ToString(),
                        Status = (ChairStatus)(random.Next(1,3)), 
                        TypeChair = (ChairType)(random.Next(1, 3))
                    });
                }
            }
        }

    }

    public class ConvertChairStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (((ChairStatus)value == ChairStatus.Booked) ?Brushes.Gray : Brushes.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConvertChairType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (((ChairType)value == ChairType.VIP) ? Brushes.Red : Brushes.DarkGreen);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}