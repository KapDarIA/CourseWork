using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CourseWork.Pages
{
    public partial class AddRoomPage : Page
    {
        private ObservableCollection<Room> rooms;

        public AddRoomPage()
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

        public void AddRoomsButton_Click(object sender, RoutedEventArgs e)
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
                        var newRoom = new Room
                        {
                            RoomTypeId = (int)roomTypesComboBox.SelectedValue,
                            Capacity = Convert.ToInt32(capacityTextBox.Text),
                            Price = Convert.ToDecimal(capacityTextBox.Text),
                            Description = descriptionTextBox.Text,
                            IsAvailable = isAvailableComboBox.Text,
                            BedTypeId = (int)bedTypesComboBox.SelectedValue,
                            FoodIncluded = IsAvailable(foodIncludedComboBox.Text),
                            MiniBarIncluded = IsAvailable(miniBarIncludedComboBox.Text),
                            RiverView = IsAvailable(riverViewComboBox.Text),
                            BabyBed = IsAvailable(babyBedComboBox.Text),
                            ForDisabled = IsAvailable(forDisabledComboBox.Text),
                        };
                        context.Rooms.Add(newRoom);
                        context.SaveChanges();
                        LoadRooms();
                    }
                    else
                    {
                        var newRoom = new Room
                        {
                            RoomTypeId = (int)roomTypesComboBox.SelectedValue,
                            Capacity = Convert.ToInt32(capacityTextBox.Text),
                            Price = Convert.ToDecimal(capacityTextBox.Text),
                            Description = descriptionTextBox.Text,
                            IsAvailable = isAvailableComboBox.Text,
                            BedTypeId = (int)bedTypesComboBox.SelectedValue,
                            FoodIncluded = IsAvailable(foodIncludedComboBox.Text),
                            MiniBarIncluded = IsAvailable(miniBarIncludedComboBox.Text),
                            RiverView = IsAvailable(riverViewComboBox.Text),
                            BabyBed = IsAvailable(babyBedComboBox.Text),
                            ForDisabled = IsAvailable(forDisabledComboBox.Text),
                            Image = imageTextBox.Text,
                        };
                        context.Rooms.Add(newRoom);
                        context.SaveChanges();
                        LoadRooms();
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
