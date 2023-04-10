using Microsoft.Win32;
using ModelLib;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Shapes;

namespace Views.Windows
{
    public static class OpenSaveHelper
    {
        static OpenFileDialog _openFdialog;
        static SaveFileDialog _saveFdialog;
        static readonly string ListFolder = Environment.CurrentDirectory + @"\Списки\";
        static readonly JsonSerializerOptions jso;
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
            jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        public static ObservableCollection<User>? OpenDial()
        {
            bool? result = _openFdialog.ShowDialog();
            if (result == true)
            {
                string StrJson = File.ReadAllText(_openFdialog.FileName);
                if (!string.IsNullOrEmpty(StrJson))
                {
                    return JsonSerializer.Deserialize<ObservableCollection<User>>(StrJson);
                }
                else Debug.WriteLine("Список не загружен");
            }
            else Debug.WriteLine("Список не загружен");
            return null;
        }
            
   
    
        public static bool  SaveDial(ObservableCollection<User> Users)
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
    }
}
