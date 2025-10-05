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

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoomReservationsPage.xaml
    /// </summary>
    public partial class RoomReservationsPage : Page
    {
        // Минимальные и максимальные количества взрослых и детей
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
        }
        private void AddUserControlToGrid()
        {
            BookingRoomUserControl userControl = new BookingRoomUserControl(); // Создаем экземпляр UserControl
            myGrid.Children.Add(userControl); // Добавляем его в контейнер, например, Grid
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
                filterGrid.Visibility = Visibility.Collapsed;
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

        //Popup
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
                GuestCountTextBox.Text += $", {currentChildren} детей";
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
    }
}

