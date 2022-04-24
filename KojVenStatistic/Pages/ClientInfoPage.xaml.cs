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
    /// Interaction logic for ClientInfoPage.xaml
    /// </summary>
    public partial class ClientInfoPage : Page
    {
        Client _client;
        DateTime date;
        public ClientInfoPage(Client client)
        {
            InitializeComponent();
            this.DataContext = client;
            SPOptional.DataContext = client.LastAppeal;
            CBoxType.ItemsSource = AppData.Context.AppealType.ToList();
            CBoxType.DisplayMemberPath = "Name";
            CBoxType.Text = client.LastAppeal.AppealType.Name;
            if (AppData.AuthUser.PostId == 1)
            {
                BtnEdit.Visibility = Visibility.Visible;
                DatePickerBirth.IsEnabled = true;
                DatePickerAppeal.IsEnabled = true;
                TBoxAddress.IsEnabled = true;
                TBoxDisease.IsEnabled = true;
                TBoxMed.IsEnabled = true;
                TBoxPassportNumber.IsEnabled = true;
                TBoxPassportSerial.IsEnabled = true;
                TBoxPhone.IsEnabled = true;
                TBoxSnils.IsEnabled = true;
                CBoxType.IsEnabled = true;
            }
            else
            {
                BtnEdit.Visibility = Visibility.Collapsed;
            }
            _client = client;
            date = client.LastAppeal.DateOfRequest;
        }

        private void BtnAddMedicine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGoPrevious_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BtnGoNext.IsEnabled = true;
                SPOptional.DataContext = _client.Appeal.OrderBy(i => i.DateOfRequest).First(i => i.DateOfRequest < date);
                CBoxType.Text = _client.Appeal.OrderBy(i => i.DateOfRequest).First(i => i.DateOfRequest < date).AppealType.Name;
                date = _client.Appeal.OrderBy(i => i.DateOfRequest).First(i => i.DateOfRequest < date).DateOfRequest;
            }
            catch (Exception)
            {
                BtnGoPrevious.IsEnabled = false;
                MessageBox.Show("Больше нет записей по этому клиенту");
                //BtnGoNext.Source = Properties.Resources.left_arrow_disabled;

            }
        }

        private void BtnGoNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BtnGoPrevious.IsEnabled = true;
                SPOptional.DataContext = _client.Appeal.OrderBy(i => i.DateOfRequest).First(i => i.DateOfRequest > date);
                CBoxType.Text = _client.Appeal.OrderBy(i => i.DateOfRequest).First(i => i.DateOfRequest > date).AppealType.Name;
                date = _client.Appeal.OrderBy(i=>i.DateOfRequest).First(i => i.DateOfRequest > date).DateOfRequest;
            }
            catch (Exception)
            {
                BtnGoNext.IsEnabled = false;
                MessageBox.Show("Больше нет записей по этому клиенту");
                //BtnGoNext.Source = Properties.Resources.left_arrow_disabled;
            }

             
       }
    }
}
