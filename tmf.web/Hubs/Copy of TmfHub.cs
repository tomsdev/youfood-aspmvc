//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using SignalR.Hubs;

//namespace tmf.web.Hubs
//{
//    [HubName("tmfHub")]
//    public class TmfHub : Hub
//    {
//        private static ConcurrentDictionary<string, User> _users = new ConcurrentDictionary<string, User>(StringComparer.OrdinalIgnoreCase);
//        private static List<Item> _items = new List<Item>();
//        private static Random _userNameGenerator = new Random();

//        public TmfHub()
//        {
//            var server = new User()
//            {
//                Name = "Server"
//            };

//            _items = new List<Item>
//            {
//                new Item() {
//                    Title = "one",
//                    ChangedBy = server
//                },
//                new Item() {
//                    Title = "two",
//                    ChangedBy = server
//                },
//                new Item() {
//                    Title = "three",
//                    ChangedBy = server
//                }
//            };
//        }

//        public IEnumerable<Item> GetItems()
//        {
//            return _items;
//        }

//        //public Task AddOrder(Guid id)
//        //{
//        //    return Clients.orderAdded(id);
//        //}

//        public Task CreateItem(string title = "defaultItem")
//        {
//            string name = Caller.user["Name"];
//            // bug: users est vide alors qu'il était plein, le hub est recréer a chaque fois je crois..
//            var user = _users[name];

//            var item = new Item();
//            item.Title = title;
//            item.ChangedBy = user;

//            _items.Add(item);

//            return Clients.itemAdded(item);
//        }

//        public void Join(string userName)
//        {
//            User user = null;
//            if (string.IsNullOrWhiteSpace(userName))
//            {
//                user = new User();
//                do
//                {
//                    user.Name = "User" + _userNameGenerator.Next(1000);
//                } while (!_users.TryAdd(user.Name, user));
//            }
//            else if (!_users.TryGetValue(userName, out user))
//            {
//                user = new User { Name = userName };
//                _users.TryAdd(userName, user);
//            }
//            Caller.user = user;
//        }
//    }

//    public class Item
//    {
//        public string ID { get; private set; }
//        public string Title { get; set; }
//        public User ChangedBy { get; set; }

//        public Item()
//        {
//            ID = Guid.NewGuid().ToString("d");
//        }
//    }

//    public class User
//    {
//        public string ID { get; private set; }
//        public string Name { get; set; }

//        public User()
//        {
//            ID = Guid.NewGuid().ToString("d");
//        }
//    }
//}