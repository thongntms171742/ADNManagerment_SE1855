using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace ADNManagermentWPF
{
    public partial class BookingDetailsWindow : Window
    {
        private readonly Booking _booking;
        private readonly IBookingService _bookingService;
        private readonly ISampleService _sampleService;
        private readonly IResultService _resultService;

        public BookingDetailsWindow(Booking booking)
        {
            InitializeComponent();
            _booking = booking;
            _bookingService = new BookingService();
            _sampleService = new SampleService();
            _resultService = new ResultService();

            LoadBookingDetails();
        }

        private void LoadBookingDetails()
        {
            // Set booking details
            txtBookingId.Text = _booking.BookingId.ToString();
            txtService.Text = _booking.Service?.ServiceName ?? "N/A";
            txtCustomer.Text = _booking.Customer?.FullName ?? "N/A";
            txtBookingDate.Text = _booking.BookingDate.ToString("dd/MM/yyyy");
            txtStatus.Text = _booking.Status.ToString();
            txtCollectionMethod.Text = _booking.CollectionMethod.ToString();
            txtAddress.Text = _booking.ShippingAddress ?? "N/A";
            txtCreatedDate.Text = _booking.CreatedDate.ToString("dd/MM/yyyy HH:mm");

            // Load sample information if available
            var sample = _sampleService.GetSampleByBookingId(_booking.BookingId);
            if (sample != null)
            {
                samplePanel.Visibility = Visibility.Visible;
                txtSampleId.Text = sample.SampleId.ToString();
                txtSampleType.Text = sample.SampleType;
                txtCollectionDate.Text = sample.CollectionDate.ToString("dd/MM/yyyy");
                txtReceivedDate.Text = sample.ReceivedDate.ToString("dd/MM/yyyy");
                txtStorageLocation.Text = sample.StorageLocation;
            }
            else
            {
                samplePanel.Visibility = Visibility.Collapsed;
            }

            // Load result information if available
            var result = _resultService.GetResultByBookingId(_booking.BookingId);
            if (result != null)
            {
                resultPanel.Visibility = Visibility.Visible;
                txtResultId.Text = result.ResultId.ToString();
                txtResultDetails.Text = result.ResultDetails;
                txtResultDate.Text = result.ResultDate.ToString("dd/MM/yyyy");
                txtStaff.Text = result.Staff?.FullName ?? "N/A";
            }
            else
            {
                resultPanel.Visibility = Visibility.Collapsed;
            }

            // Show update status controls only for staff/admin
            if (App.CurrentUser != null && (App.CurrentUser.Role == UserRole.Staff || App.CurrentUser.Role == UserRole.Manager || App.CurrentUser.Role == UserRole.Admin))
            {
                updateStatusPanel.Visibility = Visibility.Visible;
                cboStatus.ItemsSource = System.Enum.GetValues(typeof(BookingStatus));
                cboStatus.SelectedItem = _booking.Status;
            }
            else
            {
                updateStatusPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (cboStatus.SelectedItem != null)
            {
                var newStatus = (BookingStatus)cboStatus.SelectedItem;
                _bookingService.UpdateBookingStatus(_booking.BookingId, newStatus);
                MessageBox.Show("Booking status updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Refresh booking details
                _booking.Status = newStatus;
                txtStatus.Text = newStatus.ToString();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 