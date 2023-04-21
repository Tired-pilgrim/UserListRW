using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Views.Windows
{
    public class OpenSaveViewHelper
    {
        private static OpenFileDialog ?_openFdialog;
        private static SaveFileDialog ?_saveFdialog;
        private readonly static string ListFolder = Environment.CurrentDirectory + @"\Списки\";
         static OpenSaveViewHelper()
        {
            Debug.WriteLine(" Конструктор OpenSaveViewHelper сработал");
            Directory.CreateDirectory(ListFolder);
            _openFdialog = new()
            {
                Filter = "Список сотрудников(*.json)|*.json| Все файлы (*.*)|*.*",
                FileName = "Список сотрудников",
                DefaultExt = ".json",
                InitialDirectory = ListFolder
            };
            _saveFdialog = new()
            {
                Filter = "Список сотрудников(*.json)|*.json| Все файлы (*.*)|*.*",
                FileName = "Список сотрудников",
                DefaultExt = ".json",
                InitialDirectory = ListFolder
            };
        }

        public static string OpenDial()
        {
            if (_openFdialog != null && _openFdialog.ShowDialog() == true)
            {
               return _openFdialog.FileName;
               
            }
            return string.Empty;
        }


        public static string SaveDial()
        {
            if (_saveFdialog != null && _saveFdialog.ShowDialog() == true)
            {
                return _saveFdialog.FileName;
            }
            return string.Empty;
        }

    }
}

