using Model;
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
        public  Action<Info> MessageBus;
        private IDialogsService _dialogsService;
       // private Action<string> _messageDialog;
        public MainViewModel(MainModel mineModel, IDialogsService dialogsService)
        {
            AddUserVM = new(mineModel);
            this.mineModel = mineModel;            
            _dialogsService = dialogsService;
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
            //_messageDialog = messageDialog;
            mineModel.Message += MineModel_Message;
            ClearUserCommand = new RelayCommand(() => mineModel.ClearUzer(), () => Users?.Count > 0);
            //object lockitems = new object();
            //BindingOperations.EnableCollectionSynchronization(Users, lockitems);
        }

        private void MineModel_Message(object? sender, string e)
        {
            if (e == "Открыт новый список")
            {
                _dialogsService?.Get<Action<Info>>().Invoke(new Info(e));
                
            }
             
            //else _messageDialog(e);
        }

        public async Task OpenListUserAsync(string path) => await Task.Run(() =>
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                mineModel.OpenList(path);
            }
            else
            {
                _dialogsService.Get<Action<Error>>().Invoke(new Error("Список не открвт"));
            }
            //Thread.Sleep(3000);
            //Message = string.Empty;
        });
        public async Task SaveListUserAsync(string patn) => await Task.Run(() =>
        {
            if (!string.IsNullOrWhiteSpace(patn))
            {
                mineModel.SaveList(patn);
                _dialogsService.Get<Action<Info>>().Invoke(new Info("Список сохранён"));
            }
            else
            {
                _dialogsService.Get<Action<Error>>().Invoke(new Error("Список не сохранён"));
            }
            //Thread.Sleep(3000);
            //Message = string.Empty;
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
