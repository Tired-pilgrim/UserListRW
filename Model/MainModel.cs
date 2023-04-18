using ModelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Model
{
    public class MainModel
    {
        public ReadOnlyObservableCollection<User> Users { get; }
        private ObservableCollection<User> privateUsers { get; } = new()
        {
            new User { Name = "Вася", Family = "Васильев", Job="Студент" },
            new User { Name = "Николай", Family = "Алексеев", Job="Аспирант"},
            new User { Name = "Сидор", Family = "Сидоров", Job="Ректор" }
        };
        public Repository repository { get; }
        public MainModel()
        {
            Users = new(privateUsers);
            repository = new Repository(this);
        }
        //public event EventHandler<string>? Message;
        public void AddUzer(User user)
        {
            lock (((ICollection)privateUsers).SyncRoot)
                privateUsers.Add(user);
        }
        public void RemoveUzer(User user)
        {
            lock (((ICollection)privateUsers).SyncRoot)
                privateUsers.Remove(user);
        }
        public void OpenList(User[] users)
        {
            foreach (User user in users)
            {
                privateUsers.Add(user);
            }
        }
        public void SaveList(string path)
        {
            if (privateUsers.Count > 0) 
            {
                lock (((ICollection)privateUsers).SyncRoot)
                {
                    string StrJson = JsonSerializer.Serialize(privateUsers);
                    File.WriteAllText(path, StrJson);
                }
            }
        }

     }

}
