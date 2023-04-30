using Model;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using VievModelLib;
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
        //private readonly DialogsService _dialogsService;
        private MessageHelper _messageHelper;
        public App()
        {
            MainModel _mainModel = new MainModel();
            //_dialogsService = new DialogsService();
            UsersSync.ValueChanged += OnUsersChanged;
            MainViewModel vm = new(_mainModel);
            UsersSync.SetValueBinding(new Binding(nameof(MainViewModel.Users)) { Source = vm });
            MainWindow _mainWindow = new MainWindow() { DataContext = vm };
            //_messageHelper = new MessageHelper(_mainWindow);
            _mainWindow.Show();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //_dialogsService.Register(_messageHelper.MessageShow);
            //_dialogsService.Register(_messageHelper.ShowErrorDialog);
            MessageDO.Register();
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
