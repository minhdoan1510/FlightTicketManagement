using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for MainApp.xaml
    /// </summary>

    public partial class MainAppView : Window
    {
        public MainAppView() {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void tileBar_MouseDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        private void Window_Closed(object sender, EventArgs e) {
            ShellView.Instance.Close(); 
        }
    }
}
