using System.Windows;
using System.Windows.Controls;
using ViewModel;
using ViewModelLib.Commands;

namespace Views.Windows
{
    public  class ViewHelper
    {
        private static OpenSaveAppHelper _OSHelper;
        public ViewHelper(OpenSaveAppHelper OSHelper)
        {
            _OSHelper = OSHelper;
        }
            public static RoutedEventHandler AddUserDialog { get; } = (s, _) =>
            {
                Window? currWin = Window.GetWindow((Button)s);
                if (currWin.DataContext != null)
                {
                    AddUzer _addUzer = new AddUzer()
                    {
                        DataContext = currWin.DataContext,
                        Owner = currWin,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    _addUzer.ShowDialog();
                }
            };
        public static RelayCommand<MainViewModel> OpenListUserDialog { get; } = new RelayCommand<MainViewModel>(vm =>
        {
            vm.OpenListUserAsync(_OSHelper.OpenDial());
        });
        public static RelayCommand<MainViewModel>  SaveListUserDialog { get; } = new RelayCommand<MainViewModel>(vm =>
        {
             vm.SaveListUser(_OSHelper.SaveDial(vm.Users));
        });
    }
 }

