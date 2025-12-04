using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CourseWork.Pages
{
    public partial class RoomReservationsPage : Page
    {
        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { if (isLoading != value) { isLoading = value; } }
        }

        // Дата въезда и выезда

        public DateTime Today { get; set; }
        public DateTime? SelectedStartDate { get; set; }
        public DateTime? SelectedEndDate { get; set; }

        // Минимальные и максимальные количество взрослых и детей

        private const int MinAdults = 1;
        private const int MaxAdults = 6;
        private const int MinChildren = 0;
        private const int MaxChildren = 4;
        private int currentAdults;
        private int currentChildren;

        public RoomReservationsPage()
        {
            InitializeComponent();

            priceFromTextBox.TextChanged += PriceFromTextBox_TextChanged;
            priceUpToTextBox.TextChanged += PriceUpToTextBox_TextChanged;
            UpdatePlaceholderVisibility();

            LoadComboBoxData();
            AddUserControlToGrid();

            currentAdults = MinAdults;
            currentChildren = MinChildren;
            UpdateGuestCountDisplay();

            Today = DateTime.Today;
            DataContext = this;
            BookingDatePicker.DisplayDateStart = Today;
            EndDatePicker.DisplayDateStart = Today;

            DataContext = this;
            _ = LoadRoomsAsync();
        }

        private void AddUserControlToGrid()
        {

        }

        private bool IsNumeric(string text)
        {
            foreach (char c in text)
            {
                // Проверка, является ли символ цифрой
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void priceFrom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверка, является ли символ цифрой
            e.Handled = !IsNumeric(e.Text);
        }

        private void priceUpTo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void CloseProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TypeRoomsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadComboBoxData()
        {
            List<string> typeRoom = new List<string>
            {
                "Стандарт",
                "Полуюкс",
                "Люкс",
                "Представительский"
            };

            typeRoomsComboBox.ItemsSource = typeRoom;

            List<string> bed = new List<string>
            {
                "1 двухспальная",
                "2 односпальные",
            };

            bedsComboBox.ItemsSource = bed;
        }

        private void FilterHiddenButton_Click(object sender, RoutedEventArgs e)
        {
            if (filterGrid.Visibility == Visibility.Visible)
            {
                filterGrid.Visibility = Visibility.Collapsed;
                GuestCountPopup.IsOpen = false;
            }
            else
                filterGrid.Visibility = Visibility.Visible;
        }

        private void PriceFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void PriceUpToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void UpdatePlaceholderVisibility()
        {
            // Скрыть подсказку, если TextBox не пустой, иначе показать
            placeholderPriceFromTextBox.Visibility = string.IsNullOrWhiteSpace(priceFromTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
            placeholderPriceUpToTextBox.Visibility = string.IsNullOrWhiteSpace(priceUpToTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void BedsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Выбор даты окончания бронирования
        private void BookingDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingDatePicker.SelectedDate.HasValue)
            {
                // Обновляем минимальную допустимую дату для EndDatePicker
                EndDatePicker.DisplayDateStart = BookingDatePicker.SelectedDate.Value.AddDays(1);

                // Если выбранная дата окончания раньше новой даты начала, сбрасываем её
                if (SelectedEndDate <= BookingDatePicker.SelectedDate)
                {
                    SelectedEndDate = null;
                    EndDatePicker.SelectedDate = null;
                }
            }
        }

        //Popup для количества гостей
        private void GuestCountTextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Открываем Popup только при нажатии на TextBox
            GuestCountPopup.IsOpen = true;
            e.Handled = true;
        }

        private void UpdateGuestCountDisplay()
        {
            // Обновляем текст в TextBlock для взрослых и детей
            AdultCountTextBlock.Text = currentAdults.ToString();
            ChildCountTextBlock.Text = currentChildren.ToString();

            if (currentAdults == 1)
                GuestCountTextBox.Text = $"{currentAdults} взрослый";
            else
                GuestCountTextBox.Text = $"{currentAdults} взрослых";

            if (currentChildren == 1)
                GuestCountTextBox.Text += $", {currentChildren} ребенок";
            if (currentChildren > 1)
                GuestCountTextBox.Text += $", {currentChildren} ребенка";
            else
                GuestCountTextBox.Text += $"";
        }

        private void DecreaseAdultButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentAdults > MinAdults)
            {
                currentAdults--;
                UpdateGuestCountDisplay();
            }
        }

        private void IncreaseAdultButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentAdults < MaxAdults)
            {
                currentAdults++;
                UpdateGuestCountDisplay();
            }
        }

        private void DecreaseChildButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentChildren > MinChildren)
            {
                currentChildren--;
                UpdateGuestCountDisplay();
            }
        }

        private void IncreaseChildButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentChildren < MaxChildren)
            {
                currentChildren++;
                UpdateGuestCountDisplay();
            }
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем Popup при нажатии на кнопку "Закрыть"
            GuestCountPopup.IsOpen = false;
        }

        // Навигация

        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRoomPage());
        }

        private void UpdateRoomButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpdateRoomPage());
        }

        private void DeleteRoomButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteRoomPage());
        }

        // Добавление карточек комнат

        private async Task LoadRoomsAsync()
        {
            try
            {
                IsLoading = true;
                using var db = new MyAppContext();
                var rooms = await db.Rooms
                                    .AsNoTracking()
                                    .Include(r => r.RoomType)
                                    .Include(r => r.Images)
                                    .ToListAsync();
                Rooms.Clear();
                foreach (var r in rooms)
                    Rooms.Add(r);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int roomId)
            {
                MessageBox.Show($"Забронировать номер {roomId}", "Бронирование", MessageBoxButton.OK, MessageBoxImage.Information);
                // Здесь можно открыть диалог бронирования / навигацию и т.д.
            }
            else if (sender is Button b && b.Tag != null)
            {
                MessageBox.Show($"Tag: {b.Tag}");
            }
        }
    }
}

