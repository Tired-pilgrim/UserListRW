using System.Windows;
using System.Windows.Controls;
using ViewModel;
using ViewModelLib.Commands;
using static Views.Windows.OpenSaveViewHelper;
namespace Views.Windows
{
    public static class ViewHelper
    {
        public static RoutedEventHandler AddUserDialog { get; } = (s, _) =>
        {
            Window? currWin = Window.GetWindow((Button)s);
            if (currWin.DataContext != null)
            {
                AddUzer _addUzer = new AddUzer()
                {
                    DataContext = ((MainViewModel)currWin.DataContext).AddUserVM,
                    Owner = currWin,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                _addUzer.ShowDialog();
            }
        };
        public static RelayCommand<ISaveOpen> OpenListUserDialog { get; } = new RelayCommand<ISaveOpen>(vm =>
        {
            _ = vm.OpenListUserAsync(OpenDial());
        });
        public static RelayCommand<ISaveOpen>  SaveListUserDialog { get; } = new RelayCommand<ISaveOpen>(vm =>
        {
            _ = vm.SaveListUser(SaveDial());
        });
    }
 }

