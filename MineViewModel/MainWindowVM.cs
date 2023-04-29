﻿using Model;
using ModelLib;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VievModelLib;
using ViewModelLib.Commands;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase, ISaveOpen
    {        
        public AddUserVM AddUserVM { get; }
        private readonly MainModel mineModel;       
        public RelayCommand RemoveUserCommand { get; }
        public RelayCommand ClearUserCommand { get; }
        public ReadOnlyObservableCollection<User>? Users => mineModel.Users;
        //public  Action<Info> ActInfo;
        private IDialogsService _dialogsService;
        public MainViewModel(MainModel mineModel, IDialogsService dialogsService)
        {
            AddUserVM = new(mineModel);
            this.mineModel = mineModel;            
            _dialogsService = dialogsService;
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
            mineModel.Message += MineModel_Message;
            ClearUserCommand = new RelayCommand(() => mineModel.ClearUzer(), () => Users?.Count > 0);
            //object lockitems = new object();
            //BindingOperations.EnableCollectionSynchronization(Users, lockitems);
        }

        private void MineModel_Message(object? sender, string e)
        {
            if (e == "Открыт новый список")
            {
                _dialogsService?.Get<Action<Info>>()?.Invoke(new Info(e));                
            }
        }

        public async Task OpenListUserAsync(string path) => await Task.Run(() =>
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                mineModel.OpenList(path);
            }
            else
            {
                _dialogsService.Get<Action<Error>>()?.Invoke(new Error("Список не открыт"));
            }
        });
        public async Task SaveListUserAsync(string patn) => await Task.Run(() =>
        {
            if (!string.IsNullOrWhiteSpace(patn))
            {
                mineModel.SaveList(patn);
                _dialogsService.Get<Action<Info>>()?.Invoke(new Info("Список сохранён"));
            }
            else
            {
                _dialogsService.Get<Action<Error>>()?.Invoke(new Error("Список не сохранён"));
            }
        });
    }
    public class Error
    {
        public string error;
        public Error(string message)
        {
            this.error = message;

        }
    }
    public class Info
    {
        public string Message;
        public Info(string message)
        {
            this.Message = message;
        }
    }
}
