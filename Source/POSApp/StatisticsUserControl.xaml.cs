using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

namespace POSApp
{
    /// <summary>
    /// Interaction logic for StatisticsUserControl.xaml
    /// </summary>
    public partial class StatisticsUserControl : UserControl
    {
        public StatisticsUserControl()
        {
            InitializeComponent();
        }
        MyShopEntities db = new MyShopEntities();
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection revenueData { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        public void UserControl_Initialized(object sender, EventArgs e)
        {
            revenueData = new SeriesCollection();

            var data = (from purchase in db.Purchases
                        where purchase.Status != 3
                        select purchase).ToList();

            var DateList = data.GroupBy(i => i.Created_At.HasValue ? i.Created_At.Value.ToString("yyyy/MM/dd") : "<not available>")
             .Select(i => new
             {
                 Day = i.Key,
                 Sum = i.Sum(x => x.Total)
             });

            ColumnSeries c = new ColumnSeries()
            {
                Title = "Tổng: ",
                Values = new ChartValues<int> { }
            };
            Labels = new List<string>();
            foreach (var s in DateList)
            {
                Labels.Add((DateTime.Parse(s.Day)).ToString("dd/MM/yyyy"));
                //Labels.Add(s.Created_At.HasValue ? s.Created_At.Value.ToString("dd/MM") : "<not available>");
                c.Values.Add(s.Sum);
            }

            revenueData.Add(c);

            //List<DateTime> days = new List<DateTime>();
            //days = GetDates(2020, 12);
            Formatter = value => value.ToString("0 VND");
            revenueStatistics.DataContext = this;
            revenueStatistics.UpdateLayout();
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }
    }
}
