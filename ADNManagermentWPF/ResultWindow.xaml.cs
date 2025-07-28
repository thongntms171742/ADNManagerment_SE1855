using BusinessObjects;
using Services;
using System;
using System.Windows;

namespace ADNManagermentWPF
{
    public partial class ResultWindow : Window
    {
        private readonly IResultService _resultService;
        private readonly Booking _booking;
        private readonly Result _existingResult;
        private readonly bool _isEditMode;

        public ResultWindow(Booking booking, Result existingResult = null)
        {
            InitializeComponent();
            _resultService = new ResultService();
            _booking = booking;
            _existingResult = existingResult;
            _isEditMode = existingResult != null;
            
            SetupWindow();
        }

        private void SetupWindow()
        {
            // Set booking information
            txtBookingId.Text = _booking.BookingId.ToString();
            txtCustomer.Text = _booking.Customer.FullName;
            txtService.Text = _booking.Service.ServiceName;

            if (_isEditMode)
            {
                txtTitle.Text = "Edit Test Result";
                dpResultDate.SelectedDate = _existingResult.ResultDate;
                txtResultDetails.Text = _existingResult.ResultDetails;
                
                // Set test status
                switch (_existingResult.TestStatus)
                {
                    case "Completed":
                        cboTestStatus.SelectedIndex = 0;
                        break;
                    case "In Progress":
                        cboTestStatus.SelectedIndex = 1;
                        break;
                    case "Failed":
                        cboTestStatus.SelectedIndex = 2;
                        break;
                }
            }
            else
            {
                txtTitle.Text = "Add Test Result";
                dpResultDate.SelectedDate = DateTime.Now;
                cboTestStatus.SelectedIndex = 0; // Default to Completed
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (dpResultDate.SelectedDate == null)
                {
                    MessageBox.Show("Please select a result date", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtResultDetails.Text))
                {
                    MessageBox.Show("Please enter result details", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cboTestStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select a test status", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var testStatus = ((System.Windows.Controls.ComboBoxItem)cboTestStatus.SelectedItem).Content.ToString();

                if (_isEditMode)
                {
                    // Update existing result
                    _existingResult.ResultDate = dpResultDate.SelectedDate.Value;
                    _existingResult.ResultDetails = txtResultDetails.Text.Trim();
                    _existingResult.TestStatus = testStatus;
                    _existingResult.StaffId = App.CurrentUser.UserId;
                    
                    _resultService.UpdateResult(_existingResult);
                    MessageBox.Show("Result updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Create new result
                    var newResult = new Result
                    {
                        BookingId = _booking.BookingId,
                        ResultDate = dpResultDate.SelectedDate.Value,
                        ResultDetails = txtResultDetails.Text.Trim(),
                        TestStatus = testStatus,
                        StaffId = App.CurrentUser.UserId
                    };
                    
                    _resultService.AddResult(newResult);
                    MessageBox.Show("Result added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving result: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 