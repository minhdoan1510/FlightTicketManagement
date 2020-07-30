using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for ChairGridView.xaml
    /// </summary>
    public partial class ChairGridView : UserControl
    {
        public int DefineW { get; set; }
        public int DefineH { get; set; }

        public List<ChairBooking> DataChairs;
        ObservableCollection<string> items = new ObservableCollection<string>();
        

    public ChairGridView()
        {
            InitializeComponent();
            ChairList.ItemsSource = items;
        }

        public void SetDefineDataChair(int W, int H)
        {
            DefineW = W;
            DefineH = H;
        }

        public void SetDataChair(List<ChairBooking> dataChairs)
        {
            DataChairs = dataChairs;
            UpdateListView();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Background == Brushes.Transparent)
            {
                btn.Background = Brushes.Green;
            }
            else
            {
                btn.Background = Brushes.Transparent;
            }
        }

        public string ChairSelected()
        {
            for (int i = 0; i < ChairList.Items.Count; i++)
            {
                Button btn = ChairList.Items[i] as Button;
                if (btn.Background == Brushes.Green)
                    return btn.Content.ToString();
            }
            return null;
        }

        public void ChangeDataChair(List<ChairBooking> dataChairs)
        {
            DataChairs = dataChairs;

            UpdateListView();
        }

        private void UpdateListView()
        {
            bool flagBooked;
            for (int i = 0; i < DefineH; i++)
            {
                for (int j = 0; j < DefineW; j++)
                {
                    flagBooked = false;
                    if (DataChairs != null)
                        for (int k = 0; k < DataChairs.Count; k++)
                        {
                            if (DataChairs[k].Equals(Convert.ToChar(i + 65).ToString() + j.ToString()))
                            {
                                flagBooked = true;
                                break;
                            }
                        }

                    Button btn = new Button() { Content = Convert.ToChar(i + 65).ToString() + j.ToString() };
                    if (j < 4)
                    {
                        btn.BorderBrush = Brushes.Yellow;
                    }
                    else
                    {
                        btn.BorderBrush = Brushes.Brown;
                    }
                    if (flagBooked)
                    {
                        btn.Tag = DataChairs[i];
                        btn.IsEnabled = false;
                        btn.Background = Brushes.Gray;
                    }
                    else
                    {
                        btn.Background = Brushes.Transparent;
                        btn.Click += Btn_Click;
                    }
                    btn.BorderThickness = new Thickness(2);
                    items.Add(btn.Content.ToString());
                }
            }
            //ChairList.Items.Refresh();
        }
    }

    public class WrapPanelWithRowsOrColumnsCount : WrapPanel
    {
        public static readonly DependencyProperty RowsOrColumnsCountProperty =
            DependencyProperty.Register(
                "RowsOrColumnsCount",
                typeof(int),
                typeof(WrapPanelWithRowsOrColumnsCount),
                new PropertyMetadata(int.MaxValue));

        public int RowsOrColumnsCount
        {
            get { return (int)GetValue(RowsOrColumnsCountProperty); }
            set { SetValue(RowsOrColumnsCountProperty, Math.Max(value, 1)); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count > 0)
            {
                Size newAvailableSize;

                if (Orientation == Orientation.Horizontal)
                {
                    var suitableWidth = EstimateSuitableRowOrColumnLength(Children.Cast<UIElement>(),
                                                                            true,
                                                                            availableSize,
                                                                            RowsOrColumnsCount);

                    newAvailableSize =
                        double.IsNaN(suitableWidth) || suitableWidth <= 0
                            ? availableSize
                            : new Size(Math.Min(suitableWidth, availableSize.Width), availableSize.Height);
                }
                else
                {
                    var suitableHeigth = EstimateSuitableRowOrColumnLength(Children.Cast<UIElement>(),
                                                                            false,
                                                                            availableSize,
                                                                            RowsOrColumnsCount);
                    newAvailableSize =
                        double.IsNaN(suitableHeigth) || suitableHeigth <= 0
                            ? availableSize
                            : new Size(availableSize.Width, Math.Min(suitableHeigth, availableSize.Height));
                }

                return base.MeasureOverride(newAvailableSize);
            }
            else
            {
                return base.MeasureOverride(availableSize);
            }
        }

        private double EstimateSuitableRowOrColumnLength(IEnumerable<UIElement> elements,
                                                            bool trueRowsFalseColumns,
                                                            Size availableSize,
                                                            int rowsOrColumnsCount)
        {
            var elementsList = elements.ToList();

            var desiredLengths = elementsList.Select(el => DesiredLength(el, availableSize, trueRowsFalseColumns)).ToList();

            var maxLength = desiredLengths.Where(length => !double.IsNaN(length)).Concat(new[] { 0.0 }).Max();

            if (maxLength <= 0.0)
            {
                return double.NaN;
            }

            var desiredLengthsRepaired = desiredLengths.Select(length => double.IsNaN(length) ? maxLength : length).ToList();

            var totalDesiredLength = desiredLengthsRepaired.Sum();

            var maxCount = Math.Min(rowsOrColumnsCount, elementsList.Count);

            var suitableRowOrColumnLength = totalDesiredLength / maxCount;

            double nextLengthIncrement;

            while (CountRowsOrColumnsNumber(desiredLengthsRepaired, suitableRowOrColumnLength, out nextLengthIncrement) > maxCount)
            {
                suitableRowOrColumnLength += nextLengthIncrement;
            }

            suitableRowOrColumnLength = Math.Max(suitableRowOrColumnLength, desiredLengthsRepaired.Max());

            return suitableRowOrColumnLength;
        }

        private int CountRowsOrColumnsNumber(List<double> desiredLengths, double rowOrColumnLengthLimit, out double nextLengthIncrement)
        {
            int rowOrColumnCount = 1;
            double currentCumulativeLength = 0;
            bool nextNewRowOrColumn = false;

            var minimalIncrement = double.MaxValue;

            foreach (var desiredLength in desiredLengths)
            {
                if (nextNewRowOrColumn)
                {
                    rowOrColumnCount++;
                    currentCumulativeLength = 0;
                    nextNewRowOrColumn = false;
                }

                if (currentCumulativeLength + desiredLength > rowOrColumnLengthLimit)
                {
                    minimalIncrement = Math.Min(minimalIncrement,
                                                currentCumulativeLength + desiredLength - rowOrColumnLengthLimit);

                    if (currentCumulativeLength == 0)
                    {
                        nextNewRowOrColumn = true;
                        currentCumulativeLength = 0;
                    }
                    else
                    {
                        rowOrColumnCount++;
                        currentCumulativeLength = desiredLength;
                    }
                }
                else
                {
                    currentCumulativeLength += desiredLength;
                }
            }

            nextLengthIncrement = minimalIncrement != double.MaxValue ? minimalIncrement : 1;

            return rowOrColumnCount;
        }

        private double DesiredLength(UIElement el, Size availableSize, bool trueRowsFalseColumns)
        {
            el.Measure(availableSize);
            Size next = el.DesiredSize;

            var length = trueRowsFalseColumns ? next.Width : next.Height;

            if (Double.IsInfinity(length) ||
                Double.IsNaN(length))
            {
                return Double.NaN;
            }
            else
            {
                return length;
            }

        }
    }
}
