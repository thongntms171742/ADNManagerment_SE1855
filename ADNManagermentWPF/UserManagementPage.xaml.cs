using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class UserManagementPage : Page
    {
        private readonly IUserService? _userService;

        public UserManagementPage()
        {
            InitializeComponent();
            try
            {
                _userService = new UserService();
            }
            catch (Exception)
            {
                _userService = null;
            }
            
            // Load data after XAML is fully loaded
            this.Loaded += UserManagementPage_Loaded;
        }

        private void UserManagementPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                if (dgUsers == null)
                {
                    return;
                }

                if (_userService == null)
                {
                    MessageBox.Show("User service is not available. Please check database connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var allUsers = _userService.GetAllUsers();
                if (allUsers == null || allUsers.Count == 0)
                {
                    MessageBox.Show("No users found in database.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgUsers.ItemsSource = new List<User>();
                    return;
                }

                // Filter out Admin users
                var filteredUsers = allUsers.Where(u => u.Role != UserRole.Admin).ToList();
                dgUsers.ItemsSource = filteredUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dgUsers.ItemsSource = new List<User>();
            }
        }

        private void ApplyFilters()
        {
            try
            {
                if (dgUsers == null)
                {
                    return;
                }

                if (_userService == null)
                {
                    MessageBox.Show("User service is not available. Please check database connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var allUsers = _userService.GetAllUsers();
                if (allUsers == null || allUsers.Count == 0)
                {
                    MessageBox.Show("No users found in database.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgUsers.ItemsSource = new List<User>();
                    return;
                }

                var filteredUsers = allUsers.Where(u => u.Role != UserRole.Admin).ToList();

                // Apply role filter
                if (cboRoleFilter?.SelectedItem is ComboBoxItem selectedItem)
                {
                    var selectedContent = selectedItem.Content?.ToString();
                    switch (selectedContent)
                    {
                        case "Customers Only":
                            filteredUsers = filteredUsers.Where(u => u.Role == UserRole.Customer).ToList();
                            break;
                        case "Staff Only":
                            filteredUsers = filteredUsers.Where(u => u.Role == UserRole.Staff).ToList();
                            break;
                        case "Managers Only":
                            filteredUsers = filteredUsers.Where(u => u.Role == UserRole.Manager).ToList();
                            break;
                        case "All Users":
                        default:
                            break;
                    }
                }

                // Apply name search
                if (!string.IsNullOrWhiteSpace(txtSearchName?.Text))
                {
                    filteredUsers = filteredUsers.Where(u => 
                        u.FullName?.Contains(txtSearchName.Text, StringComparison.OrdinalIgnoreCase) == true).ToList();
                }

                dgUsers.ItemsSource = filteredUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dgUsers.ItemsSource = new List<User>();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearchName.Clear();
            cboRoleFilter.SelectedIndex = 0; // Reset to "All Users"
            LoadUsers();
        }

        private void cboRoleFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userWindow = new UserWindow();
                if (userWindow.ShowDialog() == true)
                {
                    LoadUsers(); // Refresh the list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selectedUser)
            {
                try
                {
                    var userWindow = new UserWindow(selectedUser);
                    if (userWindow.ShowDialog() == true)
                    {
                        LoadUsers(); // Refresh the list
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to edit", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show($"Are you sure you want to delete user '{selectedUser.FullName}'?\n\nThis will also delete all related bookings, results, and feedback.", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (_userService != null)
                        {
                            _userService.DeleteUser(selectedUser.UserId);
                            LoadUsers(); // Refresh the list
                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("User service is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting user: {ex.Message}\n\nPlease ensure the user has no active bookings or related data.", 
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
} 