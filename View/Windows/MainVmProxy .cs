using System.Diagnostics;
using System.Windows;
using ViewModel;
using ViewModelLib.Commands;
using static Views.Windows.OpenSaveViewHelper;

namespace Views.Windows
{
    public class MainVmProxy : ValueProxy<MainViewModel>
    {
        public MainVmProxy()
        {
            AddUserDialog = new RelayCommand<DependencyObject>(dobj =>
            {
                Window? currWin = Window.GetWindow(dobj);
                if (currWin.DataContext != null)
                {
                    AddUzer _addUzer = new AddUzer()
                    {
                        DataContext = ProtectedValue!.AddUserVM,
                        Owner = currWin,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    _addUzer.ShowDialog();
                }
            });
            OpenListUserDialog = new RelayCommand(() =>
            {
                _ = ProtectedValue!.OpenListUserAsync(OpenDial());
            });
            SaveListUserDialog = new RelayCommand(() =>
            {
                _ = ProtectedValue!.SaveListUser(SaveDial());
            });
        }
        public RelayCommand AddUserDialog { get; }
        public RelayCommand OpenListUserDialog { get; }
        public RelayCommand SaveListUserDialog { get; }
    }
}
