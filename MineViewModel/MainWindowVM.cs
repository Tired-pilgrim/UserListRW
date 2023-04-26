using Model;
using ModelLib;
using System;
using System.Collections.ObjectModel;
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
        public RelayCommand ClearUserCommand { get; }
        public ReadOnlyObservableCollection<User>? Users => mineModel.Users;
        private Action<string> _messageDialog;
        public MainViewModel(MainModel mineModel, Action<string> messageDialog)
        {
            AddUserVM = new(mineModel);
            this.mineModel = mineModel;            
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
            _messageDialog = messageDialog;
            mineModel.Message += MineModel_Message;
            ClearUserCommand = new RelayCommand(() => mineModel.ClearUzer(), () => Users?.Count > 0);
            //object lockitems = new object();
            //BindingOperations.EnableCollectionSynchronization(Users, lockitems);
        }

        private void MineModel_Message(object? sender, string e)
        {
            if (e == "Открыт новый список") Message = e;
            else _messageDialog(e);
        }

        public async Task OpenListUserAsync(string path) => await Task.Run(() =>
        {            
            if (!string.IsNullOrWhiteSpace(path))
            {
                mineModel.OpenList(path);
            }
            else
            {
                _messageDialog("Список не открыт");
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
                _messageDialog("Список не сохранён");
            }
            Thread.Sleep(3000);
            Message = string.Empty;
        });
    }
}
