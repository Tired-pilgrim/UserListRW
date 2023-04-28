using Model;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using ViewModel;
using Views;
using Views.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ValueProxy<ICollection> UsersSync = new ValueProxy<ICollection>();
        public App()
        {
            MainModel _mainModel = new MainModel();

            UsersSync.ValueChanged += OnUsersChanged;
            MainViewModel vm = new(_mainModel, ShowMessageDialog);
            UsersSync.SetValueBinding(new Binding(nameof(MainViewModel.Users)) { Source = vm });
            MainWindow _mainWindow = new MainWindow() { DataContext = vm };
            _ = new MessageBusHelper(_mainWindow);
            _mainWindow.Show();
        }
        private void ShowMessageDialog(string message)
        {
            MessageBox.Show(message, "Список служащих");
        }
        private void OnUsersChanged(object? sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ICollection old)
            {
                BindingOperations.DisableCollectionSynchronization(old);
            }
            if (e.NewValue is ICollection @new)
            {
                BindingOperations.EnableCollectionSynchronization(@new, @new.SyncRoot);
            }
        }
       
    }
}
