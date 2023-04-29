using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ViewModel;

namespace Views.Windows
{
    public class MessageBusHelper
    {
        private Window _window;
        private MainViewModel _vm;
        public MessageBusHelper(Window window)
        {
            _window = window;
            _vm = (MainViewModel)_window.DataContext;
            window.Loaded += OnMessageSubscrib;
            window.Unloaded += OnMessageUnsubscrib;
        }
        private void OnMessageSubscrib(object sender, RoutedEventArgs e) => _vm.MessageBus += MessageShow;
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
        public void MessageShow(Info message)
        {
            _ = _window.Dispatcher.BeginInvoke(async () =>
            {
                (_window as MainWindow).MessageBox.Opacity = 1;
                (_window as MainWindow).MessageBox.BeginAnimation(UIElement.OpacityProperty, ZeroToOneAnimation);
                (_window as MainWindow).MessageBox.Text = message.Message;
                await Task.Delay(3000);
                (_window as MainWindow).MessageBox.BeginAnimation(UIElement.OpacityProperty, OneToZeroAnimation);
            });
        }
        public void ShowErrorDialog(Error message)
        {
            MessageBox.Show(message.error, "Список служащих");
        }
        private void OnMessageUnsubscrib(object sender, RoutedEventArgs e)
        {
            _vm.MessageBus -= MessageShow;
        }
    }
}
