using Model;
using ModelLib;
using System.Collections.ObjectModel;
using VievModelLib;
using ViewModelLib.Commands;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //protected override void OnPropertyChanged(string? PropertyName)
        //{
        //    base.OnPropertyChanged(PropertyName);
        //    if (PropertyName == nameof(Path) && !string.IsNullOrEmpty(Path))
        //    {
        //        if (Path != string.Empty) mineModel.OpenList(Path);
        //    }
        //}
        
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
        public void OpenListUser(string puth) => mineModel.OpenList(puth);
        // Только для режима разработки
        //public MainViewModel()
        //{
        //    mineModel = new();
        //    AddUserCommand = new RelayCommand(() => { });
        //}
    }
}
