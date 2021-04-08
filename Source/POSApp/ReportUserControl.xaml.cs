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
using LiveCharts;
using LiveCharts.Wpf;

namespace POSApp
{
    /// <summary>
    /// Interaction logic for report.xaml
    /// </summary>
    public partial class ReportUserControl : UserControl
    {
        MyShopEntities db = new MyShopEntities();
        public ReportUserControl()
        {
            InitializeComponent();
        }
        public SeriesCollection purchaseData { get; set; }
        public SeriesCollection category_productData { get; set; }
        public void UserControl_Initialized(object sender, EventArgs e)
        {
            //top sell
            var bestSellProduct = (db.Products.GroupJoin(db.PurchaseDetails,
                                    p => p.Product_ID,
                                    d => d.Product_ID,
                                    (p, group) => new
                                    {
                                        Name = p.Product_Name,
                                        Thumbnail = p.Photos.FirstOrDefault().Data,
                                        Count = "Đã bán " + group.Count() + " sản phẩm"
                                    })).OrderByDescending(x => x.Count).ToList().Take(10);
            bestSellProductListView.ItemsSource = bestSellProduct;

            //sap het hang
            var almostOverProduct = (from product in db.Products
                                     where product.Quantity < 10
                                     select new
                                     {
                                         Id = product.Product_ID,
                                         Name = product.Product_Name,
                                         Price = product.Price,
                                         Quantity = "Còn " + product.Quantity + " sản phẩm",
                                         Thumbnail = product.Photos.FirstOrDefault().Data
                                     }).ToList();
            almostOverProductListView.ItemsSource = almostOverProduct;

            // Chart
            purchaseData = new SeriesCollection() { };
            category_productData = new SeriesCollection() { };

            var purchase = (db.Purchases.GroupJoin(db.PurchaseStatusEnums,
                                    p => p.Status,
                                    s => s.Value,
                                    (p, group) => new
                                    {
                                        Name = p.PurchaseStatusEnum.Display_Text,
                                    })).ToList();
            foreach (var item in purchase.GroupBy(x => x.Name).Select(g => new { g.Key, Count = g.Count() }))
            {
                PieSeries Pie = new PieSeries()
                {
                    Values = new ChartValues<float> { item.Count },
                    Title = $"{item.Key}"
                };
                purchaseData.Add(Pie);
            }

            var category_product = (db.Products.GroupJoin(db.Categories,
                                    p => p.Catgory_ID,
                                    c => c.Category_ID,
                                    (p, group) => new
                                    {
                                        Name = p.Category.Category_Name,
                                    })).ToList();
            foreach (var item in category_product.GroupBy(x => x.Name).Select(g => new { g.Key, Count = g.Count() }))
            {
                PieSeries Pie = new PieSeries()
                {
                    Values = new ChartValues<float> { item.Count },
                    Title = $"{item.Key}"
                };
                category_productData.Add(Pie);
            }

            category_product_piechart.Series = category_productData;
            purchase_pie_chart.Series = purchaseData;
        }
        public delegate void StatisticsDelegate(object sender, RoutedEventArgs e);
        public event StatisticsDelegate sd;
        public void statistics_click(object sender, RoutedEventArgs e)
        {
            sd?.Invoke(sender, e);
        }

    }
}

