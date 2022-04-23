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
    /// Interaction logic for NavigationPage.xaml
    /// </summary>
    public partial class NavigationPage : Page
    {
        private User _user;
        public NavigationPage()
        {
            InitializeComponent();
            _user = AppData.AuthUser;
            UserInfo.DataContext = _user;

            BtnAppeal.Visibility = Visibility.Collapsed;
            BtnInfo.Visibility = Visibility.Collapsed;
            BtnClients.Visibility = Visibility.Collapsed;
            BtnMedicaments.Visibility = Visibility.Collapsed;
            BtnUsers.Visibility = Visibility.Collapsed;

            switch (_user.PostId)
            {
                case 1:
                    BtnUsers.Visibility = Visibility.Visible;
                    BtnClients.Visibility = Visibility.Visible;
                    BtnMedicaments.Visibility = Visibility.Visible;
                    PageFrame.Navigate(new UsersPage());
                    BtnUsers.IsChecked = true;
                    break;
                case 2:
                    BtnClients.Visibility = Visibility.Visible;
                    BtnMedicaments.Visibility = Visibility.Visible;
                    PageFrame.Navigate(new ClientsPage());
                    BtnClients.IsChecked = true;
                    break;
                case 3:
                    BtnAppeal.Visibility = Visibility.Visible;
                    BtnInfo.Visibility = Visibility.Visible;
                    PageFrame.Navigate(new AppealPage());
                    BtnAppeal.IsChecked = true;
                    break;
                default:
                    break;
            }

           

        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new UsersPage());
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new ClientsPage());
        }

        private void BtnMedicaments_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new MedicamentsPage());
        }

        private void BtnAppeal_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new AppealPage());
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
