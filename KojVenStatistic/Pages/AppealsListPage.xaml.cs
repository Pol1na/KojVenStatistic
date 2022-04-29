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
    /// Логика взаимодействия для AppealsListPage.xaml
    /// </summary>
    public partial class AppealsListPage : Page
    {
        private List<Appeal> _appeals = new List<Appeal>();
        public AppealsListPage()
        {
            InitializeComponent();
            /*
            CBoxSort.ItemsSource = new string[] { "Без сортировки", "По имени (возр.)", "По имени (убыв.)", "По дате обращения (возр.)", "По дате обращения (убыв.)" };
            CBoxSort.SelectedIndex = 0;*/
            UpdateList();
        }

        private void BtnMoreInfo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppealInfoPage((sender as Button).DataContext as Appeal));
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();

            if (!string.IsNullOrEmpty(TBoxSearch.Text))
                TBlockPlaceholer.Visibility = Visibility.Collapsed;
            else
                TBlockPlaceholer.Visibility = Visibility.Visible;
        }
        private void UpdateList()
        {
            List<Appeal> appeals = AppData.AuthUser.Appeal.ToList().OrderBy(x => x.DateOfRequest).ToList();

            if(ChBoxIsActive.IsChecked == true)
                appeals = appeals.Where(x => x.IsActive).ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                appeals = appeals.Where(x => x.Client.FullName.ToLower().Contains(text) || x.RequestDateText.ToLower().Contains(text) || x.AppealType.Name.ToLower().Contains(text)).ToList();

            _appeals = appeals;
            ListCollectionView collectionView = new ListCollectionView(appeals);
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("AppealDateText"));
            LViewAppels.ItemsSource = collectionView;
        }

        private void ChBoxIsActive_Click(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }
    }
}
