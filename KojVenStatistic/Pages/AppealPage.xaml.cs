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
    /// Логика взаимодействия для AppealPage.xaml
    /// </summary>
    public partial class AppealPage : Page
    {
        public AppealPage()
        {
            InitializeComponent(); 
            List<Category> categories = AppData.Context.Category.ToList();
            categories.Insert(0, new Category { Name = "Все специальности" });
            CBoxCategories.ItemsSource = categories;
            CBoxCategories.SelectedIndex = 0;
        }

        private void UpdateList()
        {
            List<User> users = AppData.Context.User.ToList().Where(x => x.PostId == 2).ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                users = users.Where(x => x.FullName.ToLower().Contains(text) || x.PhoneNumber.ToLower().Contains(text)).ToList();

            if (CBoxCategories.SelectedIndex != 0)
                users = users.Where(x => x.Category == CBoxCategories.SelectedItem).ToList();

            LViewUsers.ItemsSource = users;
        }

        private void CBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
            if (string.IsNullOrEmpty(TBoxSearch.Text))
                TBlockPlaceholer.Visibility = Visibility.Visible;
            else
                TBlockPlaceholer.Visibility = Visibility.Hidden;
        }

        private void BtnCreateAppeal_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateAppealPage((sender as Button).DataContext as User));
        }
    }
}
