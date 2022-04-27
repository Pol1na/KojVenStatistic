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
    /// Interaction logic for AddNewMedicineForUserWindow.xaml
    /// </summary>
    public partial class AddNewMedicineForUserWindow : Window
    {
        private List<Medicament> _selectedMed => LViewMedicament.ItemsSource as List<Medicament>;
        private List<Medicament> _allMed => CBoxMedicament.ItemsSource as List<Medicament>;
        private List<MedicamentOfRecipe> _appeal;
        public AddNewMedicineForUserWindow(Appeal appeal)
        {
            InitializeComponent();
            CBoxMedicament.ItemsSource = AppData.Context.Medicament.ToList();
            //_appeal = client.Appeal.First(i => i.DateOfRequest == date).ToList();
            _appeal = appeal.Recipe.First(i => i.AppealId == appeal.Id).MedicamentOfRecipe.ToList();
            LViewMedicament.ItemsSource = _appeal;
        }

        private void BtnAddMedicament_Click(object sender, RoutedEventArgs e)
        {
            if (CBoxMedicament.SelectedItem is Medicament medicament)
            {
                // тут трабл с тем, что добавляется препарат в заявку => ее рецепт
                _appeal.Add(medicament);
                _selectedMed.Add(medicament);

                LViewMedicament.ItemsSource = _selectedMed.ToList();

                _allMed.Remove(medicament);
                CBoxMedicament.ItemsSource = _allMed.ToList();
            }
            else
            {
                MessageBox.Show("Выберите препарат из списка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemoveMedicament_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
