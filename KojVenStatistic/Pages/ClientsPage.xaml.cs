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
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            CBoxSort.ItemsSource = new string[] { "Без сортировки", "По имени (возр.)", "По имени (убыв.)", "По дате рождения (возр.)", "По дате рождения (убыв.)" };
            CBoxSort.SelectedIndex = 0;
        }

        private void UpdateList()
        {
            List<Client> clients = AppData.Context.Client.ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                clients = clients.Where(x => x.FullName.ToLower().Contains(text) || x.MedPolis.ToLower().Contains(text) || x.Snils.ToLower().Contains(text)).ToList();

            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    clients = clients.OrderBy(x => x.FullName).ToList();
                    break;
                case 2:
                    clients = clients.OrderByDescending(x => x.FullName).ToList();
                    break;
                case 3:
                    clients = clients.OrderBy(x => x.DateOfBirth).ToList();
                    break;
                case 4:
                    clients = clients.OrderByDescending(x => x.DateOfBirth).ToList();
                    break;
                default:
                    break;
            }


            LViewClients.ItemsSource = clients;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();

            if(!string.IsNullOrEmpty(TBoxSearch.Text))
                TBlockPlaceholer.Visibility = Visibility.Collapsed;
            else
                TBlockPlaceholer.Visibility = Visibility.Visible;
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
