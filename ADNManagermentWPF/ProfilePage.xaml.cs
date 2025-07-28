using BusinessObjects;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class ProfilePage : Page
    {
        private readonly IUserService _userService;
        private User _currentUser;

        public ProfilePage()
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            if (App.CurrentUser != null)
            {
                _currentUser = App.CurrentUser;
                txtFullName.Text = _currentUser.FullName;
                txtEmail.Text = _currentUser.Email;
                txtPhone.Text = _currentUser.PhoneNumber ?? "";
                txtAddress.Text = _currentUser.Address ?? "";
                dpDateOfBirth.SelectedDate = _currentUser.DateOfBirth;
                txtRole.Text = _currentUser.Role.ToString();
                txtCreatedDate.Text = _currentUser.CreatedDate.ToString("dd/MM/yyyy");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.CurrentUser != null)
                {
                    // Update user information
                    App.CurrentUser.FullName = txtFullName.Text;
                    App.CurrentUser.PhoneNumber = txtPhone.Text;
                    App.CurrentUser.Address = txtAddress.Text;
                    App.CurrentUser.DateOfBirth = dpDateOfBirth.SelectedDate ?? DateTime.Now;

                    _userService.UpdateUser(App.CurrentUser);
                    MessageBox.Show("Profile updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.CurrentUser != null)
                {
                    var changePasswordWindow = new ChangePasswordWindow(App.CurrentUser);
                    changePasswordWindow.Owner = Window.GetWindow(this);
                    changePasswordWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("User information is not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening change password window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 