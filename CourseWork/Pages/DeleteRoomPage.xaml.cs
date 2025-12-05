using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CourseWork.Pages
{
    public partial class DeleteRoomPage : Page
    {
        public DeleteRoomPage()
        {
            InitializeComponent();
        }

        private void HomeNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomReservationsPage());
        }

        private void DeleteRoomButton_Click(object sender, RoutedEventArgs e)
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

        private void CapacityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }
    }
}
