using ModelLib;
using System.Collections;
using System.Collections.ObjectModel;

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

        public MainModel()
        {
            Users = new(privateUsers);
        }

        public void AddUzer(User user) => privateUsers.Add(user);
        public void RemoveUzer(User user) => privateUsers.Remove(user);
        public void OpenList(ObservableCollection<User> users)
        {
            lock (((ICollection)privateUsers).SyncRoot)
            {
                privateUsers.Clear();
                foreach (User user in users)
                {
                    privateUsers.Add(user);
                }
            }
        }
    }

}
