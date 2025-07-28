using BusinessObjects;
using Services;
using System;
using System.Windows;

namespace ADNManagermentWPF
{
    public partial class ServiceWindow : Window
    {
        private readonly IServiceService _serviceService;
        private readonly Service _service;
        private readonly bool _isEditMode;

        public ServiceWindow(Service service = null)
        {
            InitializeComponent();
            _serviceService = new ServiceService();
            _service = service;
            _isEditMode = service != null;
            
            SetupWindow();
        }

        private void SetupWindow()
        {
            if (_isEditMode)
            {
                txtTitle.Text = "Edit Service";
                txtServiceName.Text = _service.ServiceName;
                txtDescription.Text = _service.Description;
                txtPrice.Text = _service.Price.ToString();
            }
            else
            {
                txtTitle.Text = "Add New Service";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtServiceName.Text))
                {
                    MessageBox.Show("Please enter service name", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    MessageBox.Show("Please enter service description", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Please enter a valid price", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_isEditMode)
                {
                    // Update existing service
                    _service.ServiceName = txtServiceName.Text.Trim();
                    _service.Description = txtDescription.Text.Trim();
                    _service.Price = price;
                    _service.LastUpdatedDate = DateTime.Now;
                    
                    _serviceService.UpdateService(_service);
                    MessageBox.Show("Service updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Create new service
                    var newService = new Service
                    {
                        ServiceName = txtServiceName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        Price = price,
                        CreatedDate = DateTime.Now
                    };
                    
                    _serviceService.AddService(newService);
                    MessageBox.Show("Service added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving service: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 