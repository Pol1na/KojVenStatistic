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
        public ClientInfoPage(Client client)
        {
            InitializeComponent();
            this.DataContext = client;
            if (AppData.user.PostId == 1)
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
        }

        private void BtnAddMedicine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
