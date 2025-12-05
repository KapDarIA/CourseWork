using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CourseWork.Pages
{
    public partial class UpdateRoomPage : Page
    {
        private ObservableCollection<Room> rooms;
        public int roomId;
        public UpdateRoomPage()
        {
            InitializeComponent();

            LoadRooms();
            BedType();
            RoomType();
        }

        private void HomeNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomReservationsPage());
        }

        private void RoomsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRoom = roomsDataGrid.SelectedItem as Room;

            if (selectedRoom != null)
            {
                roomTypesComboBox.Text = Convert.ToString(selectedRoom.RoomType.NameType);
                capacityTextBox.Text = Convert.ToString(selectedRoom.Capacity);
                priceTextBox.Text = Convert.ToString(selectedRoom.Price);
                descriptionTextBox.Text = Convert.ToString(selectedRoom.Description);
                isAvailableComboBox.Text = Convert.ToString(selectedRoom.IsAvailable);
                bedTypesComboBox.Text = Convert.ToString(selectedRoom.BedType.NameBed);
                foodIncludedComboBox.Text = IsAvailable(selectedRoom.FoodIncluded);
                miniBarIncludedComboBox.Text = IsAvailable(selectedRoom.MiniBarIncluded);
                riverViewComboBox.Text = IsAvailable(selectedRoom.RiverView);
                babyBedComboBox.Text = IsAvailable(selectedRoom.BabyBed);
                forDisabledComboBox.Text = IsAvailable(selectedRoom.ForDisabled);
                imageTextBox.Text = Convert.ToString(selectedRoom.Image);
                roomId = selectedRoom.RoomId;
            }
        }



        public string IsAvailable(bool availabe)
        {
            if (availabe == true)
                return "Доступно";
            return "Не доступно";
        }

        // Загрузка данных в DataGrid 

        public void LoadRooms()
        {
            using (var context = new MyAppContext())
            {
                var roomList = context.Rooms.Include(r => r.RoomType).Include(r => r.BedType).ToList();
                rooms = new ObservableCollection<Room>(roomList);
                roomsDataGrid.ItemsSource = rooms;
            }
        }

        public void BedType()
        {
            using (var context = new MyAppContext())
            {
                var bedTypes = context.BedTypes.ToList();
                bedTypesComboBox.ItemsSource = bedTypes;
                bedTypesComboBox.DisplayMemberPath = "NameBed";     // показывать название
                bedTypesComboBox.SelectedValuePath = "BedTypeId"; // возвращать id выбранного типа
            }
        }

        public void RoomType()
        {
            using (var context = new MyAppContext())
            {
                var roomTypes = context.RoomTypes.ToList();
                roomTypesComboBox.ItemsSource = roomTypes;
                roomTypesComboBox.DisplayMemberPath = "NameType";     // показывать название
                roomTypesComboBox.SelectedValuePath = "RoomTypeId"; // возвращать id выбранного типа
            }
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

        // Изменение данных в таблице

        private void UpdateRoomButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyAppContext())
            {
                if (string.IsNullOrEmpty(roomTypesComboBox.Text) || string.IsNullOrEmpty(capacityTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text)
                || string.IsNullOrEmpty(descriptionTextBox.Text) || string.IsNullOrEmpty(isAvailableComboBox.Text) || string.IsNullOrEmpty(bedTypesComboBox.Text)
                || string.IsNullOrEmpty(foodIncludedComboBox.Text) || string.IsNullOrEmpty(miniBarIncludedComboBox.Text) || string.IsNullOrEmpty(roomTypesComboBox.Text)
                || string.IsNullOrEmpty(babyBedComboBox.Text) || string.IsNullOrEmpty(forDisabledComboBox.Text))
                {
                    MessageBox.Show("Заполните данные!");
                }
                else
                {
                    if (string.IsNullOrEmpty(imageTextBox.Text))
                    {
                        var selectedRoom = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                        if (selectedRoom != null)
                        {
                            selectedRoom.RoomTypeId = (int)roomTypesComboBox.SelectedValue;
                            selectedRoom.Capacity = Convert.ToInt32(capacityTextBox.Text);
                            selectedRoom.Price = Convert.ToDecimal(priceTextBox.Text);
                            selectedRoom.Description = descriptionTextBox.Text;
                            selectedRoom.IsAvailable = isAvailableComboBox.Text;
                            selectedRoom.BedTypeId = (int)bedTypesComboBox.SelectedValue;
                            selectedRoom.FoodIncluded = IsAvailable(foodIncludedComboBox.Text);
                            selectedRoom.MiniBarIncluded = IsAvailable(miniBarIncludedComboBox.Text);
                            selectedRoom.RiverView = IsAvailable(riverViewComboBox.Text);
                            selectedRoom.BabyBed = IsAvailable(babyBedComboBox.Text);
                            selectedRoom.ForDisabled = IsAvailable(forDisabledComboBox.Text);

                            if (!string.IsNullOrEmpty(imageTextBox.Text))
                                selectedRoom.Image = imageTextBox.Text;

                            context.SaveChanges();
                            LoadRooms();
                        }
                    }
                }
            }
        }

        public bool IsAvailable(string availabe)
        {
            if (availabe == "Доступно")
                return true;
            return false;
        }
    }
}

