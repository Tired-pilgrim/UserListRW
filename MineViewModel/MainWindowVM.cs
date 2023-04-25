﻿using Model;
using ModelLib;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using VievModelLib;
using ViewModelLib.Commands;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase, ISaveOpen
    {
        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => Set(ref _message, ref value);
        }
        public AddUserVM AddUserVM { get; }
        private readonly MainModel mineModel;
       
        public RelayCommand RemoveUserCommand { get; }
        public IEnumerable<User>? Users => mineModel.Users;
        public MainViewModel(MainModel mineModel)
        {
            AddUserVM = new(mineModel);
            this.mineModel = mineModel;
            mineModel.Message += (_, e) => Message = e;
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
        }
        public async Task OpenListUserAsync(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                Debug.WriteLine("Путь к Файлу:" + path);
                mineModel.OpenList(path);
            }
            else
            {
                Message = "Список НЕ загружен";
            }
            Thread.Sleep(3000);
            Message = string.Empty;
        });
        public async Task SaveListUserAsync(string patn) => await Task.Run(() =>
        {
            if (!string.IsNullOrWhiteSpace(patn))
            {
                mineModel.SaveList(patn);
                Message = "Список сохранён";
            }
            else
            {
                Message = "Список НЕ сохранён";
            }
            Thread.Sleep(3000);
            Message = string.Empty;
        });

    }
}
