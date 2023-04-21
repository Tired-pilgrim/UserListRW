using System.Windows;
using System.Windows.Controls;
using ViewModel;
using ViewModelLib.Commands;

namespace Views.Windows
{
    public  class ViewHelper
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
       
    }
 }

