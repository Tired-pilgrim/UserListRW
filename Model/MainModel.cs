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
        
        public MainModel()
        {
            Users = new();
           
        }

        public void AddUzer(User user)
        {
            if (Users != null)  Users.Add(user);  
        }
        public void RemoveUzer(User user)
        {
            if (Users != null) Users.Remove(user);
        }
        public void OpenList(ObservableCollection<User> users)
        {
            Users = users;
            NewUserList?.Invoke(this, EventArgs.Empty);
        }

        public void SaveList(string path)
        {
            //if (Users != null && Users.Count > 0 && !string.IsNullOrEmpty(path))
            //{
            //    File.WriteAllText(path, JsonSerializer.Serialize(Users, jso));
            //}
            //else Debug.WriteLine("Список не схранён");
        }
    }

}
