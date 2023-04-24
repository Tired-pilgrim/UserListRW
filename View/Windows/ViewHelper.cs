using System;
using System.Windows;
using System.Windows.Markup;
using ViewModel;
using ViewModelLib.Commands;
using static Views.Windows.OpenSaveViewHelper;
namespace Views.Windows
{
    [MarkupExtensionReturnType(typeof(RelayCommand))]
    public class ViewHelper: MarkupExtension
    {
        public ViewHelper() 
        { 
            this.AddUserDialog = AddUserDialog;
        }
        private RelayCommand AddUserDialog { get; } = new RelayCommand( _ =>
            {
                if (currWin?.DataContext != null)
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
        private static Window? currWin;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget service = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget))!;
            currWin = Window.GetWindow(service.TargetObject as DependencyObject);
            return AddUserDialog;
            
        }
    }
 }

