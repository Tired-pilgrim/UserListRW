using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using VievModelLib;

namespace Views.Windows
{
    public class MessageBusHelper
    {
        private Window _window;
        public MessageBusHelper(Window window)
        {
            _window = window;
        }
        private readonly DoubleAnimation ZeroToOneAnimation = new DoubleAnimation()
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.5)
        };
        private readonly DoubleAnimation OneToZeroAnimation = new DoubleAnimation()
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.5)
        };
        public void MessageShow(Info message)
        {
            _ = _window.Dispatcher.BeginInvoke(async () =>
            {
                ((MainWindow)_window).MessageBox.Opacity = 1;
                ((MainWindow)_window).MessageBox.BeginAnimation(UIElement.OpacityProperty, ZeroToOneAnimation);
                ((MainWindow)_window).MessageBox.Text = message.message;
                await Task.Delay(3000);
                ((MainWindow)_window).MessageBox.BeginAnimation(UIElement.OpacityProperty, OneToZeroAnimation);
                
            });
        }
        public void ShowErrorDialog(Error message)
        {
            MessageBox.Show(message.error, "Список служащих");
        }
    }
}
