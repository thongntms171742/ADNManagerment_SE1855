using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class ResultPage : Page
    {
        private readonly IResultService _resultService;

        public ResultPage()
        {
            InitializeComponent();
            _resultService = new ResultService();
            SetupRoleBasedAccess();
            LoadCustomerResults();
        }

        private void SetupRoleBasedAccess()
        {
            // Only customers can access results
            if (App.CurrentUser.Role != UserRole.Customer)
            {
                MessageBox.Show("Access denied. Only customers can view test results.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Navigate back or show empty page
                dgResults.ItemsSource = new List<Result>();
                return;
            }
        }

        private void LoadCustomerResults()
        {
            try
            {
                // Only load results for the current customer
                if (App.CurrentUser.Role == UserRole.Customer)
                {
                    var results = _resultService.GetResultsByCustomerId(App.CurrentUser.UserId);
                    dgResults.ItemsSource = results;
                }
                else
                {
                    dgResults.ItemsSource = new List<Result>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading results: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.Role != UserRole.Customer)
            {
                MessageBox.Show("Access denied. Only customers can search results.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (int.TryParse(txtSearchBookingId.Text, out int bookingId))
            {
                try
                {
                    // Only search for results belonging to the current customer
                    var result = _resultService.GetResultByBookingId(bookingId);
                    if (result != null && result.Booking.CustomerId == App.CurrentUser.UserId)
                    {
                        dgResults.ItemsSource = new List<Result> { result };
                    }
                    else
                    {
                        MessageBox.Show("No result found for this booking ID or you don't have access to this result", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching result: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid booking ID", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.Role != UserRole.Customer)
            {
                MessageBox.Show("Access denied. Only customers can view results.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            txtSearchBookingId.Clear();
            LoadCustomerResults();
        }

        private void btnSendFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.Role != UserRole.Customer)
            {
                MessageBox.Show("Access denied. Only customers can send feedback.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dgResults.SelectedItem is Result selectedResult)
            {
                // Verify the result belongs to the current customer
                if (selectedResult.Booking.CustomerId == App.CurrentUser.UserId)
                {
                    var feedbackWindow = new FeedbackWindow(selectedResult, App.CurrentUser);
                    if (feedbackWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Feedback submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Access denied. You can only send feedback for your own results.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a result to send feedback", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.Role != UserRole.Customer)
            {
                MessageBox.Show("Access denied. Only customers can view result details.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dgResults.SelectedItem is Result selectedResult)
            {
                // Verify the result belongs to the current customer
                if (selectedResult.Booking.CustomerId == App.CurrentUser.UserId)
                {
                    MessageBox.Show($"Result Details for Result ID: {selectedResult.ResultId}\n\nDetails: {selectedResult.ResultDetails}", "Result Details", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Access denied. You can only view your own results.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a result to view details", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
} 