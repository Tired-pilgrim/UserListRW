using Microsoft.Win32;
using System;
using System.IO;

namespace Views.Windows
{
    public class OpenSaveAppHelper
    {
        private static OpenFileDialog _openFdialog;
        private static SaveFileDialog _saveFdialog;
        private readonly string ListFolder = Environment.CurrentDirectory + @"\Списки\";
        public OpenSaveAppHelper()
        {
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

        public string OpenDial()
        {
            if (_openFdialog != null && _openFdialog.ShowDialog() == true)
            {
               return _openFdialog.FileName;
               
            }
            return string.Empty;
        }


        public string SaveDial()
        {
            if (_saveFdialog != null && _saveFdialog.ShowDialog() == true)
            {
                return _saveFdialog.FileName;
            }
            return string.Empty;
        }

    }
}

