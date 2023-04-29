﻿using Model;
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
        private readonly DialogsService _dialogsService;
        private MessageBusHelper _messageBusHelper;
        public App()
        {

            MainModel _mainModel = new MainModel();
            _dialogsService = new DialogsService();
            UsersSync.ValueChanged += OnUsersChanged;
            MainViewModel vm = new(_mainModel, _dialogsService);
            UsersSync.SetValueBinding(new Binding(nameof(MainViewModel.Users)) { Source = vm });
            MainWindow _mainWindow = new MainWindow() { DataContext = vm };
            _messageBusHelper = new MessageBusHelper(_mainWindow);
            _mainWindow.Show();
        }
        //public delegate void Info<in T>(T obj);
        //public Action<Info>? MessageBus;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // _dialogsService.Register<Action<string>>(_messageBusHelper.MessageShow);
            _dialogsService.Register(_messageBusHelper.MessageShow);
            _dialogsService.Register(_messageBusHelper.ShowErrorDialog);
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
