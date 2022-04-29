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
using System.Windows.Shapes;

namespace KojVenStatistic.Windows
{
    /// <summary>
    /// Interaction logic for AddMedicamentWindow.xaml
    /// </summary>
    public partial class AddMedicamentWindow : Window
    {
        private List<Disease> _selectedDiseases => LViewDisease.ItemsSource as List<Disease>;
        private List<Disease> _allDieseases => CBoxDisease.ItemsSource as List<Disease>;
        public AddMedicamentWindow()
        {
            InitializeComponent();
            CBoxDisease.ItemsSource = AppData.Context.Disease.ToList();
            LViewDisease.ItemsSource = new List<Disease>();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDiseases.Count>0)
            {
                try
                {
                    var medicament = new Medicament()
                    {
                        Name = TBoxName.Text,
                        Description = TBoxDescription.Text,
                        Disease = _selectedDiseases,
                    };
                    AppData.Context.Medicament.Add(medicament);
                    AppData.Context.SaveChanges();
                    DialogResult = true;
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Произошла ошибка проверьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите хотя бы одно заболенивание.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnAddDisease_Click(object sender, RoutedEventArgs e)
        {
            if(CBoxDisease.SelectedItem is Disease disease)
            {
                _selectedDiseases.Add(disease);
                LViewDisease.ItemsSource = _selectedDiseases.ToList();

                _allDieseases.Remove(disease);
                CBoxDisease.ItemsSource = _allDieseases.ToList();
            }
            else
            {
                MessageBox.Show("Выберите болезнь из списка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BtnRemoveDisease_Click(object sender, RoutedEventArgs e)
        {

            if ((sender as Button).DataContext is Disease disease)
            {
                _allDieseases.Add(disease);
                CBoxDisease.ItemsSource = _allDieseases.ToList();

                _selectedDiseases.Remove(disease);
                LViewDisease.ItemsSource = _selectedDiseases.ToList();
            }
            else
            {
                MessageBox.Show("Выберите болезнь из списка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
