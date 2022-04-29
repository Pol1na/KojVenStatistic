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
    /// Interaction logic for RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        public RecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            DataContext = recipe;
            string med = "";
            foreach (var item in recipe.MedicamentOfRecipe)
            {
                med += $"{item.Medicament.Name}\n{item.AmountPerDay} раз(а) в день\n";
            }
            TBlockMedicaments.Text = med;
        }

        private void BtnRecipePrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(Recipe, "Рецепт");
            }
            try
            {
                DialogResult = true;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
