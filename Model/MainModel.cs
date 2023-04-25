using ModelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Shapes;

namespace Model
{
    public class MainModel
    {
        public ReadOnlyObservableCollection<User> Users { get; }
        public ObservableCollection<User> privateUsers { get; } = new()
        {
            new User { Name = "Вася", Family = "Васильев", Job="Студент" },
            new User { Name = "Николай", Family = "Алексеев", Job="Аспирант"},
            new User { Name = "Сидор", Family = "Сидоров", Job="Ректор" }
        };

        public MainModel()
        {
            Users = new(privateUsers);
        }
        public event EventHandler<string>? Message;
        public void AddUzer(User user)
        {
            Debug.WriteLine("Добавлен сотрудник");
            lock (((ICollection)privateUsers).SyncRoot)
                privateUsers.Add(user);
        }
        public void RemoveUzer(User user)
        {
            lock (((ICollection)privateUsers).SyncRoot)
                privateUsers.Remove(user);
        }
        public void OpenList(string path)
        {
            lock (((ICollection)privateUsers).SyncRoot)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    string StrJson = File.ReadAllText(path);
                    if (!string.IsNullOrEmpty(StrJson))
                    {
                        try
                        {
                            User[]? users = JsonSerializer.Deserialize<User[]>(StrJson);
                            privateUsers.Clear();
                            if (users != null)
                            {
                                foreach (User user in users)
                                {
                                    privateUsers.Add(user);
                                }
                                Message?.Invoke(this, "Открыт новый список");
                            }
                        }
                        catch
                        {
                            Message?.Invoke(this, "Не удалось открыть список");
                        }
                    }
                }
            }
        }
        public void OpenListConc(string path, ObservableCollection<User> UsersC)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string StrJson = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(StrJson))
                {
                    try
                    {
                        User[]? users = JsonSerializer.Deserialize<User[]>(StrJson);
                        UsersC.Clear();
                        {
                            foreach (User user in users)
                            {
                                UsersC.Add(user);
                            }
                            Message?.Invoke(this, "Открыт новый список");
                        }
                    }
                    catch
                    {
                        Message?.Invoke(this, "Не удалось открыть список");
                    }
                }
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
