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
        private int minutes => int.Parse(TBoxMinute.Text);
        private int hours => int.Parse(TBoxHour.Text);

        private bool isGoodRequestDate = true;
        public CreateAppealPage(User selectedDoctor)
        {
            InitializeComponent();
            _selectedDoctor = selectedDoctor;
            TBlockHeader.DataContext = selectedDoctor;
            CBoxSnils.ItemsSource = AppData.Context.Client.ToList();
            DGridAppeals.ItemsSource = AppData.Context.Appeal.Where(p => p.User.Id == selectedDoctor.Id && p.DateOfRequest >= DateTime.Now).OrderBy(p=>p.DateOfRequest).ToList();
            CBoxTypeAppeal.ItemsSource = AppData.Context.AppealType.ToList();

            DPickerRequetsDate.DisplayDateStart = DateTime.Now;
            DPickerRequetsDate.DisplayDateEnd = DateTime.Now.AddDays(14);

            TBoxHour.Text = 7.ToString("00");
            TBoxMinute.Text = 0.ToString("00");
            DPickerRequetsDate.SelectedDate = DateTime.Now;

            while (!isGoodRequestDate)
            {
                AddInterval();

                if ((DPickerRequetsDate.SelectedDate.Value.Date == DPickerRequetsDate.DisplayDateEnd.Value.Date.AddDays(1) && $"{hours}:{minutes}" == "7:0"))
                {
                    TBlockError.Text = "У данного врача нет свободных записей на две недели.";
                    SPanelDateTime.Visibility = Visibility.Collapsed;
                    break;
                }
            }
            
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
                    if (!isGoodRequestDate)
                    {
                        MessageBox.Show("Данное время приема уже занято.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void BtnAddInterval_Click(object sender, RoutedEventArgs e)
        {
            AddInterval();
        }
        private void BtnRemoveInterval_Click(object sender, RoutedEventArgs e)
        {
            RemoveInterval();
        }
        private void DPickerRequetsDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateDateTime();
        }
        private void UpdateArrows()
        {
            BtnAddInterval.IsEnabled = !(DPickerRequetsDate.SelectedDate.Value.Date == DPickerRequetsDate.DisplayDateEnd.Value.Date && $"{hours}:{minutes}" == "17:45");
            BtnRemoveInterval.IsEnabled = !(DPickerRequetsDate.SelectedDate.Value.Date == DPickerRequetsDate.DisplayDateStart.Value.Date && $"{hours}:{minutes}" == "7:0");
        }
        private void RemoveInterval()
        {
            if (DPickerRequetsDate.SelectedDate.HasValue)
            {
                TBoxMinute.Text = (minutes - 15).ToString("00");

                if (minutes == -15)
                {
                    TBoxHour.Text = (hours - 1).ToString("00");
                    TBoxMinute.Text = 45.ToString("00");
                    if (hours == 6)
                    {
                        TBoxHour.Text = 17.ToString("00");
                        DPickerRequetsDate.SelectedDate = DPickerRequetsDate.SelectedDate.Value.AddDays(-1);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Выберите дату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ValidateDateTime();
        }
        private void AddInterval()
        {
            if (DPickerRequetsDate.SelectedDate.HasValue)
            {
                TBoxMinute.Text = (minutes + 15).ToString("00");

                if (minutes == 60)
                {
                    TBoxHour.Text = (hours + 1).ToString("00");
                    TBoxMinute.Text = 0.ToString("00");

                    if (hours == 18)
                    {
                        TBoxHour.Text = 7.ToString("00");
                        DPickerRequetsDate.SelectedDate = DPickerRequetsDate.SelectedDate.Value.AddDays(1);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Выберите дату!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ValidateDateTime();

        }
        private void ValidateDateTime()
        {
            if (DPickerRequetsDate.SelectedDate.HasValue)
            {
                var date = DPickerRequetsDate.SelectedDate.Value;
                isGoodRequestDate = _selectedDoctor.Appeal.ToList().FirstOrDefault(x => new DateTime(date.Year, date.Month, date.Day,
                            int.Parse(TBoxHour.Text), int.Parse(TBoxMinute.Text), 0) == x.DateOfRequest) == null;
                if (isGoodRequestDate)
                {
                    TBlockError.Visibility = Visibility.Hidden;
                }
                else
                {
                    TBlockError.Visibility = Visibility.Visible;
                }
                UpdateArrows();
            }
            
        }

    }
}
