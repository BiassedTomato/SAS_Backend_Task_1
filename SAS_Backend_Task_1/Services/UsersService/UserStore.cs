using SAS_Backend_Task_1.Models;
using System.Collections;
using System.Collections.Generic;

namespace SAS_Backend_Task_1
{
    public class UserStore : IUserStore
    {
        // HACK: I felt like uint range was way too small
        public static Dictionary<ulong, UserModel> Users = new Dictionary<ulong, UserModel>();

        uint id = 0;

        public void Add(UserModel user)
        {
            user.ID = id++;

            Users.Add(user.ID, user);

        }

        public void Edit(ulong id,UserModel user)
        {
            Users[id] = user;
        }

        public UserModel Get(ulong id)
        {
            return Users[id];
        }

        public void Remove(ulong id)
        {
            Users.Remove(id);
        }
    }
}
