using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CourseWork.Pages
{

    public partial class AuthorizationPage : Page
    {
        bool correctLogin = false;
        bool correctPassword = false;

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void HomeNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomReservationsPage());
        }

        private void AuthorizationNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(loginTextBox.Text))
            {
                correctLogin = false;
                hintLoginTextBlock.Text = "Введите логин";
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
            else
            {
                correctPassword = true;
                hintPasswordTextBlock.Visibility = Visibility.Collapsed;
            }

            if (correctLogin && correctPassword)
                NavigationService.Navigate(new RoomReservationsPage());

        }

        private void RegistrationNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
