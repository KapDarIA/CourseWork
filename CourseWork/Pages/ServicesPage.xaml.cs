using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CourseWork.Pages
{
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
        }

        private void HomeNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomReservationsPage());
        }
    }
}