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
    /// Interaction logic for ClientEditorWindow.xaml
    /// </summary>
    public partial class ClientEditorWindow : Window
    {
        public ClientEditorWindow()
        {
            InitializeComponent();
            InitializeUI();
        }
        public ClientEditorWindow(string snils)
        {
            InitializeComponent();
            InitializeUI();
            TBoxSnils.Text = snils.Substring(0, 14);
        }

        public void InitializeUI()
        {
            CBoxGender.ItemsSource = AppData.Context.Gender.ToList();
            CBoxGender.SelectedIndex = 2;
            DPickerDateOfBirth.DisplayDateEnd = DateTime.Now;
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TBoxSnils.Text)) errors += "Заполните поле СНИЛС\n";
            if (string.IsNullOrWhiteSpace(TBoxSurname.Text)) errors += "Заполните поле Фамилия\n";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) errors += "Заполните поле Имя\n";
            if (string.IsNullOrWhiteSpace(TBoxPassportSeria.Text)) errors += "Заполните поле Серия паспорта\n";
            if (string.IsNullOrWhiteSpace(TBoxPassportNumber.Text)) errors += "Заполните поле Номер пасспорта\n";
            if (string.IsNullOrWhiteSpace(TBoxAddress.Text)) errors += "Заполните поле Адрес\n";
            if (DPickerDateOfBirth.SelectedDate == null) errors += "Выберите дату рождения\n";
            if (string.IsNullOrWhiteSpace(TBoxTelephone.Text)) errors += "Заполните поле Номер телефона\n";
            if (string.IsNullOrWhiteSpace(TBoxMedPolis.Text)) errors += "Заполните поле Номер полиса\n";
            if (AppData.Context.Client.ToList().FirstOrDefault(x => x.Snils == TBoxSnils.Text) != null) 
                errors += "Клиент c данным СНИЛСом уже есть\n";

            if(errors == "")
            {
                try
                {
                    Client client = new Client();
                    client.Snils = TBoxSnils.Text;
                    client.LastName = TBoxSurname.Text;
                    client.FirstName = TBoxName.Text;
                    client.PassportSeria = TBoxPassportSeria.Text;
                    client.PassportNumber = TBoxPassportNumber.Text;
                    client.Address = TBoxAddress.Text;
                    client.DateOfBirth = DPickerDateOfBirth.SelectedDate.Value;
                    client.Telephone = TBoxTelephone.Text;
                    client.MedPolis = TBoxMedPolis.Text;
                    client.Gender = CBoxGender.SelectedItem as Gender;

                    AppData.Context.Client.Add(client);
                    AppData.Context.SaveChanges();
                    DialogResult = true;
                    Close();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка проверьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
            else
            {
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
