using System.Windows;
using System.Windows.Controls;
using ViewModel;

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
                        DataContext = currWin.DataContext,
                        Owner = currWin,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    _addUzer.ShowDialog();
                }
            };
        public static RoutedEventHandler OpenListUserDialog { get; } = (s, _) =>
        {
            Window? currWin = Window.GetWindow((Button)s);
            ((MainViewModel)currWin.DataContext).OpenListUser(OpenSaveHelper.OpenDial());
        };
    }
    }

