using SAS_Backend_Task_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1.Models
{
    public interface IUserStore
    {
        ulong Size { get; }
        void Add(UserModel user);

        void Remove(ulong id);

        void Edit(ulong id, UserModel user);

        UserModel Get(ulong id);
    }
}
