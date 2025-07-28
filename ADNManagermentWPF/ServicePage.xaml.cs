using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ADNManagermentWPF
{
    public partial class ServicePage : Page
    {
        private readonly IServiceService _serviceService;

        public ServicePage()
        {
            InitializeComponent();
            _serviceService = new ServiceService();
            LoadServices();
        }

        private void LoadServices()
        {
            try
            {
                var services = _serviceService.GetAllServices();
                dgServices.ItemsSource = services;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading services: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceWindow = new ServiceWindow();
                if (serviceWindow.ShowDialog() == true)
                {
                    LoadServices(); // Refresh the list
                    MessageBox.Show("Service added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding service: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgServices.SelectedItem is Service selectedService)
                {
                    var serviceWindow = new ServiceWindow(selectedService);
                    if (serviceWindow.ShowDialog() == true)
                    {
                        LoadServices(); // Refresh the list
                        MessageBox.Show("Service updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a service to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing service: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgServices.SelectedItem is Service selectedService)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete service '{selectedService.ServiceName}'?\n\nThis will also delete all related bookings, results, and feedback.", 
                        "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        _serviceService.DeleteService(selectedService.ServiceId);
                        LoadServices(); // Refresh the list
                        MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a service to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting service: {ex.Message}\n\nPlease ensure the service has no active bookings or related data.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadServices();
        }
    }
} 