using System;
using System.Windows;
using BusinessObjects;
using Services;

namespace ADNManagermentWPF
{
    public partial class RegisterWindow : Window
    {
        private readonly IAuthenticationService _authService;

        public RegisterWindow()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(txtFullName.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) ||
                    string.IsNullOrEmpty(txtPassword.Password) ||
                    string.IsNullOrEmpty(txtConfirmPassword.Password))
                {
                    MessageBox.Show("Please fill in all required fields", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtPassword.Password != txtConfirmPassword.Password)
                {
                    MessageBox.Show("Passwords do not match", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Check if email already exists
                if (_authService.IsEmailExists(txtEmail.Text))
                {
                    MessageBox.Show("Email already exists. Please use a different email.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create new user
                var user = new User
                {
                    FullName = txtFullName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Password, // In production, use proper password hashing
                    PhoneNumber = txtPhone.Text,
                    Address = txtAddress.Text,
                    DateOfBirth = dpDateOfBirth.SelectedDate ?? DateTime.Now,
                    Role = UserRole.Customer, // Default role for new registrations
                    CreatedDate = DateTime.Now
                };

                // Register user
                _authService.Register(user);

                MessageBox.Show("Registration successful! You can now login.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Navigate to login window
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
} 