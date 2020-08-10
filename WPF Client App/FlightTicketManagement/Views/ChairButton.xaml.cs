using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for ChairButton.xaml
    /// </summary>
    public partial class ChairButton : UserControl
    {
        public int posX, posY;
        public string classId;
        public bool isBought;

        public ChairButton() {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.mainBtn.Content = posY.ToString() + "-" + posX.ToString();

            if (this.isBought) {
                this.mainBtn.Background = Brushes.Gray;
                this.mainBtn.IsEnabled = false;
                return;
            }
            else {
                if (classId == "1") {
                    this.mainBtn.Background = Brushes.Gold;
                }
                else if (classId == "2") {
                    this.mainBtn.Background = Brushes.LightGreen;
                }
            }
        }

        public ChairButton(int posX, int posY, string classId, bool isBought) {
            this.posX = posX;
            this.posY = posY;
            this.classId = classId;
            this.isBought = isBought;

            InitializeComponent();
        }
    }
}
