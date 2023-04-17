using System.Windows;
using System.Windows.Controls;
using ViewModel;
using ViewModelLib.Commands;

namespace Views.Windows
{
    public static class ViewHelper
    {
        //private static OpenSaveAppHelper? _OSHelper;
        //public ViewHelper(OpenSaveAppHelper OSHelper)
        //{
        //    _OSHelper = OSHelper;
        //}
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
        public static RelayCommand<MainViewModel> OpenListUserDialog { get; } = new RelayCommand<MainViewModel>(vm =>
        {
             _ = vm.OpenListUserAsync(OpenSaveAppHelper.OpenDial());
        });
        public static RelayCommand<MainViewModel>  SaveListUserDialog { get; } = new RelayCommand<MainViewModel>(vm =>
        {
            _ = vm.SaveListUser(OpenSaveAppHelper.SaveDial());
        });
    }
 }

