using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class ResultManagementPage : Page
    {
        private readonly IResultService _resultService;
        private readonly IBookingService _bookingService;

        public ResultManagementPage()
        {
            InitializeComponent();
            _resultService = new ResultService();
            _bookingService = new BookingService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Load pending results (bookings without results)
                var allBookings = _bookingService.GetAllBookings();
                var bookingsWithResults = _resultService.GetAllResults().Select(r => r.BookingId).ToList();
                var pendingBookings = allBookings.Where(b => !bookingsWithResults.Contains(b.BookingId)).ToList();
                

                
                dgPendingResults.ItemsSource = pendingBookings;

                // Load all results
                var allResults = _resultService.GetAllResults();
                dgAllResults.ItemsSource = allResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtSearchBookingId.Text, out int bookingId))
                {
                    var booking = _bookingService.GetBookingById(bookingId);
                    if (booking != null)
                    {
                        dgPendingResults.ItemsSource = new List<Booking> { booking };
                    }
                    else
                    {
                        MessageBox.Show("Booking not found", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid booking ID", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddResult_Click(object sender, RoutedEventArgs e)
        {
            if (dgPendingResults.SelectedItem is Booking selectedBooking)
            {
                var resultWindow = new ResultWindow(selectedBooking);
                if (resultWindow.ShowDialog() == true)
                {
                    LoadData(); // Refresh the lists
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to add result", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnEditResult_Click(object sender, RoutedEventArgs e)
        {
            if (dgAllResults.SelectedItem is Result selectedResult)
            {
                var resultWindow = new ResultWindow(selectedResult.Booking, selectedResult);
                if (resultWindow.ShowDialog() == true)
                {
                    LoadData(); // Refresh the lists
                }
            }
            else
            {
                MessageBox.Show("Please select a result to edit", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnViewResult_Click(object sender, RoutedEventArgs e)
        {
            if (dgAllResults.SelectedItem is Result selectedResult)
            {
                try
                {
                    // Safely access navigation properties with null checks
                    var customerName = selectedResult.Booking?.Customer?.FullName ?? "N/A";
                    var serviceName = selectedResult.Booking?.Service?.ServiceName ?? "N/A";
                    var staffName = selectedResult.Staff?.FullName ?? "N/A";
                    
                    var details = $"Result Details:\n\n" +
                                 $"Result ID: {selectedResult.ResultId}\n" +
                                 $"Booking ID: {selectedResult.BookingId}\n" +
                                 $"Customer: {customerName}\n" +
                                 $"Service: {serviceName}\n" +
                                 $"Result Date: {selectedResult.ResultDate:dd/MM/yyyy}\n" +
                                 $"Staff: {staffName}\n" +
                                 $"Test Status: {selectedResult.TestStatus}\n\n" +
                                 $"Details:\n{selectedResult.ResultDetails}";
                    
                    MessageBox.Show(details, "Result Details", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying result details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a result to view details", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
} 