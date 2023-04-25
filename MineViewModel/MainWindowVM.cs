using Model;
using ModelLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
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
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public MainViewModel(MainModel mineModel)
        {
            AddUserVM = new(mineModel);
            this.mineModel = mineModel;
            mineModel.Message += (_, e) => Message = e;
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
            object lockitems = new object();
            BindingOperations.EnableCollectionSynchronization(Users, lockitems);

        }
        public async Task OpenListUserAsync(string path) => await Task.Run(() =>
        {
            
            if (!string.IsNullOrWhiteSpace(path))
            {
                //mineModel.OpenList(path);
                mineModel.OpenListConc(path, Users);
            }
            else
            {
                Message = "Список НЕ загружен";
            }
            Thread.Sleep(3000);
            Message = string.Empty;
        });
        public async Task SaveListUserAsync(string putn)
        {
            if (!string.IsNullOrWhiteSpace(putn))
            {
                mineModel.SaveList(putn);
                Message = "Список сохранён";
            }
            else
            {
                Message = "Список НЕ сохранён";
            }
            await Task.Delay(3000);
            Message = string.Empty;
        }

    }
}
