using BusinessObjects;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class FeedbackWindow : Window
    {
        private readonly IFeedbackService _feedbackService;
        private readonly Result _result;
        private readonly User _currentUser;

        public FeedbackWindow(Result result, User currentUser)
        {
            InitializeComponent();
            _feedbackService = new FeedbackService();
            _result = result;
            _currentUser = currentUser;
            SetupWindow();
        }

        private void SetupWindow()
        {
            // Display booking information
            txtBookingId.Text = _result.BookingId.ToString();
            txtService.Text = _result.Booking?.Service?.ServiceName ?? "N/A";
            txtResultDate.Text = _result.ResultDate.ToString("dd/MM/yyyy");
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtComment.Text))
                {
                    MessageBox.Show("Please enter a comment.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Get rating
                int rating = 5; // Default
                if (rb1.IsChecked == true) rating = 1;
                else if (rb2.IsChecked == true) rating = 2;
                else if (rb3.IsChecked == true) rating = 3;
                else if (rb4.IsChecked == true) rating = 4;
                else if (rb5.IsChecked == true) rating = 5;

                // Create feedback
                var feedback = new Feedback
                {
                    CustomerId = _currentUser.UserId,
                    BookingId = _result.BookingId,
                    Rating = rating,
                    Comment = txtComment.Text.Trim(),
                    FeedbackDate = DateTime.Now
                };

                // Save feedback
                _feedbackService.AddFeedback(feedback);

                MessageBox.Show("Thank you for your feedback!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting feedback: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 