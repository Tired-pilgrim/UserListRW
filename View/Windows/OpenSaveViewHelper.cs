using Microsoft.Win32;
using ModelLib;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;

namespace Views.Windows
{
    public class OpenSaveViewHelper
    {
        private OpenFileDialog _openFdialog;
        private SaveFileDialog _saveFdialog;
        private readonly string ListFolder = Environment.CurrentDirectory + @"\Списки\";
        private readonly JsonSerializerOptions jso;
        public OpenSaveViewHelper()
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
            jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        public  ObservableCollection<User>? OpenDial()
        {
            bool? result = _openFdialog.ShowDialog();
            if (result == true)
            {
                string StrJson = File.ReadAllText(_openFdialog.FileName);
                if (!string.IsNullOrEmpty(StrJson))
                {
                    try
                    {
                        return JsonSerializer.Deserialize<ObservableCollection<User>>(StrJson);
                    }
                    catch (JsonException)
                    {
                        ShowMessage("Не правильный формат списка");
                        return null;
                    }
                }
            }
            return null;
        }


        public  bool SaveDial(ObservableCollection<User> Users)
        {
            bool? result = _saveFdialog.ShowDialog();
            if (result == true)
            {
                if (Users != null && Users.Count > 0)
                {
                    File.WriteAllText(_saveFdialog.FileName, JsonSerializer.Serialize(Users, jso));
                    return true;
                }
                else Debug.WriteLine("Список не схранён");
            }
            return false;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "Ошибка");
        }
    }
}

