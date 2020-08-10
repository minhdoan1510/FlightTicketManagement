using Library.Models;
using ServerFTM.Models;
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
using System.Windows.Shapes;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for ChairGrid.xaml
    /// </summary>
    public partial class ChairGrid : Window
    {
        ChairState chairState;
        public List<Chair> listChair; 

        public ChairGrid() {
            InitializeComponent();
        }

        public ChairGrid(ChairState value) {
            chairState = value;
            listChair = new List<Chair>();
            InitializeComponent();
        }

        public void initData() {
            int[,] arr = new int[this.chairState.height, this.chairState.width];

            foreach (var item in this.chairState.chairBooked) {
                arr[item.posY - 1, item.posX - 1] = 1;
            }

            for (int i = 0; i < this.chairState.height; i++) {
                for (int j = 0; j < this.chairState.width; j++) {
                    ChairButton newBtn = null;

                    if (arr[i, j] == 1) {
                        newBtn = new ChairButton(j + 1, i + 1, "1", true);
                        newBtn.Margin = new Thickness(5);
                        newBtn.mainBtn.Click += chairClick;
                    }
                    else {
                        string classId = "2";
                        if (i <= 4) 
                            classId = "1";

                        newBtn = new ChairButton(j + 1, i + 1, classId, false);
                        newBtn.Margin = new Thickness(5);
                        newBtn.mainBtn.Click += chairClick;
                    }
                    this.chairPanel.Children.Add(newBtn);
                }
            }
        }

        public void chairClick(object sender, RoutedEventArgs e) {
            Button btn = sender as Button;
            btn.Background = Brushes.Gray;
            btn.IsEnabled = false;

            ChairButton parent = btn.Parent as ChairButton;
            listChair.Add(new Chair() {
                posX = parent.posX,
                posY = parent.posY,
                classID = parent.classId
            });
        }
    }
}
