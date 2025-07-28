using System;
using System.Windows;
using BusinessObjects;

namespace ADNManagermentWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Store the current logged-in user
        public static User CurrentUser { get; set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Start with the login window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
} 