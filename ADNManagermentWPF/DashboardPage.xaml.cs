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
            
            // Set modern dark background
            monthlyChart.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
            
            // Get monthly booking data
            var monthlyData = bookings
                .GroupBy(b => b.CreatedDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .OrderBy(x => x.Month)
                .ToList();

            if (monthlyData.Count == 0) return;

            double maxCount = monthlyData.Max(x => x.Count);
            double chartWidth = monthlyChart.ActualWidth > 0 ? monthlyChart.ActualWidth : 400;
            double chartHeight = 180;
            double barWidth = (chartWidth - 80) / monthlyData.Count;
            double maxBarHeight = chartHeight - 100;

            // Draw background grid
            for (int i = 0; i <= 4; i++)
            {
                double yPosition = 30 + (i * maxBarHeight / 4);
                var gridLine = new Line
                {
                    X1 = 60,
                    Y1 = yPosition,
                    X2 = chartWidth - 20,
                    Y2 = yPosition,
                    Stroke = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                    StrokeThickness = 0.5
                };
                monthlyChart.Children.Add(gridLine);
            }

            // Draw Y-axis with gradient
            var yAxis = new Line
            {
                X1 = 60,
                Y1 = 30,
                X2 = 60,
                Y2 = chartHeight - 70,
                Stroke = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(0, 1),
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(0, 255, 255), 0),
                        new GradientStop(Color.FromRgb(0, 150, 255), 1)
                    }
                },
                StrokeThickness = 2
            };
            monthlyChart.Children.Add(yAxis);

            // Draw X-axis with gradient
            var xAxis = new Line
            {
                X1 = 60,
                Y1 = chartHeight - 70,
                X2 = chartWidth - 20,
                Y2 = chartHeight - 70,
                Stroke = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(0, 255, 255), 0),
                        new GradientStop(Color.FromRgb(0, 150, 255), 1)
                    }
                },
                StrokeThickness = 2
            };
            monthlyChart.Children.Add(xAxis);

            // Y-axis labels with modern styling
            for (int i = 0; i <= 4; i++)
            {
                double yValue = (maxCount / 4) * i;
                double yPosition = chartHeight - 70 - (i * maxBarHeight / 4);
                
                var yLabel = new TextBlock
                {
                    Text = yValue.ToString("0"),
                    FontSize = 10,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                    FontFamily = new FontFamily("Segoe UI")
                };
                
                Canvas.SetLeft(yLabel, 15);
                Canvas.SetTop(yLabel, yPosition - 8);
                monthlyChart.Children.Add(yLabel);
            }

            // Calculate and display average line
            if (maxCount > 0)
            {
                double average = monthlyData.Average(x => x.Count);
                double averageHeight = (average / maxCount) * maxBarHeight;
                
                var averageLine = new Line
                {
                    X1 = 60,
                    Y1 = chartHeight - 70 - averageHeight,
                    X2 = chartWidth - 20,
                    Y2 = chartHeight - 70 - averageHeight,
                    Stroke = new SolidColorBrush(Color.FromRgb(255, 193, 7)),
                    StrokeThickness = 1.5,
                    StrokeDashArray = new DoubleCollection { 5, 3 }
                };
                monthlyChart.Children.Add(averageLine);

                // Average label
                var averageLabel = new TextBlock
                {
                    Text = $"Avg: {average:F1}",
                    FontSize = 8,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 193, 7)),
                    FontWeight = FontWeights.SemiBold
                };
                Canvas.SetLeft(averageLabel, chartWidth - 70);
                Canvas.SetTop(averageLabel, chartHeight - 70 - averageHeight - 15);
                monthlyChart.Children.Add(averageLabel);
            }

            // Draw modern columns with effects
            for (int i = 0; i < monthlyData.Count; i++)
            {
                var data = monthlyData[i];
                double barHeight = maxCount > 0 ? (data.Count / maxCount) * maxBarHeight : 0;
                double xPosition = 70 + (i * barWidth);
                
                // Create modern column with gradient and glow
                var column = new Rectangle
                {
                    Width = barWidth - 6,
                    Height = barHeight,
                    Fill = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(0, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Color.FromRgb(0, 255, 255), 0),
                            new GradientStop(Color.FromRgb(0, 180, 255), 0.3),
                            new GradientStop(Color.FromRgb(0, 120, 255), 0.7),
                            new GradientStop(Color.FromRgb(0, 80, 200), 1)
                        }
                    },
                    Stroke = new SolidColorBrush(Color.FromRgb(0, 255, 255)),
                    StrokeThickness = 1,
                    RadiusX = 4,
                    RadiusY = 4
                };
                
                Canvas.SetLeft(column, xPosition);
                Canvas.SetTop(column, chartHeight - 70 - barHeight);
                monthlyChart.Children.Add(column);
                
                // Add shadow effect
                var shadow = new Rectangle
                {
                    Width = barWidth - 6,
                    Height = 3,
                    Fill = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0)),
                    RadiusX = 2,
                    RadiusY = 2
                };
                Canvas.SetLeft(shadow, xPosition);
                Canvas.SetTop(shadow, chartHeight - 67);
                monthlyChart.Children.Add(shadow);
                
                // Add value label with glow effect
                if (data.Count > 0)
                {
                    var valueLabel = new TextBlock
                    {
                        Text = data.Count.ToString(),
                        FontSize = 10,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontFamily = new FontFamily("Segoe UI")
                    };
                    
                    Canvas.SetLeft(valueLabel, xPosition);
                    Canvas.SetTop(valueLabel, chartHeight - 70 - barHeight - 18);
                    monthlyChart.Children.Add(valueLabel);
                }
                
                // Add month label with modern styling
                var monthLabel = new TextBlock
                {
                    Text = GetMonthName(data.Month),
                    FontSize = 9,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontFamily = new FontFamily("Segoe UI")
                };
                
                Canvas.SetLeft(monthLabel, xPosition);
                Canvas.SetTop(monthLabel, chartHeight - 50);
                monthlyChart.Children.Add(monthLabel);
            }

            // Add chart title with modern styling
            var title = new TextBlock
            {
                Text = "MONTHLY BOOKING TREND",
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 255)),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = new FontFamily("Segoe UI")
            };
            Canvas.SetLeft(title, 20);
            Canvas.SetTop(title, 8);
            monthlyChart.Children.Add(title);

            // Add subtitle
            var subtitle = new TextBlock
            {
                Text = $"Total: {bookings.Count} bookings",
                FontSize = 9,
                Foreground = new SolidColorBrush(Color.FromRgb(150, 150, 150)),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = new FontFamily("Segoe UI")
            };
            Canvas.SetLeft(subtitle, 20);
            Canvas.SetTop(subtitle, 25);
            monthlyChart.Children.Add(subtitle);
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