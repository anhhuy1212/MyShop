using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MasterDataUserControl master = new MasterDataUserControl();
        TransactionUserControl transaction = new TransactionUserControl();
        AboutUsUserControl aboutUs = new AboutUsUserControl();
        ReportUserControl report = new ReportUserControl();//TA thêm
        StatisticsUserControl statistics = new StatisticsUserControl();//TA thêm
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tabsContent.ItemsSource = new ObservableCollection<TabItem>()
            {
                new TabItem() { Content = master, Visibility = Visibility.Collapsed,},
                new TabItem() { Content = transaction, Visibility = Visibility.Collapsed},
                new TabItem() { Content = aboutUs, Visibility = Visibility.Collapsed},
                new TabItem() { Content = statistics, Visibility = Visibility.Collapsed},//TA thêm
                new TabItem() { Content = report, Visibility = Visibility.Collapsed},//TA thêm
            };

            tabsContent.SelectedIndex = 0;
            master.UserControl_Initialized(sender, e);
            report.sd += statistics_MouseLeftButtonDown;
        }

        private void masterData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabsContent.SelectedIndex = 0;

            //masterDataBorder.BorderThickness = new Thickness(1, 1, 1, 1);
            //transactionBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            master.UserControl_Initialized(sender, e);

        }

        private void logout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }

        private void transactionData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabsContent.SelectedIndex = 1;
            transaction.UserControl_Initialized(sender, e);
        }

        private void AboutUsData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabsContent.SelectedIndex = 2;
        }


        //TA thêm

        private void statistics_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            tabsContent.SelectedIndex = 3;
            statistics.UserControl_Initialized(sender, e);
        }

        private void report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabsContent.SelectedIndex = 4;
            report.UserControl_Initialized(sender, e);
        }

    }
}