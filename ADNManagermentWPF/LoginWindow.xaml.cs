using System.Windows;
using Services;
using BusinessObjects;
using System;

namespace ADNManagermentWPF
{
    public partial class LoginWindow : Window
    {
        private readonly IAuthenticationService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                string password = txtPassword.Password;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both email and password", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = _authService.Login(email, password);

                if (user != null)
                {
                    // Store the current user in the application
                    App.CurrentUser = user;
                    
                    try
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating main window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please check your credentials.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegisterWindow registerWindow = new RegisterWindow();
                registerWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening register window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
} 