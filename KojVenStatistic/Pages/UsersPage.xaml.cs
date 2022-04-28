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
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private User user => DGUsers.SelectedItem as User;

        public UsersPage()
        {
            InitializeComponent();
            List<Category> categories = AppData.Context.Category.ToList();
            categories.Insert(0, new Category { Name = "Все специальности" });
            CBoxCategories.ItemsSource = categories;
            CBoxCategories.SelectedIndex = 0;
            if (AppData.AuthUser.PostId == 1)
            {
                BtnAdd.Visibility = Visibility.Visible;
                BtnEdit.Visibility = Visibility.Visible;
                BtnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAdd.Visibility = Visibility.Collapsed;
                BtnEdit.Visibility = Visibility.Collapsed;
                BtnDelete.Visibility = Visibility.Collapsed;
            }
        }

        private void TBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            List<User> users = AppData.Context.User.ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                users = users.Where(x => x.FirstName.ToLower().Contains(text) || x.LastName.ToLower().Contains(text) || x.Email.ToLower().Contains(text) || x.Login.ToLower().Contains(text)).ToList();
            if(CBoxCategories.SelectedIndex != 0)
                users = users.Where(x => x.Category == CBoxCategories.SelectedItem).ToList();

            DGUsers.ItemsSource = users;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
            if (string.IsNullOrEmpty(TBoxSearch.Text))
            {
                TBlockPlaceholer.Visibility = Visibility.Visible;
            }
            else
            {
                TBlockPlaceholer.Visibility = Visibility.Hidden;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.AddEditUserWindow(null);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.AddEditUserWindow(user);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данного пользователя?",
     "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppData.Context.User.Remove(user);
                AppData.Context.SaveChanges();
                UpdateGrid();
            }
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = DGUsers.SelectedItem != null;
            BtnDelete.IsEnabled = value;
            BtnEdit.IsEnabled = value;
        }
    }
}
