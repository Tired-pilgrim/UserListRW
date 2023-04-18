using ModelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Model
{
    public class Repository
    {
        MainModel _model;
        public Repository(MainModel model) 
        {
            _model = model;
        }
        public event EventHandler<string>? Message;
        public void GetUser(string path)
        {
            Debug.WriteLine("Получен путь " + path);
            if (!string.IsNullOrEmpty(path))
            {
                 User[]? Users = null;
                string StrJson = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(StrJson))
                {
                    lock (((ICollection)Users).SyncRoot)
                    {
                        try
                        {
                            User[]? users = JsonSerializer.Deserialize<User[]>(StrJson);
                            if (users != null)
                            {
                                _model.OpenList(users);
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
    }
}
