using Model;
using ModelLib;
using System.Collections.Generic;
using System.Threading.Tasks;
using VievModelLib;
using ViewLib;
using ViewModelLib.Commands;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
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
        public RelayCommand OpenDialogCommand { get; }
        public RelayCommand SaveDialogCommand { get; }
        public IEnumerable<User>? Users => mineModel.Users;
        //private IOpenSave _openSave;
        public MainViewModel(MainModel mineModel, IOpenSave openSave)
        {
              
            AddUserVM = new(mineModel);
            this.mineModel = mineModel;
            mineModel.Message += (_, e) => Message = e;
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
            OpenDialogCommand = new RelayCommand(() => _ = OpenListUserAsync(openSave.OpenDial()));
            SaveDialogCommand = new RelayCommand(() => _ = SaveListUser(openSave.SaveDial()));
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
        public async Task SaveListUser(string putn)
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
