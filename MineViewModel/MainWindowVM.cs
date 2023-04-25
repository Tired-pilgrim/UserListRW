using Model;
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
        public async Task OpenListUserAsync(string puth)
        {
            if (!string.IsNullOrWhiteSpace(puth))
            {
                mineModel.OpenList(puth);
            }
            else
            {
                Message = "Список НЕ загружен";
            }
            await Task.Delay(3000);
            Message = string.Empty;
        }
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
