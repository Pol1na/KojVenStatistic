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

        private List<MedicamentOfRecipe> _currentMedicamentOfRecipes = new List<MedicamentOfRecipe>();
        private List<Medicament> allMedicaments => CBoxMedicament.ItemsSource as List<Medicament>;

        private Recipe _recipe = null;
        private Appeal _appeal = null;
        public AddNewMedicineForUserWindow(Appeal appeal)
        {
            InitializeComponent();
            _appeal = appeal;
            _recipe = appeal.Recipe;
            Title = appeal.CreateRecipeText;
            if (_recipe != null)
            {
                _currentMedicamentOfRecipes = _recipe.MedicamentOfRecipe.ToList();
                LViewMedicament.ItemsSource = _currentMedicamentOfRecipes;
            }

            CBoxMedicament.ItemsSource = AppData.Context.Medicament.ToList().Where(x => _currentMedicamentOfRecipes.FirstOrDefault(y => y.Medicament == x) == null).ToList();
        }

        private void BtnAddMedicament_Click(object sender, RoutedEventArgs e)
        {
            if (CBoxMedicament.SelectedItem is Medicament medicament)
            {
                if (int.TryParse(TBoxAmount.Text, out int amount) && amount > 1 && amount <= 20)
                {
                    _currentMedicamentOfRecipes.Add(new MedicamentOfRecipe() { Medicament = medicament, AmountPerDay = amount });

                    LViewMedicament.ItemsSource = _currentMedicamentOfRecipes.ToList();

                    allMedicaments.Remove(medicament);
                    var list = allMedicaments;
                    CBoxMedicament.ItemsSource = allMedicaments.ToList();
                    TBoxAmount.Text = "";
                }
                else
                {
                    MessageBox.Show("Поля заполнены некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Выберите препарат из списка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemoveMedicament_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is MedicamentOfRecipe medicament)
            {
                allMedicaments.Add(medicament.Medicament);

                CBoxMedicament.ItemsSource = allMedicaments.ToList();

                _currentMedicamentOfRecipes.Remove(medicament);

                LViewMedicament.ItemsSource = _currentMedicamentOfRecipes.ToList();
            }
            else
            {
                MessageBox.Show("Выберите препарат из списка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentMedicamentOfRecipes.Count > 0)
                {
                    if (_recipe == null)
                    {
                        _recipe = AppData.Context.Recipe.Add(new Recipe() { Appeal = _appeal, Date = DateTime.Now });
                    }
                    _recipe.MedicamentOfRecipe = null;

                    AppData.Context.SaveChanges();
                    _recipe.MedicamentOfRecipe = _currentMedicamentOfRecipes;

                    AppData.Context.SaveChanges();
                    Close();
                }
                else
                {
                    MessageBox.Show("Рецепт не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении рецепта в базу данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
