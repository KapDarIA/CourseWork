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
        public RoomReservationsPage()
        {
            InitializeComponent();

            priceFromTextBox.TextChanged += PriceFromTextBox_TextChanged;
            priceUpToTextBox.TextChanged += PriceUpToTextBox_TextChanged;
            UpdatePlaceholderVisibility();

            LoadComboBoxData();
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
    }
}
