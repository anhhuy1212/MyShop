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
using System.Configuration;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace POSApp
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();


        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUsername.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tài khoản!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                textBoxUsername.Focus();
            }
            else if (textBoxPassword.SecurePassword.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                textBoxPassword.Focus();
            }
            else
            {   
               
                var connectionString = @"Data Source=.;Initial Catalog=MyShop;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);

                
                
                    connection.Open();
                    //command.Connection = connection;
                    string query = "Select * From Account Where username='" + textBoxUsername.Text + "' AND Matkhau='" + textBoxPassword.Password + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader dta = command.ExecuteReader();
                    if(dta.Read()==true)
                    {
                    //Lay thong tin server trong appconfig
                    var server = ConfigurationManager.AppSettings["server"];
                    var database = ConfigurationManager.AppSettings["database"];
                    

                        var username = textBoxUsername.Text;
                        var password = textBoxPassword.Password;

                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        if (Check.IsChecked == true)
                        {
                            config.AppSettings.Settings["username"].Value = username;
                            config.AppSettings.Settings["password"].Value = password;
                    }
                        else
                        {
                            config.AppSettings.Settings["username"].Value = "";
                            config.AppSettings.Settings["password"].Value = "";
                            config.AppSettings.Settings["entropy"].Value = "";
                        }
                        config.Save(ConfigurationSaveMode.Minimal);
                        ConfigurationManager.RefreshSection("appSettings");
                        var db = new MyShopEntities();
                        var account = db.Accounts.Find(username);
                        MessageBox.Show($"{account.username} - Quyền truy cập {account.rolename}");

                    loginProgressBar.IsIndeterminate = true;
                        for (int i = 3; i >= 1; i--) // Keep running progress Ring for 3 seconds  
                        {
                            await Task.Delay(1000);
                        }
                    loginProgressBar.IsIndeterminate = false;
                    if (account != null)
                    {
                        if (account.rolename == "admin")
                        {
                            var m = new MainWindow();
                            m.Show();
                            this.Close();
                        }
                        else
                        {
                            var s = new SaleWindow();
                            s.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Login failed!");
                    }
                }
                    else
                    {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!!!");
                    }
                    connection.Close();

            }
   
        }

        private void Settingbutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var setting = new SettingWindow();
            setting.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Lay thong tin server trong appconfig
            var server = ConfigurationManager.AppSettings["server"];
            var database = ConfigurationManager.AppSettings["database"];
            var username = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];
            //var EncryptedPassword = Convert.FromBase64String(ConfigurationManager.AppSettings["password"]);
            if(password.Length != 0)
            {
                textBoxUsername.Text = username;
                textBoxPassword.Password = password;
            }
            else
            {

            }

            
        }

        private void CreateAccountTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            var register = new RegisterWindow();
            register.ShowDialog();
        }

        private void Exitbutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
