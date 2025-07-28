using BusinessObjects;
using Services;
using System;
using System.Windows;

namespace ADNManagermentWPF
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly IUserService _userService;
        private readonly User _currentUser;

        public ChangePasswordWindow(User currentUser)
        {
            InitializeComponent();
            _userService = new UserService();
            _currentUser = currentUser;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                // Verify current password
                if (_currentUser.Password != txtCurrentPassword.Password)
                {
                    MessageBox.Show("Current password is incorrect.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCurrentPassword.Focus();
                    return;
                }

                // Update password
                _currentUser.Password = txtNewPassword.Password;
                _userService.UpdateUser(_currentUser);

                // Update App.CurrentUser password
                if (App.CurrentUser != null)
                {
                    App.CurrentUser.Password = txtNewPassword.Password;
                }

                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            // Validate current password
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Password))
            {
                MessageBox.Show("Current password is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCurrentPassword.Focus();
                return false;
            }

            // Validate new password
            if (string.IsNullOrWhiteSpace(txtNewPassword.Password))
            {
                MessageBox.Show("New password is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPassword.Focus();
                return false;
            }

            if (txtNewPassword.Password.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPassword.Focus();
                return false;
            }

            // Validate confirm password
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Password))
            {
                MessageBox.Show("Please confirm your new password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            if (txtNewPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            // Check if new password is same as current password
            if (txtCurrentPassword.Password == txtNewPassword.Password)
            {
                MessageBox.Show("New password must be different from current password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNewPassword.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 