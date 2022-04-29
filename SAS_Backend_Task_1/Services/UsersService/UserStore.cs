using SAS_Backend_Task_1.Models;
using System.Collections;
using System.Collections.Generic;

namespace SAS_Backend_Task_1
{
    public class UserStore : IUserStore
    {
        // HACK: I felt like uint range was way too small
        private Dictionary<ulong, UserModel> _users = new Dictionary<ulong, UserModel>();

        public ulong Size => (ulong)_users.Count;

        uint id = 0;

        public void Add(UserModel user)
        {
            user.ID = id++;

            _users.Add(user.ID, user);

        }

        public void Edit(ulong id,UserModel user)
        {
            _users[id] = user;
        }

        public UserModel Get(ulong id)
        {
            return _users[id];
        }

        public void Remove(ulong id)
        {
            _users.Remove(id);
        }
    }
}
