﻿using Microsoft.Win32;
using System;
using System.IO;

namespace Views.Windows
{
    public static class OpenSaveHelper
    {
        static  OpenFileDialog _openFdialog;
        static SaveFileDialog _saveFdialog;
        static readonly string ListFolder = Environment.CurrentDirectory + @"\Списки\";
        static OpenSaveHelper()
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

       public static string OpenDial()
        {
            bool? result = _openFdialog.ShowDialog();
            if (result == true)
            {
                return _openFdialog.FileName;
            }
            return string.Empty;
        }
        public static string  SaveDial()
        {
            bool? result = _saveFdialog.ShowDialog();
            if (result == true)
            {
                return _saveFdialog.FileName;
            }
            return string.Empty;
        }
    }
}
