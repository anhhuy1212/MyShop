using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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
using System.Data.Sql;
using System.Data.SqlClient;

namespace POSApp
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            var server = ServerTextBox.Text;
            var database = DbTextBox.Text;
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.InitialCatalog = database;
            builder.UserID = username;
            builder.Password = password;

            var connectionString = builder.ConnectionString;

            //Thu connect voi db
            var db = new MyShopEntities(connectionString);

            var tc = db.TestConnection();

            if(tc == true)
            {
                MessageBox.Show("Connect is OK");
                //Chuyen toi man hinh chinh
            }
            else
            {
                MessageBox.Show("Connect was failed");
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var server = ServerTextBox.Text;
            var database = DbTextBox.Text;
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["server"].Value = server;
            config.AppSettings.Settings["database"].Value = database;
            config.AppSettings.Settings["username"].Value = username;
            config.AppSettings.Settings["password"].Value = password;

            //Xu ly ma hoa password
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            var cyperText = ProtectedData.Protect(passwordInBytes, entropy, DataProtectionScope.CurrentUser);

            config.AppSettings.Settings["password"].Value = Convert.ToBase64String(cyperText);
            config.AppSettings.Settings["entropy"].Value = Convert.ToBase64String(entropy);

            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");

            DialogResult = true;
        }
    }
}
