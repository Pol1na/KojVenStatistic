using KojVenStatistic.Entity;
using KojVenStatistic.Windows;
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
    /// Interaction logic for CreateAppealPage.xaml
    /// </summary>
    public partial class CreateAppealPage : Page
    {
        private User _selectedDoctor;
        public CreateAppealPage(User selectedDoctor)
        {
            InitializeComponent();
            _selectedDoctor = selectedDoctor;
            TBlockHeader.DataContext = selectedDoctor;
            CBoxSnils.ItemsSource = AppData.Context.Client.ToList();
            CBoxTypeAppeal.ItemsSource = AppData.Context.AppealType.ToList();
            DPickerRequetsDate.DisplayDateStart = DateTime.Now;
            DPickerRequetsDate.DisplayDateEnd = DateTime.Now.AddDays(14);
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            if(new ClientEditorWindow().ShowDialog() == true)
            {
                CBoxSnils.ItemsSource = AppData.Context.Client.ToList();
            }
        }

        private void BtnCreateAppeal_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(CBoxSnils.Text))
                {
                    MessageBox.Show("Заполните поле СНИЛС.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Client client = AppData.Context.Client.ToList().FirstOrDefault(x => x.Snils == CBoxSnils.Text);
                if(client != null)
                {
                    if (!DPickerRequetsDate.SelectedDate.HasValue)
                    {
                        MessageBox.Show("Выберите дату приема.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if(CBoxTypeAppeal.SelectedItem is null)
                    {
                        MessageBox.Show("Выберите заболевание.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Appeal appeal = new Appeal();
                    appeal.Client = client;

                    appeal.AppealType = CBoxTypeAppeal.SelectedItem as AppealType;

                    var date = DPickerRequetsDate.SelectedDate.Value;
                    appeal.DateOfRequest = new DateTime(date.Year, date.Month, date.Day,
                        int.Parse(TBoxHour.Text), int.Parse(TBoxMinute.Text), 0);
                    appeal.User = _selectedDoctor;
                    if(new TicketPreviewWindow(appeal).ShowDialog() == true)
                    {
                        appeal = AppData.Context.Appeal.Add(appeal);
                        AppData.Context.SaveChanges();
                        NavigationService.GoBack();
                        MessageBox.Show("Запись успешно создана.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {

                    var result = MessageBox.Show("Клиента с данным СНИЛСом не существует. Хотите создать его?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if(result == MessageBoxResult.Yes)
                    {
                        if(new ClientEditorWindow(CBoxSnils.Text).ShowDialog() == true)
                        {
                            CBoxSnils.ItemsSource = AppData.Context.Client.ToList();
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Произошла ошибка проверьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }
    }
}
