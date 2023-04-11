using Model;
using ModelLib;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VievModelLib;
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
        private readonly MainModel mineModel;
        public RelayCommand AddUserCommand { get; }
        public RelayCommand RemoveUserCommand { get; }
        public ObservableCollection<User>? Users => mineModel.Users;
        public MainViewModel(MainModel mineModel)
        {
            this.mineModel = mineModel;
            mineModel.NewUserList += (s, e) => OnPropertyChanged(nameof(Users));
            AddUserCommand = new RelayCommand<User>
            (
                user => mineModel.AddUzer(user),
                user => !(string.IsNullOrWhiteSpace(user.Name) ||
                          string.IsNullOrWhiteSpace(user.Family) ||
                          string.IsNullOrWhiteSpace(user.Job)));
            RemoveUserCommand = new RelayCommand<User>(User => mineModel.RemoveUzer(User));
        }
        public async Task OpenListUserAsync(ObservableCollection<User> users)
        {
            if (users != null)
            {
                mineModel.OpenList(users);
                Message = "Открыт новый список";
            } 
            else
            {
                Message = "Список  НЕ загружен";                
            }
            await Task.Delay(3000);
            Message = string.Empty;
        }
        public async Task SaveListUser(bool Success)
        {
            if (Success)
            {
                Message = "Список сохранён";
            }
            else
            {
                Message = "Список НЕ сохранён";
            }
            await Task.Delay(3000);
            Message = string.Empty;
        }
        // Только для режима разработки
        //public MainViewModel()
        //{
        //    mineModel = new();
        //    AddUserCommand = new RelayCommand(() => { });
        //}
    }
}
