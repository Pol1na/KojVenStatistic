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
    /// Interaction logic for MedicamentsPage.xaml
    /// </summary>
    public partial class MedicamentsPage : Page
    {
        public MedicamentsPage()
        {
            InitializeComponent();
            LViewMedicaments.ItemsSource = AppData.Context.Medicament.ToList();
            CBoxSort.ItemsSource = new string[] { "Без сортировки", "По имени (возр.)", "По имени (убыв.)", "По описанию (возр.)", "По описанию (убыв.)" };
            CBoxSort.SelectedIndex = 0;
            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.AuthUser.PostId ==1)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данный препарат?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AppData.Context.Medicament.Remove((sender as Button).DataContext as Medicament);
                        AppData.Context.SaveChanges();
                        UpdateList();
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка, попробуйте позже.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("У вас нет прав на выполнение данной операции", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }


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
            List<Medicament> medicaments = AppData.Context.Medicament.ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                medicaments = medicaments.Where(x => x.Name.ToLower().Contains(text) || x.Description.ToLower().Contains(text)).ToList();
            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    medicaments = medicaments.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    medicaments = medicaments.OrderByDescending(x => x.Name).ToList();
                    break;
                case 3:
                    medicaments = medicaments.OrderBy(x => x.Description).ToList();
                    break;
                case 4:
                    medicaments = medicaments.OrderByDescending(x => x.Description).ToList();
                    break;
                default:
                    break;
            }
            LViewMedicaments.ItemsSource = medicaments;

        }

        private void BtnAddMedicament_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.AddMedicamentWindow();
            if (editor.ShowDialog() == true)
            {
                UpdateList();
            }
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
