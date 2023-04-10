using ModelLib;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Model
{
    public class MainModel
    {
        //Вторая ветвь
        public ObservableCollection<User>? Users { get; private set; }
      
        public event EventHandler? NewUserList;
        private readonly JsonSerializerOptions jso;
        public MainModel()
        {
            Users = new();
            jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        public void AddUzer(User user)
        {
            if (Users != null)  Users.Add(user);  
        }
        public void RemoveUzer(User user)
        {
            if (Users != null) Users.Remove(user);
        }
        public void OpenList(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string StrJson = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(StrJson))
                {
                    Users = JsonSerializer.Deserialize<ObservableCollection<User>>(StrJson);
                    NewUserList?.Invoke(this, EventArgs.Empty);
                }
                else Debug.WriteLine("Список не загружен");
            }
            else Debug.WriteLine("Список не загружен");
        }

        public void SaveList(string path)
        {
            if (Users != null && Users.Count > 0 && !string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, JsonSerializer.Serialize(Users, jso));
            }
            else Debug.WriteLine("Список не схранён");
        }
    }

}
