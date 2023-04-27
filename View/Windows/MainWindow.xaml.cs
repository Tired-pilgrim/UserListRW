using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using ViewModel;

namespace Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnMessageSubscrib;
            Unloaded += OnMessageUnsubscrib;
        }
        private void OnMessageSubscrib(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = (MainViewModel)DataContext;
            vm.MessageBus += MessageShow;
        }
        private readonly DoubleAnimation ZeroToOneAnimation = new DoubleAnimation()
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(1)
        };
        private readonly DoubleAnimation OneToZeroAnimation = new DoubleAnimation()
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromSeconds(1)
        };       
        private void MessageShow(string message)
        {
            _ = Dispatcher.BeginInvoke(async () =>
            {
                MessageBox.Opacity = 1;
                MessageBox.BeginAnimation(OpacityProperty, ZeroToOneAnimation);
                MessageBox.Text = message;
                await Task.Delay(3000);
                MessageBox.BeginAnimation(OpacityProperty, OneToZeroAnimation);
            });
        }

        private void OnMessageUnsubscrib(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = (MainViewModel)DataContext;
            vm.MessageBus -= MessageShow;
        }
    }
}
