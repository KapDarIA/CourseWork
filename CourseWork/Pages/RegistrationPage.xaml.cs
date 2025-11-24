using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CourseWork.Pages
{
    public partial class RegistrationPage : Page
    {
        bool correctLogin = false;
        bool correctPassword = false;
        bool correctRepeatPassword = false;
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegistrationNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(loginTextBox.Text))
            {
                correctLogin = false;
                hintLoginTextBlock.Text = "Введите логин";
                hintLoginTextBlock.Visibility = Visibility.Visible;
            }
            else if (loginTextBox.Text.Length < 6)
            {
                correctLogin = false;
                hintLoginTextBlock.Text = "Логин не менее 6 символов";
                hintLoginTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                correctLogin = true;
                hintLoginTextBlock.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Password))
            {
                correctPassword = false;
                hintPasswordTextBlock.Text = "Введите пароль";
                hintPasswordTextBlock.Visibility = Visibility.Visible;
            }
            else if (passwordTextBox.Password.Length < 6)
            {
                correctPassword = false;
                hintPasswordTextBlock.Text = "Пароль не менее 6 символов";
                hintPasswordTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                correctPassword = true;
                hintPasswordTextBlock.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Password))
            {
                correctRepeatPassword = false;
                hintRepeatPasswordTextBlock.Text = "Введите пароль";
                hintRepeatPasswordTextBlock.Visibility = Visibility.Visible;
            }
            else if (passwordTextBox.Password != repeatPasswordTextBox.Password)
            {
                correctRepeatPassword = false;
                hintRepeatPasswordTextBlock.Text = "Пароли не совпадают";
                hintRepeatPasswordTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                correctRepeatPassword = true;
                hintRepeatPasswordTextBlock.Visibility = Visibility.Collapsed;
            }

            if (correctLogin && correctPassword && correctRepeatPassword)
                NavigationService.Navigate(new RoomReservationsPage());
        }

        private void AuthorizationNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void HomeNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomReservationsPage());
        }
    }
}
