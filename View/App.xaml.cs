using Model;
using System.Windows;
using ViewModel;
using Views.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainModel _mainModel = new MainModel();
            MainViewModel vm = new(_mainModel);
            MainWindow _mainWindow = new MainWindow() { DataContext = vm };
            _mainWindow.Show();
        }
    }
}
