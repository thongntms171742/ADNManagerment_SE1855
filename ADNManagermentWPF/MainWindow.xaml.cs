using System.Windows;
using BusinessObjects;

namespace ADNManagermentWPF
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            SetupUserInterface();
            // Navigate to Home page by default
            mainFrame.Navigate(new HomePage());
        }

        private void SetupUserInterface()
        {
            lblWelcome.Content = $"Welcome, {_currentUser.FullName}";

            // Show/hide menu items based on user role
            switch (_currentUser.Role)
            {
                case UserRole.Admin:
                    // Admin: Show all controls
                    btnBooking.Visibility = Visibility.Collapsed;
                    btnResults.Visibility = Visibility.Collapsed;
                    btnProfile.Visibility = Visibility.Visible;
                    btnUserManagement.Visibility = Visibility.Visible;
                    btnServiceManagement.Visibility = Visibility.Visible;
                    btnResultManagement.Visibility = Visibility.Visible;
                    btnDashboard.Visibility = Visibility.Visible;
                    break;
                case UserRole.Manager:
                    // Manager: Show management controls (no user management)
                    btnBooking.Visibility = Visibility.Collapsed;
                    btnResults.Visibility = Visibility.Collapsed;
                    btnProfile.Visibility = Visibility.Visible;
                    btnUserManagement.Visibility = Visibility.Collapsed;
                    btnServiceManagement.Visibility = Visibility.Visible;
                    btnResultManagement.Visibility = Visibility.Visible;
                    btnDashboard.Visibility = Visibility.Visible;
                    break;
                case UserRole.Staff:
                    // Staff: Show service and result management
                    btnBooking.Visibility = Visibility.Collapsed;
                    btnResults.Visibility = Visibility.Collapsed;
                    btnProfile.Visibility = Visibility.Visible;
                    btnUserManagement.Visibility = Visibility.Collapsed;
                    btnServiceManagement.Visibility = Visibility.Visible;
                    btnResultManagement.Visibility = Visibility.Visible;
                    btnDashboard.Visibility = Visibility.Collapsed;
                    break;
                case UserRole.Customer:
                    // Customer: Show booking and results
                    btnBooking.Visibility = Visibility.Visible;
                    btnResults.Visibility = Visibility.Visible;
                    btnProfile.Visibility = Visibility.Visible;
                    btnUserManagement.Visibility = Visibility.Collapsed;
                    btnServiceManagement.Visibility = Visibility.Collapsed;
                    btnResultManagement.Visibility = Visibility.Collapsed;
                    btnDashboard.Visibility = Visibility.Collapsed;
                    break;
                default:
                    // Guest or unknown role
                    btnBooking.Visibility = Visibility.Collapsed;
                    btnResults.Visibility = Visibility.Collapsed;
                    btnProfile.Visibility = Visibility.Collapsed;
                    btnUserManagement.Visibility = Visibility.Collapsed;
                    btnServiceManagement.Visibility = Visibility.Collapsed;
                    btnResultManagement.Visibility = Visibility.Collapsed;
                    btnDashboard.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new HomePage());
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            // Only customers can access booking
            if (_currentUser.Role == UserRole.Customer)
            {
                mainFrame.Navigate(new BookingPage());
            }
            else
            {
                MessageBox.Show("Access denied. Only customers can access booking functionality.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to services page
            mainFrame.Navigate(new ServicePage());
        }

        private void btnResults_Click(object sender, RoutedEventArgs e)
        {
            // Only customers can access results
            if (_currentUser.Role == UserRole.Customer)
            {
                mainFrame.Navigate(new ResultPage());
            }
            else
            {
                MessageBox.Show("Access denied. Only customers can access results functionality.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ProfilePage());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        // Menu item click handlers
        private void btnUserManagement_Click(object sender, RoutedEventArgs e)
        {
            // Only Admin can access user management
            if (_currentUser.Role == UserRole.Admin)
            {
                mainFrame.Navigate(new UserManagementPage());
            }
            else
            {
                MessageBox.Show("Access denied. Only Admin can access user management.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnServiceManagement_Click(object sender, RoutedEventArgs e)
        {
            // Staff, Manager, Admin can access service management
            if (_currentUser.Role == UserRole.Staff || _currentUser.Role == UserRole.Manager || _currentUser.Role == UserRole.Admin)
            {
                mainFrame.Navigate(new ServicePage());
            }
            else
            {
                MessageBox.Show("Access denied. Only Staff, Manager, and Admin can access service management.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnResultManagement_Click(object sender, RoutedEventArgs e)
        {
            // Check if user has permission to access result management
            if (_currentUser.Role == UserRole.Staff || _currentUser.Role == UserRole.Manager || _currentUser.Role == UserRole.Admin)
            {
                mainFrame.Navigate(new ResultManagementPage());
            }
            else
            {
                MessageBox.Show("Access denied. Only Staff, Manager, and Admin can access result management.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            // Manager and Admin can access dashboard
            if (_currentUser.Role == UserRole.Manager || _currentUser.Role == UserRole.Admin)
            {
                mainFrame.Navigate(new DashboardPage());
            }
            else
            {
                MessageBox.Show("Access denied. Only Manager and Admin can access dashboard.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
} 