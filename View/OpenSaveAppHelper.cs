﻿using Microsoft.Win32;
using System;
using System.IO;

namespace Views.Windows
{
    public class OpenSaveAppHelper
    {
        private OpenFileDialog _openFdialog;
        private SaveFileDialog _saveFdialog;
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
            bool? result = _openFdialog.ShowDialog();
            if (result == true)
            {
                return _openFdialog.FileName;
            }
            return string.Empty;
        }


        public string SaveDial()
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
