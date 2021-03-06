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
        private Client _client;
        private int _selectedPage;
        private int _pagesCount;
        private Appeal _currentAppeal;
        public ClientInfoPage(Client client)
        {
            InitializeComponent();
            this.DataContext = client;
            SPOptional.DataContext = client.LastAppeal;
            CBoxType.ItemsSource = AppData.Context.AppealType.ToList();
            if (AppData.AuthUser.PostId == 1)
            {
                BtnEdit.Visibility = Visibility.Visible;  
            }
            else
            {
                BtnEdit.Visibility = Visibility.Collapsed;
            }

            if(AppData.AuthUser.PostId == 3)
            {
                BtnReport.Visibility = Visibility.Visible;
            }
            else
            {
                BtnReport.Visibility = Visibility.Collapsed;
            }

            _client = client;



            _pagesCount = client.Appeal.Count();
            _selectedPage = _pagesCount - 1; 
            
            if(_pagesCount == 0)
            {
                TBlockEmpty.Visibility = Visibility.Visible;
                SPOptional.Visibility = Visibility.Collapsed;
            }
            else
            {
                TBlockEmpty.Visibility = Visibility.Collapsed;
                SPOptional.Visibility = Visibility.Visible;
            }

            ChangePage();
            UpdateArrows();
        }

        private void BtnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.AddNewMedicineForUserWindow(_currentAppeal);
            editor.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(new Windows.ClientEditorWindow(_client).ShowDialog() == true)
            {
                DataContext = null;
                DataContext = _client;
            }
        }

        private void BtnGoPrevious_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(_selectedPage > 0)
            {
                _selectedPage--;
                ChangePage();
            }
            UpdateArrows();
        }
        private void BtnGoNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_selectedPage < _pagesCount - 1)
            {
                _selectedPage++;
                ChangePage();
            }
            UpdateArrows();

        }

        private void UpdateArrows()
        {
            // Правая стрелка
            if (_selectedPage != _pagesCount - 1)
            {
                BtnGoNext.Source = new BitmapImage(new Uri("/KojVenStatistic;component/Assets/left_arrow.png", UriKind.Relative));
            }
            else
            {
                BtnGoNext.Source = new BitmapImage(new Uri("/KojVenStatistic;component/Assets/left_arrow_disabled.png", UriKind.Relative));
            }
            // Левая стрелка
            if (_selectedPage != 0)
            {
                BtnGoPrevious.Source = new BitmapImage(new Uri("/KojVenStatistic;component/Assets/left_arrow.png", UriKind.Relative));
            }
            else
            {
                BtnGoPrevious.Source = new BitmapImage(new Uri("/KojVenStatistic;component/Assets/left_arrow_disabled.png", UriKind.Relative));
            }

            if(_pagesCount == 0)
            {
                BtnGoNext.Source = new BitmapImage(new Uri("/KojVenStatistic;component/Assets/left_arrow_disabled.png", UriKind.Relative));
                BtnGoPrevious.Source = new BitmapImage(new Uri("/KojVenStatistic;component/Assets/left_arrow_disabled.png", UriKind.Relative));
            }
        }

        private void ChangePage()
        {
            var appeals = _client.Appeal.OrderBy(i => i.DateOfRequest).ToList();
            try
            {
                SPOptional.DataContext = appeals[_selectedPage];
                _currentAppeal = appeals[_selectedPage];
            }
            catch 
            {

            }
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            new Windows.CreateDoctorVisitSertificateWindow(_currentAppeal).ShowDialog();
        }
    }
}
