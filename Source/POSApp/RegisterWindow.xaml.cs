using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace POSApp
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void NhapLaiLuonNeBanOi()
        {
            TaiKhoanTextBox.Text = "";
            PasswordTextBox.Clear();
            RepasswordTextBox.Clear();
            HoTenTextBox.Text = "";
            DiaChiTextBox.Text = "";
            SDTTextBox.Text = "";
            EmailTextBox.Text = "";
        }

        private void ButtonNhapLai_Click(object sender, RoutedEventArgs e)
        {
            NhapLaiLuonNeBanOi();
        }

        private void ButtonDangKy_Click(object sender, RoutedEventArgs e)
        {
            if (TaiKhoanTextBox.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                TaiKhoanTextBox.Focus();
            }
            else if (PasswordTextBox.Password.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                PasswordTextBox.Focus();
            }
            else if (RepasswordTextBox.Password.Length == 0)
            {
                MessageBox.Show("Vui lòng xác nhận lại mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                RepasswordTextBox.Focus();
            }
            else if(PasswordTextBox.Password.ToString() != RepasswordTextBox.Password.ToString())
            {
                MessageBox.Show("Mật khẩu không trùng khớp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                RepasswordTextBox.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=MyShop;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Account(username,rolename,Matkhau,HoTen,DiaChi,SDT,Email) values ('"+TaiKhoanTextBox.Text+"','guess', '"+PasswordTextBox.Password.ToString()+"','"+HoTenTextBox.Text+"','"+DiaChiTextBox.Text+"','"+SDTTextBox.Text+"','"+EmailTextBox.Text+"')");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Đăng ký thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                NhapLaiLuonNeBanOi();
            }
        }

        private void ButtonDong_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
