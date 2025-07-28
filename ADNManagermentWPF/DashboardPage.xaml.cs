using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ADNManagermentWPF
{
    public partial class DashboardPage : Page
    {
        private readonly IBookingService _bookingService;
        private readonly IResultService _resultService;
        private readonly IServiceService _serviceService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public DashboardPage()
        {
            InitializeComponent();
            _bookingService = new BookingService();
            _resultService = new ResultService();
            _serviceService = new ServiceService();
            _userService = new UserService();
            _feedbackService = new FeedbackService();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Load statistics
                var allBookings = _bookingService.GetAllBookings();
                var allResults = _resultService.GetAllResults();
                var allServices = _serviceService.GetAllServices();
                var allUsers = _userService.GetAllUsers();

                // Update statistics cards
                txtTotalBookings.Text = allBookings.Count.ToString();
                txtCompletedTests.Text = allResults.Count.ToString();
                txtPendingResults.Text = (allBookings.Count - allResults.Count).ToString();
                
                // Calculate total revenue
                decimal totalRevenue = allBookings.Sum(b => b.Service?.Price ?? 0);
                txtTotalRevenue.Text = $"${totalRevenue:N2}";

                // Load service distribution
                LoadServiceDistribution(allBookings);

                // Load recent activities
                LoadRecentActivities(allBookings, allResults);

                // Load customer feedback
                LoadCustomerFeedback();

                // Draw simple chart
                DrawMonthlyChart(allBookings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadServiceDistribution(List<Booking> bookings)
        {
            serviceDistributionList.Items.Clear();
            
            var serviceGroups = bookings
                .GroupBy(b => b.Service?.ServiceName ?? "Unknown")
                .Select(g => new { Service = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            foreach (var group in serviceGroups)
            {
                var item = new ListViewItem();
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                
                stackPanel.Children.Add(new TextBlock 
                { 
                    Text = group.Service, 
                    Width = 150, 
                    VerticalAlignment = VerticalAlignment.Center 
                });
                
                stackPanel.Children.Add(new TextBlock 
                { 
                    Text = group.Count.ToString(), 
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Blue),
                    VerticalAlignment = VerticalAlignment.Center 
                });
                
                item.Content = stackPanel;
                serviceDistributionList.Items.Add(item);
            }
        }

        private void LoadRecentActivities(List<Booking> bookings, List<Result> results)
        {
            recentActivitiesList.Items.Clear();
            
            var activities = new List<string>();
            
            // Add recent bookings
            foreach (var booking in bookings.OrderByDescending(b => b.CreatedDate).Take(5))
            {
                activities.Add($"📅 New booking #{booking.BookingId} - {booking.Customer?.FullName} ({booking.CreatedDate:dd/MM/yyyy})");
            }
            
            // Add recent results
            foreach (var result in results.OrderByDescending(r => r.ResultDate).Take(5))
            {
                activities.Add($"✅ Result completed for booking #{result.BookingId} ({result.ResultDate:dd/MM/yyyy})");
            }

            foreach (var activity in activities.OrderByDescending(x => x).Take(10))
            {
                var item = new ListViewItem { Content = activity };
                recentActivitiesList.Items.Add(item);
            }
        }

        private void DrawMonthlyChart(List<Booking> bookings)
        {
            monthlyChart.Children.Clear();
            
            // Simple bar chart
            var monthlyData = bookings
                .GroupBy(b => b.CreatedDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .OrderBy(x => x.Month)
                .ToList();

            if (monthlyData.Count == 0) return;

            double maxCount = monthlyData.Max(x => x.Count);
            double chartWidth = monthlyChart.ActualWidth > 0 ? monthlyChart.ActualWidth : 400; // Default width
            double barWidth = chartWidth / monthlyData.Count;
            double maxHeight = 150;

            for (int i = 0; i < monthlyData.Count; i++)
            {
                var data = monthlyData[i];
                double barHeight = (data.Count / maxCount) * maxHeight;
                
                var bar = new Rectangle
                {
                    Width = barWidth - 10,
                    Height = barHeight,
                    Fill = new SolidColorBrush(Colors.Blue),
                    Margin = new Thickness(5, maxHeight - barHeight, 5, 0)
                };
                
                Canvas.SetLeft(bar, i * barWidth);
                Canvas.SetTop(bar, 0);
                
                monthlyChart.Children.Add(bar);
                
                // Add month label
                var label = new TextBlock
                {
                    Text = GetMonthName(data.Month),
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                Canvas.SetLeft(label, i * barWidth);
                Canvas.SetTop(label, maxHeight + 5);
                
                monthlyChart.Children.Add(label);
            }
        }

        private void LoadCustomerFeedback()
        {
            try
            {
                feedbackList.Items.Clear();
                
                var allFeedbacks = _feedbackService.GetAllFeedbacks();
                if (allFeedbacks == null || allFeedbacks.Count == 0)
                {
                    var item = new ListViewItem { Content = "No feedback available" };
                    feedbackList.Items.Add(item);
                    return;
                }

                foreach (var feedback in allFeedbacks.OrderByDescending(f => f.FeedbackDate).Take(10))
                {
                    var customerName = feedback.Customer?.FullName ?? "Unknown Customer";
                    var serviceName = feedback.Booking?.Service?.ServiceName ?? "Unknown Service";
                    var rating = new string('⭐', feedback.Rating);
                    var date = feedback.FeedbackDate.ToString("dd/MM/yyyy");
                    
                    var feedbackText = $"{rating} - {customerName} ({serviceName}) - {date}";
                    if (!string.IsNullOrEmpty(feedback.Comment))
                    {
                        feedbackText += $"\n\"{feedback.Comment}\"";
                    }
                    
                    var item = new ListViewItem { Content = feedbackText };
                    feedbackList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                var item = new ListViewItem { Content = $"Error loading feedback: {ex.Message}" };
                feedbackList.Items.Add(item);
            }
        }

        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "Jan", 2 => "Feb", 3 => "Mar", 4 => "Apr",
                5 => "May", 6 => "Jun", 7 => "Jul", 8 => "Aug",
                9 => "Sep", 10 => "Oct", 11 => "Nov", 12 => "Dec",
                _ => month.ToString()
            };
        }
    }
} 