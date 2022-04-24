using KojVenStatistic.Entity;
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

namespace KojVenStatistic.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = AppData.Context.User.ToList().FirstOrDefault(P => P.Login == TBoxLogin.Text && P.Password == PBoxPassword.Password);
                if (result != null)
                {
                    AppData.AuthUser = result;
                    AppData.MainFrame.Navigate(new NavigationPage());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
