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
        User users;
        public NavigationPage()
        {
            InitializeComponent();
            users = AppData.Context.User.FirstOrDefault(P => P.Id == Properties.Settings.Default.UserId);
            User.Text = users.LastName + " " + users.FirstName;
           // Ellipse.Fill = users.Photo;
            //PageFrame.Navigate(new UsersPage());
        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new UsersPage());
        }

        private void ClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new ClientsPage());
        }

        private void MedBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(new MedicamentsPage());
        }
    }
}
