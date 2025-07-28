using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace ADNManagermentWPF
{
    public partial class BookingPage : Page
    {
        private readonly IBookingService _bookingService;
        private readonly IServiceService _serviceService;

        public BookingPage()
        {
            InitializeComponent();
            _bookingService = new BookingService();
            _serviceService = new ServiceService();
            
            LoadData();
        }

        private void LoadData()
        {
            // Load services
            cboServices.ItemsSource = _serviceService.GetAllServices();
            cboServices.DisplayMemberPath = "ServiceName";
            cboServices.SelectedValuePath = "ServiceId";

            // Load collection methods
            cboCollectionMethod.ItemsSource = System.Enum.GetValues(typeof(CollectionMethod));

            // If user is a customer, load their bookings
            if (App.CurrentUser != null && App.CurrentUser.Role == UserRole.Customer)
            {
                dgBookings.ItemsSource = _bookingService.GetBookingsByCustomerId(App.CurrentUser.UserId);
            }
            else
            {
                // For staff, manager, admin - show all bookings
                dgBookings.ItemsSource = _bookingService.GetAllBookings();
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser == null)
            {
                MessageBox.Show("Please login first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cboServices.SelectedItem == null)
            {
                MessageBox.Show("Please select a service", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cboCollectionMethod.SelectedItem == null)
            {
                MessageBox.Show("Please select a collection method", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dpBookingDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a booking date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create new booking
            var booking = new Booking
            {
                CustomerId = App.CurrentUser.UserId,
                ServiceId = (int)cboServices.SelectedValue,
                BookingDate = dpBookingDate.SelectedDate.Value,
                Status = BookingStatus.Registered,
                CollectionMethod = (CollectionMethod)cboCollectionMethod.SelectedItem,
                ShippingAddress = txtAddress.Text,
                CreatedDate = System.DateTime.Now
            };

            _bookingService.AddBooking(booking);
            MessageBox.Show("Booking created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Refresh the grid
            LoadData();
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookings.SelectedItem is Booking selectedBooking)
            {
                // Show booking details in a new window
                BookingDetailsWindow detailsWindow = new BookingDetailsWindow(selectedBooking);
                detailsWindow.ShowDialog();

                // Refresh data after dialog closes
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a booking", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 