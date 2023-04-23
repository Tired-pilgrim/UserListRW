using System.Windows;
using System.Windows.Controls;
using ViewModel;
using ViewModelLib.Commands;
using static Views.Windows.OpenSaveViewHelper;
namespace Views.Windows
{
    public static class ViewHelper
    {
        public static RelayCommand<DependencyObject> AddUserDialog { get; } = new RelayCommand<DependencyObject>(dobj =>
        {
            Window? currWin = Window.GetWindow(dobj);
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
        });
        public static RelayCommand<ISaveOpen> OpenListUserDialog { get; } = new RelayCommand<ISaveOpen>(Ivm =>
        {
            _ = Ivm.OpenListUserAsync(OpenDial());
        });
        public static RelayCommand<ISaveOpen>  SaveListUserDialog { get; } = new RelayCommand<ISaveOpen>(Ivm =>
        {
            _ = Ivm.SaveListUser(SaveDial());
        });
        
    }
 }

