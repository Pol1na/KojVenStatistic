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
using System.Windows.Threading;

namespace KojVenStatistic.Pages
{
    /// <summary>
    /// Interaction logic for NavigationPage.xaml
    /// </summary>
    public partial class NavigationPage : Page
    {
        private User _user;
        private DispatcherTimer _timer = new DispatcherTimer();
        public NavigationPage()
        {
            InitializeComponent();
            _user = AppData.AuthUser;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            UserInfo.DataContext = _user;

            BtnAppeal.Visibility = Visibility.Collapsed;
            BtnInfo.Visibility = Visibility.Collapsed;
            BtnClients.Visibility = Visibility.Collapsed;
            BtnMedicaments.Visibility = Visibility.Collapsed;
            BtnUsers.Visibility = Visibility.Collapsed;
            BtnAppealsList.Visibility = Visibility.Collapsed;

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
                    BtnAppealsList.Visibility = Visibility.Visible;
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

            CheckNotifications();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckNotifications();
        }

        private void CheckNotifications()
        {
            using (KojVenStatisticEntities db = new KojVenStatisticEntities())
            {
                var user = db.User.ToList().FirstOrDefault(x => x.Id == _user.Id);
                if (user != null)
                {
                    var appeals = user.Appeal.ToList().Where(x => x.DateOfRequest.Date == DateTime.Now.Date &&
                            x.DateOfRequest.TimeOfDay > DateTime.Now.TimeOfDay && x.Comment == null);
                    if (appeals.Count() != 0)
                    {
                        BorderNotifications.Visibility = Visibility.Visible;
                        BorderNotifications.DataContext = appeals.Count() > 9 ? "+9" : appeals.Count().ToString();
                    }
                    else
                    {
                        BorderNotifications.Visibility = Visibility.Collapsed;
                    }
                }
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

        private void BtnAppealsList_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new AppealsListPage());
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window editor = new Windows.AddEditUserWindow(AppData.AuthUser);
            if( editor.ShowDialog() == true)
            {
                UserInfo.DataContext = AppData.AuthUser;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window editor = new Windows.AddEditUserWindow(AppData.AuthUser);
            if (editor.ShowDialog() == true)
            {
                UserInfo.DataContext = null;
                UserInfo.DataContext = AppData.AuthUser;

            }
        }
    }
}
