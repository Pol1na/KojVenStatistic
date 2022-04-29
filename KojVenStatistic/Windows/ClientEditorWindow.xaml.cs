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
        private Client _client = null;
        private bool _isCreating => _client == null;
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

        public ClientEditorWindow(Client client)
        {
            InitializeComponent();
            InitializeUI();
            TBoxSnils.Text = client.Snils;

            TBoxName.Text = client.FirstName;
            TBoxSurname.Text = client.LastName;


            TBoxPassportNumber.Text = client.PassportNumber;
            TBoxPassportSeria.Text = client.PassportSeria;
            TBoxAddress.Text = client.Address;

            DPickerDateOfBirth.SelectedDate = client.DateOfBirth;
            TBoxTelephone.Text = client.Telephone;
            TBoxMedPolis.Text = client.MedPolis;
            CBoxGender.Text = client.Gender.Name;
            BtnAddClient.Content = "Редактировать клиента";
            Title = "Редактирование клиента";
            TBoxSnils.IsEnabled = false;
            _client = client;
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
            if(_client == null)
            {
                if (AppData.Context.Client.ToList().FirstOrDefault(x => x.Snils == TBoxSnils.Text) != null) 
                    errors += "Клиент c данным СНИЛСом уже есть\n";
            }

            if(errors == "")
            {
                try
                {
                    if(_isCreating)
                        _client = new Client();

                    try
                    {
                        Convert.ToInt32(TBoxSnils.Text);
                    }
                    catch (Exception)
                    {
                        errors += "Введите корректный СНИЛС\n";
                    }
                    _client.LastName = TBoxSurname.Text;
                    _client.FirstName = TBoxName.Text;
                    _client.PassportSeria = TBoxPassportSeria.Text;
                    _client.PassportNumber = TBoxPassportNumber.Text;
                    _client.Address = TBoxAddress.Text;
                    _client.DateOfBirth = DPickerDateOfBirth.SelectedDate.Value;
                    _client.Telephone = TBoxTelephone.Text;
                    _client.MedPolis = TBoxMedPolis.Text;
                    _client.Gender = CBoxGender.SelectedItem as Gender;

                    if (_isCreating)
                        AppData.Context.Client.Add(_client);

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
