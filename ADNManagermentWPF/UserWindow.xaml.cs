using BusinessObjects;
using Services;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class UserWindow : Window
    {
        private readonly IUserService _userService;
        private readonly User? _existingUser;
        private readonly bool _isEditMode;

        public UserWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            _existingUser = null;
            _isEditMode = false;
            SetupWindow();
        }

        public UserWindow(User user)
        {
            InitializeComponent();
            _userService = new UserService();
            _existingUser = user;
            _isEditMode = true;
            SetupWindow();
            LoadUserData();
        }

        private void SetupWindow()
        {
            txtHeader.Text = _isEditMode ? "Edit User" : "Add New User";
            cboRole.SelectedIndex = 0; // Default to Customer
        }

        private void LoadUserData()
        {
            if (_existingUser != null)
            {
                txtFullName.Text = _existingUser.FullName ?? "";
                txtEmail.Text = _existingUser.Email ?? "";
                txtPhoneNumber.Text = _existingUser.PhoneNumber ?? "";
                txtAddress.Text = _existingUser.Address ?? "";
                
                if (_existingUser.DateOfBirth.HasValue)
                {
                    dpDateOfBirth.SelectedDate = _existingUser.DateOfBirth.Value;
                }

                // Set role
                switch (_existingUser.Role)
                {
                    case UserRole.Customer:
                        cboRole.SelectedIndex = 0;
                        break;
                    case UserRole.Staff:
                        cboRole.SelectedIndex = 1;
                        break;
                    case UserRole.Manager:
                        cboRole.SelectedIndex = 2;
                        break;
                    case UserRole.Admin:
                        cboRole.SelectedIndex = 3;
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                var user = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Password,
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    DateOfBirth = dpDateOfBirth.SelectedDate,
                    CreatedDate = DateTime.Now
                };

                // Set role
                if (cboRole.SelectedItem is ComboBoxItem selectedItem)
                {
                    switch (selectedItem.Content.ToString())
                    {
                        case "Customer":
                            user.Role = UserRole.Customer;
                            break;
                        case "Staff":
                            user.Role = UserRole.Staff;
                            break;
                        case "Manager":
                            user.Role = UserRole.Manager;
                            break;
                        case "Admin":
                            user.Role = UserRole.Admin;
                            break;
                    }
                }

                if (_isEditMode && _existingUser != null)
                {
                    user.UserId = _existingUser.UserId;
                    user.CreatedDate = _existingUser.CreatedDate;
                    _userService.UpdateUser(user);
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _userService.AddUser(user);
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            // Validate Full Name
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFullName.Focus();
                return false;
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validate Password (only for new users)
            if (!_isEditMode && string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }

            // Validate Role
            if (cboRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cboRole.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 